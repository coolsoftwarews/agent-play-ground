"""Client for the deployed Foundry Managed Hosted Agent."""

import os

from azure.ai.projects import AIProjectClient
from azure.identity import AzureCliCredential
from dotenv import load_dotenv


def main() -> None:
    load_dotenv()

    project = AIProjectClient(
        endpoint=os.environ["FOUNDRY_PROJECT_ENDPOINT"],
        credential=AzureCliCredential(),
    )
    agent_name = os.getenv("FOUNDRY_AGENT_NAME", "python-test-agent-managed")

    # agent_name routes this client to the Hosted Agent's dedicated endpoint.
    openai = project.get_openai_client(agent_name=agent_name)
    conversation = openai.conversations.create()

    print(f"Calling managed agent: {agent_name}")
    print("Type 'exit' to quit.")

    while True:
        question = input("You: ").strip()
        if question.lower() in {"exit", "quit"}:
            break
        if not question:
            continue

        response = openai.responses.create(
            input=question,
            extra_body={"conversation": conversation.id},
        )
        print(f"Agent: {response.output_text}\n")


if __name__ == "__main__":
    main()
