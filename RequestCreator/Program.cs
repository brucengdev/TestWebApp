// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;
using System.Text;

var client = new HttpClient();
var endpoint = "http://172.29.162.25:5068/json/post";

string GenerateString(int count)
{
    var sb = new StringBuilder("{\"val\":\"");
    for (int i = 0; i < count; i++)
    {
        sb.Append("a");
    }

    sb.Append("\"}");
    return sb.ToString();
}

var stringContent = GenerateString(10000);
Console.WriteLine("Spamming server with POST requests...");
while (!Console.KeyAvailable)
{
    var content = new StringContent(stringContent);
    var result = await client.PostAsync(endpoint, content);
    Console.WriteLine("POST to /json/post, status: {0}", result.StatusCode);
}

Console.WriteLine("Stopped spamming.");