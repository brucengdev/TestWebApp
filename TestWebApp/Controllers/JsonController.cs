using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class JsonController: ControllerBase
{
    [HttpGet]
    [Route("[action]")]
    public string[] List()
    {
        if (!Directory.Exists("json"))
        {
            return ["No json"];
        }

        return Directory.GetFiles("json");
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<string> Post()
    {
        Directory.CreateDirectory("json");
        var files = Directory.GetFiles("json/");
        var newFile = "0";
        if (files.Length > 0)
        {
            Array.Sort(files);
            newFile = (Convert.ToInt32(files.Last().Split("/")[1]) + 1).ToString();
        }

        var json = await (new StreamReader(HttpContext.Request.Body).ReadToEndAsync());
        var writer = new FileStream("json/" + newFile, FileMode.Create);
        var buffer = Encoding.UTF8.GetBytes(json);
        await writer.WriteAsync(buffer);
        writer.Close();

        return "Saved";
    }
}