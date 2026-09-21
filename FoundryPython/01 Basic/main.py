##install
#pip install "azure-ai-projects>=2.3.0" azure-identity

from azure.identity import AzureCliCredential
from azure.ai.projects import AIProjectClient
from azure.ai.projects.models import PromptAgentDefinition


foundry_project_endpoint = (
    "https://cswstestplayground.services.ai.azure.com/"
    "api/projects/proj-default"
)

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
        "If asked who you are, answer exactly: "
        "'I am a helpful assistant created from Python.'"
    )
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