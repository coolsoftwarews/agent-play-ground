# Python basic Prompt Agent

This folder contains two small Python examples using the Azure AI Projects SDK:

- `main.py` creates a Prompt Agent with instructions and the Foundry web-search tool.
- `chat.py` connects to the `python-test-agent` agent and runs an interactive Responses conversation.

## Prerequisites

- Python 3.10 or later
- Azure CLI
- Access to the target Foundry project

Install the SDK packages in an activated virtual environment:

```powershell
python -m venv .venv
.\.venv\Scripts\Activate.ps1
python -m pip install --upgrade pip
python -m pip install "azure-ai-projects>=2.3.0" azure-identity
az login
```

## Create the Prompt Agent

Set the project endpoint in the same PowerShell session, then run:

```powershell
$env:FOUNDRY_PROJECT_ENDPOINT = "<foundry-project-endpoint>"
python main.py
```

The sample creates the agent named `python-test-agent`. Change `foundry_agent_name` in `main.py` if needed.

## Chat with the agent

After the agent exists, run:

```powershell
$env:FOUNDRY_PROJECT_ENDPOINT = "<foundry-project-endpoint>"
python chat.py
```

Type messages at the `You:` prompt. Type `exit` or `quit` to stop.

The endpoint is intentionally supplied through an environment variable so the project URL is not stored in source code.
