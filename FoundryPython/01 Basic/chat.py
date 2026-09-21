from azure.identity import AzureCliCredential
from azure.ai.projects import AIProjectClient


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

# Get an OpenAI client bound specifically to our Foundry agent
openai = project_client.get_openai_client(
    agent_name=foundry_agent_name
)

# Create a conversation so the agent remembers previous messages
conversation = openai.conversations.create()

print(f"Conversation: {conversation.id}")
print("Type 'exit' to quit.")
print()

while True:

    message = input("You: ")

    if message.lower() in ["exit", "quit"]:
        break

    response = openai.responses.create(
        conversation=conversation.id,
        input=message
    )

    print()
    print(f"Agent: {response.output_text}")
    print()