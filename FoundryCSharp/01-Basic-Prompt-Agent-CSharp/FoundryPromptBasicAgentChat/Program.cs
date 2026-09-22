using Azure.Identity;
using Azure.AI.Projects;
using Azure.AI.Extensions.OpenAI;
using OpenAI.Responses;

#pragma warning disable OPENAI001

var foundryProjectEndpoint = Environment.GetEnvironmentVariable("FOUNDRY_PROJECT_ENDPOINT")
    ?? throw new InvalidOperationException("FOUNDRY_PROJECT_ENDPOINT is not set.");

var foundryAgentName = "csharp-test-agent";

var credential = new AzureCliCredential();

AIProjectClient projectClient = new(
    endpoint: new Uri(foundryProjectEndpoint),
    tokenProvider: credential);

// Create a conversation
ProjectConversation conversation =
    projectClient.ProjectOpenAIClient
        .GetProjectConversationsClient()
        .CreateProjectConversation();

Console.WriteLine($"Conversation: {conversation.Id}");
Console.WriteLine("Type 'exit' to quit.");
Console.WriteLine();

// Create a Responses client bound to our existing Foundry agent
ProjectResponsesClient responsesClient =
    projectClient.ProjectOpenAIClient
        .GetProjectResponsesClientForAgent(
            defaultAgent: foundryAgentName,
            defaultConversationId: conversation.Id);

while (true)
{
    Console.Write("You: ");

    string? message = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(message))
        continue;

    if (message.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
        message.Equals("quit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    ResponseResult response =
        responsesClient.CreateResponse(message);

    Console.WriteLine();
    Console.WriteLine($"Agent: {response.GetOutputText()}");
    Console.WriteLine();
}

