// See https://aka.ms/new-console-template for more information

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

var contentSize = 20 * 1024 * 1024;//20MB
var stringContent = GenerateString(contentSize);
var numberOfParallelThreads = 10;
Console.WriteLine("Spamming server with POST requests...");
while (!Console.KeyAvailable)
{
    var tasks = new Task[numberOfParallelThreads];
    for (int i = 0; i < numberOfParallelThreads; i++)
    {
        tasks[i] = SendAsync(stringContent, client, endpoint);
    }

    await Task.WhenAll(tasks);
}

Console.WriteLine("Stopped spamming.");

async Task SendAsync(string s, HttpClient httpClient, string endpoint1)
{
    var content = new StringContent(s);
    var result = await httpClient.PostAsync(endpoint1, content);
    Console.WriteLine("POST to /json/post, status: {0}", result.StatusCode);
}