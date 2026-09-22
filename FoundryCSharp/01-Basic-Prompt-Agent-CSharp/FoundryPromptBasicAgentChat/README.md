# C# basic Prompt Agent chat

This sample connects to a Foundry Prompt Agent and sends a multi-turn chat through the Responses API. It is intended to be run after the agent has been created by the sibling `FoundryPromptBasicAgent` sample.

## Prerequisites

- .NET 10 SDK
- Azure CLI
- An existing Prompt Agent in the target Foundry project

Authenticate with Azure:

```powershell
az login
```

## Configure and run

Set the project endpoint in the same PowerShell session:

```powershell
$env:FOUNDRY_PROJECT_ENDPOINT = "<foundry-project-endpoint>"
dotnet run
```

The sample currently calls the agent named `csharp-test-agent`. Change `foundryAgentName` in `Program.cs` if your agent has another name.

Enter messages at the `You:` prompt. Type `exit` or `quit` to stop.
