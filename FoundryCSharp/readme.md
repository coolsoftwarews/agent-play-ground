# Foundry C# examples

These projects target .NET 10 and use the Azure AI Projects and Microsoft Agent Framework packages.

## Projects

- `01-Basic-Prompt-Agent-CSharp` - Prompt Agent creation and chat examples.
- `03-Hosted-Agent-CSharp` - Microsoft Agent Framework agent exposed through the Foundry Responses protocol and deployed as a Managed Hosted Agent.
- `03-Hosted-Agent-Client-CSharp` - console client that calls `csharp-test-agent-managed` through the project Responses endpoint and keeps a Responses conversation ID across turns.

The hosted project includes provider-side web search through `HostedWebSearchTool`; like the Python version, search is performed by Foundry rather than by local code.

The hosted project uses the current Microsoft hosting integration:

- `Azure.AI.Projects` prerelease
- `Azure.Identity`
- `Microsoft.Agents.AI`
- `Microsoft.Agents.AI.Foundry.Hosting` prerelease

The client uses `Azure.AI.Extensions.OpenAI` prerelease and `Azure.AI.Projects` to obtain a `ProjectResponsesClient` for the managed agent endpoint.




