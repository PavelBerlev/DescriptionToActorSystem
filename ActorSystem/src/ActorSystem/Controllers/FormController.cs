using ActorSystem;
using ActorSystem.Actors;
using ActorSystem.Communication;
using Microsoft.AspNetCore.Mvc;



[Route("api/[controller]")]
[ApiController]
public class FormsController : ControllerBase
{
    private readonly ActorSystemMVP _actorSystem;
    public FormsController(ActorSystemMVP actorSystem)
    {
        _actorSystem = actorSystem;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadForm([FromForm] string title, [FromForm] string group, [FromForm] string description, [FromForm] IFormFile[] files)
    {
        var message = new Message();
        message.Sender = "WebForm";
        message.Receiver = "Send";
        message.Context["Group"] = group;
        message.Context["TaskName"] = title;
        message.Context["Description"] = description;
        if (files != null && files.Length > 0)
        {
            IList<FileUploadData> fileUploadDatas = new List<FileUploadData>();
            foreach(var file in files)
            {
                var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var fileData = new FileUploadData(file.FileName, memoryStream);
                fileUploadDatas.Add(fileData);
            }
            message.Context["Files"] = fileUploadDatas;
        }
        _actorSystem.Send(message);
        return Ok("Форма отправленна в акторную модель");
    }
}
