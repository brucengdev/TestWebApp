using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class FilesController: ControllerBase
{
    [HttpGet("[action]")]
    public string[] List()
    {
        return Directory.GetFiles("files");
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<string> Post()
    {
        foreach (var file in Request.Form.Files)
        {
            await SaveFile(file);
        }
        return "Ok";
    }

    private async Task SaveFile(IFormFile file)
    {
        using var fileWriter = new FileStream("files/" + file.FileName, FileMode.Create);
        var buffer = new byte[1024];
        var readStream = file.OpenReadStream();
        int readBytes = 0;
        do
        {
            readBytes = await readStream.ReadAsync(buffer);
            if (readBytes == 0)
            {
                continue;
            }

            await fileWriter.WriteAsync(buffer, 0, readBytes);
        } while (readBytes > 0);
        fileWriter.Close();
    }
}