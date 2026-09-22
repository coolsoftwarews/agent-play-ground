# C# client for a managed Hosted Agent

This console application calls a Microsoft Foundry managed Hosted Agent through the Responses API. The hosted agent must already be deployed from `FoundryCSharp/03-Hosted-Agent-CSharp`.

## Prerequisites

- .NET 10 SDK (or the target framework configured in the project)
- Azure CLI
- Access to the Foundry project and deployed agent

Authenticate once:

```powershell
az login
```

## Configure and run

Open PowerShell in this folder and set the two values in the same terminal session:

```powershell
$env:FOUNDRY_PROJECT_ENDPOINT = "<foundry-project-endpoint>"
$env:FOUNDRY_AGENT_NAME = "<managed-agent-name>"
dotnet run
```

The project endpoint is the endpoint of the Foundry project that hosts the agent. The managed agent name is the `name` configured by the hosted agent deployment. These values can be copied from the successful deployment output or the Foundry portal.

The client creates one Responses conversation when it starts and reuses that conversation for each prompt. Enter `exit` or `quit` to stop it.

## Finding the values

Use the project endpoint shown in the Foundry portal for the target project. Use the deployed agent's exact name shown under **Build > Agents**. Do not use the model deployment name for `FOUNDRY_AGENT_NAME`.

For a local template, copy `.env.example` to `.env` as a reference, but note that this sample reads environment variables from the shell; it does not load `.env` automatically.

