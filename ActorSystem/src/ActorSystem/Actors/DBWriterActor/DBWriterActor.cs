using ActorSystem.Communication;
using ActorSystem.DI;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace ActorSystem.Actors;
public class DBWriterActor : ActorBase
{
    string[] _keys;
    IMongoCollection<BsonDocument> _collection;
    IMongoDatabase _dataBase;
    bool _mustSendToNext = false;
    public DBWriterActor(IMessageSystem messageSystem, IMailBox mailBox,string ID, IMongoDatabase database, string collection,bool mustSendToNext, params string[] keys) : base(messageSystem, mailBox, ID)
    {
        _dataBase = database;
        _collection = database.GetCollection<BsonDocument>(collection);
        _mustSendToNext = mustSendToNext;
        _keys = keys;
    }

    public override async Task HandleMessage()
    {
        var message = await _mailbox.GetMessage();
        Console.WriteLine("Получил сообщение");
        var document = new BsonDocument();

        //Для записи файлов
        var gridFSBucket = new GridFSBucket(_dataBase);
        var fileIds = new List<string>();
        foreach(var key in _keys)
        {
            if(message.Context.ContainsKey(key))
            {
                if (message.Context[key] is IList<FileUploadData> files)
                {
                    foreach(var file in files)
                    {
                        file.FileContent.Position = 0;
                        var fileId = await gridFSBucket.UploadFromStreamAsync(file.FileName, file.FileContent);
                        fileIds.Add(fileId.ToString());
                    }
                }
                else
                {
                    document[key] = (string)message.Context[key]!;
                }
            }
        }
        if(fileIds.Count > 0)
        {
            document["FileIds"] = new BsonArray(fileIds);
        }
        await _collection.InsertOneAsync(document);

        //Отправить сообщение дальше по цепочке
        if(_mustSendToNext)
        {
            var msg = IoC.Resolve<IMessage>();
            msg.Sender = _id;
            msg.Receiver = "Next";
            msg.Context = message.Context;
            _messageSystem.requestMessage(msg);
        }
      
    }
}