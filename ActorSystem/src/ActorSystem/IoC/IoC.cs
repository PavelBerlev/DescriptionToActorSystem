using ActorSystem.Communication;
using ActorSystem.Factory;
using ActorSystem.Entity;
using System.Net;
using MongoDB.Driver;
using System.Net.Mail;

namespace ActorSystem.DI;

public static class IoC
    {
        private static readonly IServiceProvider _provider;

        static IoC()
        {
            var services = new ServiceCollection();
            //Компоненты акторной системы
            services.AddTransient<IMessage, Message>();
            services.AddTransient<IMailBox, MailBox>();
            services.AddTransient<IMessageQueue, MessageQueue>();
            services.AddTransient<IMessageSystem, MessageSystemEventLoopBased>();

            //Фабрики
            services.AddTransient<IDictMailBoxFactory, DictMailBoxFactory>();
            services.AddTransient<IRedirectRuleRepositoryFactory,RedirectRuleRepositoryFactory>();
            services.AddTransient<IRedirectRulesFactory, RedirectRulesFactory>();


            //Сущности, добавлять по мере появления
            services.AddKeyedTransient<IXmlEntity, DBWriterEntity>("DBWriter");
            services.AddKeyedTransient<IXmlEntity, TaskNotificatorEntity>("TaskNotificator");

            //Загрузка сущностей из XML файла
            services.AddTransient<XmlEntityFactory>();
            services.AddTransient<XmlEntityLoader>();
            
            //Правила перессылок, добавлять по мере появления
            services.AddKeyedTransient<IRuleProcces, DBWriterActorRules>("DBWriter");
            services.AddKeyedTransient<IRuleProcces, TaskNotionActorRules>("TaskNotificator");

            //Способ создания актора, добавлять по мере появления
            services.AddKeyedTransient<IActorCreator, DBWriterActorCreator>("DBWriter");
            services.AddKeyedTransient<IActorCreator, TaskNotificatorActorCreator>("TaskNotificator");

            //Фабрика создания акторов
            services.AddTransient<IActorsFactory, ActorsFactory>();
            services.AddTransient<IWorkersFactory, ActorWorkersFactory>();

            //Подключение к БД, заменить !!!!!
            string connectionURI = "mongodb://root:password@localhost:27017";
            string dataBaseName = "ActorSystem";
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                return new MongoClient(connectionURI);
            });
            services.AddSingleton<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(dataBaseName);
            });


            //Клиент отправки Email
            var username = "00c224c2033a04";
            var password = "f3d7f7fd2e626f";
            var port = 2525;
            var host = "sandbox.smtp.mailtrap.io";
            var mailClient = new SmtpClient()
            {
                Credentials = new NetworkCredential(username, password),
                Host = host,
                Port = port,
                EnableSsl = true
            };
            services.AddSingleton(mailClient);


            _provider = services.BuildServiceProvider();
        }

    public static T Resolve<T>() where T : class
    {
        return _provider.GetRequiredService<T>();
    }
}
