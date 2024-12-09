using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace SpaApp.Models;
public class FormModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Title { get; set; }
    public string Group { get; set; }
    public string Description { get; set; }
    public List<string> FileIds { get; set; }
}
