# Managed Hosted Python agent

This project deploys the Python Agent Framework application as a Microsoft Foundry Managed Hosted Agent.

## Current deployment

- Foundry project: `<foundry-project-name>`
- Project endpoint: `<foundry-project-endpoint>`
- Model deployment: `gpt-4.1`
- Managed agent name: `python-test-agent-managed`
- Protocol: Responses (`/responses`)

## Prerequisites

- Python 3.13 or later
- Azure CLI authenticated with `az login`
- Azure Developer CLI 1.27.1 or later
- Foundry Azure Developer CLI extensions
- Foundry Project Manager access to the target project

Install the Foundry extension if needed:

```powershell
azd extension install microsoft.foundry
```

## Configure the azd environment

Run these commands from this folder. Azure CLI and Azure Developer CLI use separate login sessions.

```powershell
az login
azd auth login
azd env select dev
$subscriptionId = az account show --query id -o tsv
azd env set "AZURE_SUBSCRIPTION_ID=$subscriptionId"
azd env set "AZURE_LOCATION=switzerlandnorth"
azd env set "FOUNDRY_PROJECT_ENDPOINT=<foundry-project-endpoint>"
azd env set "AZURE_AI_PROJECT_ID=<full-project-resource-id>"
azd env set "AZURE_AI_MODEL_DEPLOYMENT_NAME=gpt-4.1"
azd env get-values
```

`AZURE_AI_PROJECT_ID` must be the full ARM resource ID for `<foundry-project-name>`. The project endpoint and project ID refer to the existing Foundry project; do not run provisioning unless you intentionally add infrastructure configuration.

## Deploy

```powershell
azd deploy
```

The deployed Responses endpoint is:

```text
<managed-agent-responses-endpoint>
```

## Local test

```powershell
python -m venv .venv
.\.venv\Scripts\Activate.ps1
python -m pip install -r requirements.txt
Copy-Item .env.example .env
az login
python main.py
```

The local Responses endpoint listens on `http://localhost:8088/responses`.

## Tracing and correspondence

The hosted service includes `OTEL_INSTRUMENTATION_GENAI_CAPTURE_MESSAGE_CONTENT=true` in `azure.yaml`. This allows development traces to include prompt and response content when the Foundry project is connected to Application Insights and the signed-in user has the required monitoring permissions.

Without content recording, Foundry still shows successful HTTP spans, IDs, and timing, but not the actual user/assistant messages. Content capture can contain sensitive prompts, responses, and tool arguments; remove or disable it for production unless that telemetry is approved.

The external client conversation is shown immediately in the client terminal. It is not automatically mirrored into the Playground chat pane; inspect the trace after content recording is enabled.

