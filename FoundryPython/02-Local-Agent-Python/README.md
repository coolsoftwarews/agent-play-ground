# Self-hosted Python agent

This sample mirrors the existing `python-test-agent` configuration but runs
the agent in this Python process instead of using a server-side Prompt Agent.
The process owns the instructions, web-search tool, and conversation session.

## Run locally (PowerShell)

```powershell
cd "FoundryPython\02 - Self Hosted Python"
python -m venv .venv
.\.venv\Scripts\Activate.ps1
python -m pip install --upgrade pip
python -m pip install -r requirements.txt
Copy-Item .env.example .env
az login
python main.py
```

The `.env` file is local configuration and is ignored by the repository root
`.gitignore`.

## Important distinction

This is a self-hosted application: it uses the Foundry project model endpoint,
but it is not the existing server-side Prompt Agent version. To deploy this
application as a Microsoft-managed Foundry Hosted Agent, add the Foundry
Responses hosting package and an `azure.yaml` deployment project.
