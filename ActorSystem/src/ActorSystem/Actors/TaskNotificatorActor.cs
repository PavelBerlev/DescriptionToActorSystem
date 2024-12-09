namespace ActorSystem.Actors;

using ActorSystem.Communication;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using System.Net.Mail;



public class TaskNotificatorActor : ActorBase
{
    private IMongoCollection<BsonDocument> _studentsCollection;
    private SmtpClient _client;

    public TaskNotificatorActor(IMessageSystem messageSystem, IMailBox mailBox,string ID,IMongoCollection<BsonDocument> collection, SmtpClient client) : base(messageSystem, mailBox, ID)
    {
        _studentsCollection = collection;
        _client = client;
    }

    public override async Task HandleMessage()
    {
        var msg = await _mailbox.GetMessage();



        
        // var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
        // {
        //     Credentials = new NetworkCredential("00c224c2033a04", "f3d7f7fd2e626f"),
        //     EnableSsl = true
        // };
        // //client.Send("from@example.com", "to@example.com", "Hello world", "testbody");

        // MailMessage mail = new MailMessage();
        // mail.From = new MailAddress("from@example.com");
        // mail.To.Add("from@example.com");
        // mail.Subject = "subject";
        // mail.Body = "AMAZING";
        // IList<MemoryStream> attachments = new List<MemoryStream>
        //         {
        //             new MemoryStream([0x01, 0x02, 0x03]),
        //             new MemoryStream([0x04, 0x05, 0x06])
        //         };

        // for (int i = 0; i < attachments.Count; i++)
        // {
        //     MemoryStream memoryStream = attachments[i];
        //     memoryStream.Position = 0; // Устанавливаем позицию потока в начало

        //     // Создаем вложение
        //     Attachment attachment = new Attachment(memoryStream, $"attachment{i + 1}.bin");
        //     mail.Attachments.Add(attachment);
        // }

        // client.Send(mail);
        // System.Console.WriteLine("Sent");




        var groupName = (string)msg.Context["Group"]!;


        if (!string.IsNullOrEmpty(groupName))
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Group", groupName);
            var students = await _studentsCollection.Find(filter).ToListAsync();

            if(students.Count > 0)
            {
                var mail = new MailMessage();

                mail.From = new MailAddress("from@example.com");
                mail.Subject = (string)msg.Context["TaskName"]!;
                mail.Body = (string)msg.Context["Description"]!;
                if(msg.Context.ContainsKey("Files"))
                {
                    var files = (IList<FileUploadData>)msg.Context["Files"];
                    foreach(var file in files)
                    {
                        file.FileContent.Position = 0;
                        mail.Attachments.Add(new Attachment(file.FileContent, file.FileName));
                    }
                }

                foreach (var student in students)
                {
        
                    var email = student.GetValue("Email", string.Empty).AsString;

                    if (!string.IsNullOrEmpty(email))
                    {
                        var stunetEmail = new MailAddress(email);
                        mail.To.Add(stunetEmail);
                        Console.WriteLine($"Сообщение отправлено на адрес: {email}");
                    }
                    else
                    {
                        Console.WriteLine("Email студента не найден.");
                    }
                }
            
                _client.Send(mail);
                Console.WriteLine("Sent");
            }
        }
    }
}