##install
#pip install "azure-ai-projects>=2.3.0" azure-identity

import os

from azure.identity import AzureCliCredential
from azure.ai.projects import AIProjectClient
from azure.ai.projects.models import (
    PromptAgentDefinition,
    WebSearchTool,
)


foundry_project_endpoint = os.environ["FOUNDRY_PROJECT_ENDPOINT"]

foundry_agent_name = "python-test-agent"

credential = AzureCliCredential()

project_client = AIProjectClient(
    endpoint=foundry_project_endpoint,
    credential=credential
)

agent_definition = PromptAgentDefinition(
    model="gpt-4.1",
    instructions=(
        "You are a helpful assistant created from Python. "
        "Use web search for current information. "
        "Never claim to have searched unless the web-search tool actually ran. "
        "If search fails or is unavailable, say so clearly."
    ),
    tools=[
        WebSearchTool()
    ],
)

agent = project_client.agents.create_version(
    agent_name=foundry_agent_name,
    definition=agent_definition
)

print(
    f"Agent created "
    f"(id: {agent.id}, "
    f"name: {agent.name}, "
    f"version: {agent.version})"
)

