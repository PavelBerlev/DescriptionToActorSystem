using System.Xml;
using System.Xml.Linq;
using ActorSystem.Actors;
using ActorSystem.Communication;
using ActorSystem.DI;
using ActorSystem.Entity;
using ActorSystem.Factory;
using ActorSystem.Grammar;
using ActorSystem.Parser;

namespace ActorSystem;

public class ActorSystemMVP
{
    private IDictionary<string, IMailBox> ?_mailBoxes;
    private IRedirectRuleRepository? _redirectRuleRepository;
    private IMessageQueue? _messageQueue;
    private IMessageSystem? _messageSystem;
    private EventLoop? _eventLoop;
    private IDictionary<string, ActorBase>? _actors;
    private IDictionary<string, ActorWorker>? _actorWorkers;

    public void Start()
    {
        _eventLoop!.Start();
        foreach(var key in _actorWorkers!.Keys)
        {
            _actorWorkers[key].Start();
        }
    }

    public async void Stop()
    {
         _eventLoop!.Stop();
        foreach(var key in _actorWorkers!.Keys)
        {
            await _actorWorkers[key].Stop();
        }
    }

    public void Send(IMessage message)
    {
        _messageSystem!.requestMessage(message);
    }

    public void AddRedirectRule(RedirectRule rule)
    {
        _redirectRuleRepository!.AddOrUpdateAdressReceiver(rule);
    }
    public void loadXml(XmlDocument xmlDocument)
    {
        //Проверка на грамматику
        if(!isCorrectXmlDocument(xmlDocument))
        {
            throw new InvalidDataException("Входные данные не соответсвуют грамматике");
        }

        //Чтение сузностей
        var entities = readEntitiesFromXml(xmlDocument);

        //Задание ящиков
        _mailBoxes = createMailBoxes(entities);

        //Чтение правил перессылки сообщений
        var redirectRules = createRules(entities);
        _redirectRuleRepository = createRedirectRuleRepository(_mailBoxes, redirectRules);

        //Создание Очередь сообщений-> Системы сообщений -> EventLoop
        _messageQueue = createMessageQueue();
        _messageSystem = createMessageSystem(_messageQueue);
        _eventLoop = new EventLoop(_redirectRuleRepository, _messageQueue);
       
        //Акторы
        _actors = createActors(entities);
        _actorWorkers = createWorkers(_actors, 1);
    }

    private bool isCorrectXmlDocument(XmlDocument xmlDocument)
    {
        //Преобразовать в строку
        var parser = new XMLParser(xmlDocument);
        var input = parser.ConvertToString();
        //Проверить на соотвествие грамматике
        var validator = new GrammarValidator();
        bool isCorrectInput = validator.isCorrectInput(input);

        return isCorrectInput;
    }

    private List<IEntity> readEntitiesFromXml(XmlDocument xmlDocument)
    {
        //Чтение сущностей
        var convertedXDocument = XDocument.Parse(xmlDocument.OuterXml);
        var actors = convertedXDocument.Element("ActorSystem");
        if(actors == null)
        {
            throw new InvalidDataException("Входной файл не содержит ActorSystem");
        }
        return IoC.Resolve<XmlEntityLoader>().Load(actors);

    }

    private IDictionary<string, IMailBox> createMailBoxes(List<IEntity> entities)
    {
        var mailBoxNames = entities.Select(e => (string)e.Fields["Name"]).ToHashSet();

        return IoC.Resolve<IDictMailBoxFactory>().Create(mailBoxNames);
    }

    private ISet<RedirectRule> createRules (List<IEntity> entities)
    {
        return IoC.Resolve<IRedirectRulesFactory>().Create(entities);
    }

    private IRedirectRuleRepository createRedirectRuleRepository(IDictionary<string,IMailBox> mailBoxes, ISet<RedirectRule> rules)
    {
        return IoC.Resolve<IRedirectRuleRepositoryFactory>().Create(mailBoxes, rules);
    }

    private IMessageQueue createMessageQueue()
    {
        return IoC.Resolve<IMessageQueue>();
    }

    private IMessageSystem createMessageSystem(IMessageQueue queue)
    {
        return new MessageSystemEventLoopBased(queue);
    }

    private IDictionary<string, ActorBase> createActors(IList<IEntity> entities)
    {
        return IoC.Resolve<IActorsFactory>().Create(_mailBoxes!,_messageSystem!, entities);
    }

    private IDictionary<string, ActorWorker> createWorkers(IDictionary<string, ActorBase> actors, int maxConcurrency)
    {
        return IoC.Resolve<IWorkersFactory>().Create(actors, maxConcurrency);
    }
}