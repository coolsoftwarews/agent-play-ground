using Azure.Identity;
using Azure.AI.Projects;
using Azure.AI.Projects.Agents;
using Azure.AI.Extensions.OpenAI;
using OpenAI.Responses;
using Azure.Core.Diagnostics;
using System.Diagnostics.Tracing;

var foundryProjectEndpoint = Environment.GetEnvironmentVariable("FOUNDRY_PROJECT_ENDPOINT")
    ?? throw new InvalidOperationException("FOUNDRY_PROJECT_ENDPOINT is not set.");
var foundryAgentName = "csharp-test-agent";


// using AzureEventSourceListener listener =
//     AzureEventSourceListener.CreateConsoleLogger(EventLevel.Informational);
    
var credential = new AzureCliCredential(); //var credential = new DefaultAzureCredential(); 

AIProjectClient projectClient = new(
    endpoint: new Uri(foundryProjectEndpoint),
    tokenProvider: credential);

#pragma warning disable OPENAI001

ProjectsAgentDefinition agentDefinition =
    new DeclarativeAgentDefinition("gpt-4.1")
    {
        Instructions =
            "You are a helpful assistant created from C#. " +
            "Use web search for current information. " +
            "Never claim to have searched unless the tool actually ran.",

        Tools =
        {
            ResponseTool.CreateWebSearchTool()
        }
    };

#pragma warning restore OPENAI001

ProjectsAgentVersion agent =
    projectClient.AgentAdministrationClient.CreateAgentVersion(
        foundryAgentName,
        options: new(agentDefinition));

Console.WriteLine(
    $"Agent created (id: {agent.Id}, name: {agent.Name}, version: {agent.Version})");

