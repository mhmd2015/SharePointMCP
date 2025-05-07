using Microsoft.Extensions.Hosting;
using ModelContextProtocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.IO;

var builder = Host.CreateEmptyApplicationBuilder(settings:null);

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools();


await builder.Build().RunAsync();

[McpToolType]
public static class Greeting
{
    [McpTool, Description("Get a greeting message for a city")]
    public static string GetGreeting(string city) => $"Hello {city} from copilot chatting window";

}

//another MCPToolType for searching inside my .md files
[McpToolType]
public static class SearchingLocalFiles
{
    [McpTool, Description("Searching local files")]
    public static string SearchMarkdown(string query) {
        //get all .md files inside the folder 
        string[] markdownFiles = Directory.GetFiles(@"C:\Users\myahya\source\repos", "*.md", SearchOption.AllDirectories);
        // Process the markdown files
        List<string> searchResults = new List<string>();

        foreach (string file in markdownFiles)
        {
            string content = File.ReadAllText(file);
            if (content.Contains(query))
            {
                searchResults.Add(file);
            }
        }

        return string.Join(", ", searchResults);
    }

}