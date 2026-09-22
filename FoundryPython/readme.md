# Foundry agent examples

This repository contains matching Python and C# examples for Microsoft Foundry agents.

## Projects

- `FoundryPython/01 Basic` - Python Prompt Agent.
- `FoundryPython/02 - Self-Hosted-Python` - local Python self-hosted Agent Framework application.
- `FoundryPython/03-Self-Hosted-Managed-Python` - Python Managed Hosted Agent deployed by `azd`.
- `FoundryPython/03b-Self-Hosted-Managed-Python-Client` - Python Responses client for that managed agent.
- `FoundryCSharp/01-Basic-Prompt-Agent-CSharp` - C# Prompt Agent examples.
- `FoundryCSharp/03-Hosted-Agent-CSharp` - .NET 10 Managed Hosted Agent deployed by `azd`.
- `FoundryCSharp/03-Hosted-Agent-Client-CSharp` - C# Responses client for the managed C# agent.

The Python and C# managed agents use the same Foundry project and model deployment but different agent names, so they can be deployed independently:

- Python: `python-test-agent-managed`
- C#: `csharp-test-agent-managed`

Each managed-agent README contains its prerequisites, environment setup, deployment command, endpoint, and client instructions. The root `.gitignore` excludes virtual environments, .NET build output, and local `.env` files.


