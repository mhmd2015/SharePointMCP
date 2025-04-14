# SharePoint MCP: The .NET MCP Server with Graph API & Semantic Kernel
In the modern enterprise, accessing specific data securely and efficiently is crucial, especially when integrating with AI capabilities. Imagine a chatbot that can not only converse intelligently but also fetch relevant documents or information directly from your organization's SharePoint sites. This post outlines a modular architecture using .NET, Microsoft Graph API, Semantic Kernel, and Gemini AI to achieve just that.

This project composed of three distinct modules:

1. The MCP Server (Semantic Kernel): The brain of our SharePoint interaction.
2. The SharePoint Connector Library: The hands reaching into SharePoint Online.
3. The MCP Client (Gemini AI): The user-facing chat interface.

Let's break down each module:

## Module 1: The .NET MCP Server (Leveraging Semantic Kernel)

This core component acts as the central hub and API endpoint. Built using .NET, we're incorporating Semantic Kernel here. While it could be a standard ASP.NET Core Web API, using Semantic Kernel allows us to potentially define the SharePoint interaction as a native "skill" or "plugin".

Expose functionality (like SharePoint search) via an API endpoint that the client can call. Orchestrate calls to the Connector Library, using .NET (e.g., ASP.NET Core Web API), Semantic Kernel SDK.
Key Function: Define an API route (e.g., /api/sharepoint/search) that accepts search queries. This route will invoke the function within our Connector Library. Using Semantic Kernel allows this function to be easily integrated into more complex AI reasoning flows if needed later.


## Module 2: SharePoint Connector Library (Graph API)

This .NET library is solely responsible for the nitty-gritty details of communicating with SharePoint Online.

Purpose: Authenticate with Microsoft Graph and execute specific SharePoint actions, starting with search.
Technology: .NET Standard or .NET Core Library, Microsoft.Graph SDK.
Authentication: Uses Client ID and Client Secret (App-Only Authentication).
Setup: Requires registering an application in Azure Active Directory (Azure AD), granting it the necessary Graph API permissions (e.g., Sites.Read.All or more granular permissions depending on your needs), and obtaining admin consent. Crucially, store your Client Secret securely (e.g., Azure Key Vault, .NET User Secrets) – never hardcode it!
Key Function: An asynchronous function (e.g., SearchSharePointAsync(string query)) that:
Uses the Client ID and Secret to obtain an access token for Microsoft Graph.
Constructs a Graph API request to the SharePoint search endpoint (/search/query).
Sends the request using the Microsoft.Graph SDK.
Parses the response and returns the relevant search results (e.g., document names, links, snippets).


## Module 3: The MCP Client (Gemini AI)

This is the application your users interact with. It features a chat interface powered by Gemini AI.

Purpose: Provide an AI-driven conversational experience. Detect user intent related to SharePoint and delegate those tasks to the MCP Server.
Technology: Any client platform (.NET MAUI, Blazor, Console App, etc.), Google AI Gemini SDK.
Functionality:
User chats with the Gemini-powered interface.
The client application (or potentially Gemini itself, depending on implementation) analyzes the conversation.
When keywords or intent related to "SharePoint" (or specific document types, sites, etc.) are detected, the client makes an HTTP request to the MCP Server's API endpoint (e.g., /api/sharepoint/search) with the relevant query.
It receives the search results from the server and presents them back to the user within the chat context.
Workflow Example:

User: "Hey Gemini, can you find the Q1 marketing report on SharePoint?"
MCP Client: Detects "SharePoint" and "Q1 marketing report".
MCP Client: Sends POST /api/sharepoint/search with query "Q1 marketing report" to the MCP Server.
MCP Server: Receives request, calls SearchSharePointAsync("Q1 marketing report") in the Connector Library.
Connector Library: Authenticates with Graph API using Client ID/Secret, performs the search /search/query.
SharePoint Online: Returns search results.
Connector Library: Returns formatted results to the MCP Server.
MCP Server: Returns results in the API response to the MCP Client.
MCP Client: Displays the results (e.g., "I found these documents: [Link 1], [Link 2]") within the Gemini chat interface.
Benefits:

Modular Design: Clear separation of concerns makes development and maintenance easier.
Secure Access: App-only authentication ensures controlled access to SharePoint data without user impersonation in the backend.
AI Integration: Seamlessly blends conversational AI with enterprise data retrieval.
Extensible: Easily add more SharePoint functions (uploading, listing files, etc.) to the Connector Library and expose them via the MCP Server.
