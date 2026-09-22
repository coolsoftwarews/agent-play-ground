# Client for the managed Hosted Agent

This project calls the Managed Hosted Agent deployed by
`FoundryPython/03-Self-Hosted-Managed-Python`.

## Current target

- Agent: `python-test-agent-managed`
- Project endpoint: `<foundry-project-endpoint>`
- Responses endpoint:
  `<managed-agent-responses-endpoint>`

## Run

```powershell
python -m venv .venv
.\.venv\Scripts\Activate.ps1
python -m pip install -r requirements.txt
Copy-Item .env.example .env
az login
python main.py
```

The `.env` file must contain:

```dotenv
FOUNDRY_PROJECT_ENDPOINT=<foundry-project-endpoint>
FOUNDRY_AGENT_NAME=python-test-agent-managed
```

The client uses `get_openai_client(agent_name=...)`, which routes requests to
the Hosted Agent's dedicated managed endpoint. Conversation history is kept by
the Responses API conversation ID.
