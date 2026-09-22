# C# Managed Hosted Agent

This project runs a .NET 10 Microsoft Foundry Managed Hosted Agent.

## Prerequisites

Authenticate before running the script:

```powershell
az login
azd auth login
```

You also need:

- .NET 10 SDK
- Azure CLI
- Azure Developer CLI
- Access to the target Foundry project

## Get the required values

Get the active subscription ID:

```powershell
$subscriptionId = az account show --query id -o tsv
```

Set the existing Foundry resource values for your environment:

```powershell
$resourceGroup = "<resource-group>"
$accountName = "<foundry-account-name>"
$projectName = "<foundry-project-name>"
```

Find the full Foundry project ARM resource ID:

```powershell
$projectId = az resource list `
  --subscription $subscriptionId `
  --resource-group $resourceGroup `
  --query "[?type=='Microsoft.CognitiveServices/accounts/projects' && name=='$accountName/$projectName'].id | [0]" `
  -o tsv

if ([string]::IsNullOrWhiteSpace($projectId)) {
    throw "Foundry project was not found. Check the subscription, resource group, account name, and project name."
}

$projectEndpoint = "https://$($accountName.ToLower()).services.ai.azure.com/api/projects/$projectName"

Write-Host "Subscription : $subscriptionId"
Write-Host "Project ID  : $projectId"
Write-Host "Endpoint    : $projectEndpoint"
```

Confirm the endpoint in the Foundry project overview before deploying.

## Deploy with the script

The script requires exactly two parameters:

1. `-SubscriptionId`
2. `-ProjectId`

Run from this folder:

```powershell
.\deploy.ps1 `
  -SubscriptionId $subscriptionId `
  -ProjectId $projectId `
  -ProjectEndpoint $projectEndpoint `
  -Deploy
```

The script supplies defaults for the environment, location, and model deployment. Pass those as parameters if your project uses different values:

```powershell
.\deploy.ps1 `
  -EnvironmentName "<azd-environment>" `
  -SubscriptionId $subscriptionId `
  -ProjectId $projectId `
  -ProjectEndpoint $projectEndpoint `
  -Location "<azure-region>" `
  -ModelDeploymentName "<model-deployment-name>" `
  -Deploy
```

Without `-Deploy`, the script only creates or selects the azd environment and writes the configuration.

## Run the agent locally

Set the variables in the same PowerShell session. `dotnet run` does not automatically read `.azure/dev/.env`.

```powershell
$env:FOUNDRY_PROJECT_ENDPOINT = $projectEndpoint
$env:AZURE_AI_MODEL_DEPLOYMENT_NAME = "<model-deployment-name>"
$env:FOUNDRY_AGENT_NAME = "<hosted-agent-name>"

dotnet run
```

The local agent listens on:

```text
http://localhost:8088/responses
```

Press `Ctrl+C` to stop it.

