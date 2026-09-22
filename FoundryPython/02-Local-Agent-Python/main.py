"""Local/self-hosted Foundry Agent Framework example.

Unlike the Prompt Agent sample, this process owns the agent instructions,
web-search tool, and conversation session.
"""

import asyncio
import os

from agent_framework import Agent
from agent_framework.foundry import FoundryChatClient
from azure.identity import AzureCliCredential
from dotenv import load_dotenv


INSTRUCTIONS = (
    "You are a helpful assistant created from Python. "
    "Use web search for current information when appropriate. "
    "Never claim to have searched unless the web-search tool actually ran. "
    "If web search is unavailable or fails, say so clearly. "
    "Include the source URLs returned by web search when available."
)


async def main() -> None:
    load_dotenv()

    project_endpoint = os.environ["FOUNDRY_PROJECT_ENDPOINT"]
    model = os.environ["AZURE_AI_MODEL_DEPLOYMENT_NAME"]
    agent_name = os.getenv("FOUNDRY_AGENT_NAME", "python-test-agent")

    client = FoundryChatClient(
        project_endpoint=project_endpoint,
        model=model,
        credential=AzureCliCredential(),
    )

    agent = Agent(
        name=agent_name,
        client=client,
        instructions=INSTRUCTIONS,
        tools=[FoundryChatClient.get_web_search_tool()],
    )

    session = agent.create_session()
    print(f"Self-hosted agent: {agent_name}")
    print("Type 'exit' to quit.")

    while True:
        question = input("You: ").strip()
        if question.lower() in {"exit", "quit"}:
            break
        if not question:
            continue

        result = await agent.run(question, session=session)
        print(f"Agent: {result.text}\n")


if __name__ == "__main__":
    asyncio.run(main())
