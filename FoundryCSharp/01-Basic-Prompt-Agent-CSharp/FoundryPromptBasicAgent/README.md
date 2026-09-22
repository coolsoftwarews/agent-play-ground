# C# basic Prompt Agent

This sample creates a Microsoft Foundry Prompt Agent using the Azure AI Projects SDK. It configures the model, instructions, and Foundry web-search tool, then creates a new agent version.

## Prerequisites

- .NET 10 SDK
- Azure CLI
- Access to the target Foundry project

Authenticate with Azure:

```powershell
az login
```

## Configure and run

Set the project endpoint in the same PowerShell session. Use the endpoint for the Foundry project where the agent should be created:

```powershell
$env:FOUNDRY_PROJECT_ENDPOINT = "<foundry-project-endpoint>"
dotnet run
```

The sample currently creates the agent named `csharp-test-agent`. Change `foundryAgentName` in `Program.cs` if you need a different name.

This project creates or updates an agent version; it does not deploy a Hosted Agent. For the managed Hosted Agent example, see `FoundryCSharp/03-Hosted-Agent-CSharp`.

