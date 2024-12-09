namespace ActorSystem.Actors;

public class FileUploadData
{
    public string FileName { get; set; }
    public MemoryStream FileContent { get; set; }

    public FileUploadData(string fileName, MemoryStream fileContent)
    {
        FileName = fileName;
        FileContent = fileContent;
    }
}