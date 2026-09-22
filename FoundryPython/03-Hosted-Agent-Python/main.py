"""Foundry-managed Hosted Agent entry point using the Responses protocol."""

import os

from agent_framework import Agent
from agent_framework.foundry import FoundryChatClient
from agent_framework_foundry_hosting import ResponsesHostServer
from azure.identity import DefaultAzureCredential
from dotenv import load_dotenv


INSTRUCTIONS = (
    "You are a helpful assistant created from Python. "
    "Use web search for current information when appropriate. "
    "Never claim to have searched unless the web-search tool actually ran. "
    "If web search is unavailable or fails, say so clearly. "
    "Include source URLs returned by web search when available."
)


def create_agent() -> Agent:
    load_dotenv()

    client = FoundryChatClient(
        project_endpoint=os.environ["FOUNDRY_PROJECT_ENDPOINT"],
        model=os.environ["AZURE_AI_MODEL_DEPLOYMENT_NAME"],
        credential=DefaultAzureCredential(),
    )

    return Agent(
        name="python-test-agent-managed",
        client=client,
        instructions=INSTRUCTIONS,
        tools=[FoundryChatClient.get_web_search_tool()],
    )


if __name__ == "__main__":
    ResponsesHostServer(create_agent()).run()
