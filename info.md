# create MCP Server
dotnet new console -n MCPServer
cd MCPServer
dotnet add package ModelContextProtocol --version 0.1.0-preview.1.25171.12
dotnet add package Microsoft.Extensions.Hosting



# code
``` cs
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
    [McpTool, Description("Searching C# local files")]
    public static string SearchCSharp(string query) {
        //get all .md files inside the folder 
        string[] markdownFiles = Directory.GetFiles(@"C:\Users\myahya\source\repos", "*.cs", SearchOption.AllDirectories);
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

```

# create mcp.json

``` json

{
    "server": {
      "name": "GreetingServer",
      "description": "A server that provides greeting messages for cities",
      "version": "1.0.0"
    },
    "tools": [
      {
        "name": "Greeting",
        "description": "Provides greeting messages",
        "commands": [
          {
            "name": "GetGreeting",
            "description": "Get a greeting message for a city",
            "parameters": [
              {
                "name": "city",
                "type": "string",
                "description": "The name of the city to greet"
              }
            ],
            "returns": {
              "type": "string",
              "description": "The greeting message"
            }
          }
        ]
      },
      {
        "name": "SearchingLocalFiles",
        "description": "Search local files",
        "commands": [
          {
            "name": "SearchMarkdown",
            "description": "find the word in local files",
            "parameters": [
              {
                "name": "query",
                "type": "string",
                "description": "The word that needs to be searched"
              }
            ],
            "returns": {
              "type": "List<string>",
              "description": "List of all files that has the searching word"
            }
          }
        ]
      },
      {
        "name": "SearchingCSharpFiles",
        "description": "Search local CSharp files",
        "commands": [
          {
            "name": "SearchCSharp",
            "description": "find the word in local CSharp files",
            "parameters": [
              {
                "name": "query",
                "type": "string",
                "description": "The word that needs to be searched"
              }
            ],
            "returns": {
              "type": "List<string>",
              "description": "List of all files that has the searching word"
            }
          }
        ]
      }
    ]
  }

```



after create a server, just test by this:
``` shell
npx @modelcontextprotocol/inspector dotnet run  
```


# run
``` cs
dotnet run

```