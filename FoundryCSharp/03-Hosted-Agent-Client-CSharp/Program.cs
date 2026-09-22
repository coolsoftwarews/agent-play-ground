using Azure.AI.Extensions.OpenAI;
using Azure.AI.Projects;
using Azure.Identity;
using OpenAI.Responses;

#pragma warning disable OPENAI001
#pragma warning disable SCME0001

var endpoint = Environment.GetEnvironmentVariable("FOUNDRY_PROJECT_ENDPOINT")
    ?? throw new InvalidOperationException("FOUNDRY_PROJECT_ENDPOINT is not set.");
var agentName = Environment.GetEnvironmentVariable("FOUNDRY_AGENT_NAME")
    ?? throw new InvalidOperationException("FOUNDRY_AGENT_NAME is not set.");

var project = new AIProjectClient(new Uri(endpoint), new AzureCliCredential());
var responses = project.ProjectOpenAIClient.GetProjectResponsesClientForAgentEndpoint(agentName);
string? agentConversationId = null;

Console.WriteLine($"Calling managed agent: {agentName}");
Console.WriteLine("Type 'exit' to quit.");

while (true)
{
    Console.Write("You: ");
    var question = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(question))
        continue;
    if (question.Equals("exit", StringComparison.OrdinalIgnoreCase) || question.Equals("quit", StringComparison.OrdinalIgnoreCase))
        break;

    var options = new CreateResponseOptions
    {
        InputItems = { ResponseItem.CreateUserMessageItem(question) }
    };

    if (agentConversationId is not null)
        options.AgentConversationId = agentConversationId;

    var response = await responses.CreateResponseAsync(options);
    agentConversationId = response.Value.AgentConversationId;
    Console.WriteLine($"Agent: {response.Value.GetOutputText()}");
    Console.WriteLine();
}

#pragma warning restore SCME0001
#pragma warning restore OPENAI001
