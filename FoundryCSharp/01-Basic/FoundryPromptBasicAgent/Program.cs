using Azure.Identity;
using Azure.AI.Projects;
using Azure.AI.Projects.Agents;
using Azure.AI.Extensions.OpenAI;
using Azure.Core.Diagnostics;
using System.Diagnostics.Tracing;

var foundryProjectEndpoint = "https://cswstestplayground.services.ai.azure.com/api/projects/proj-default";
var foundryAgentName = "csharp-test-agent";


// using AzureEventSourceListener listener =
//     AzureEventSourceListener.CreateConsoleLogger(EventLevel.Informational);
    
var credential = new AzureCliCredential(); //var credential = new DefaultAzureCredential(); 

AIProjectClient projectClient = new(
    endpoint: new Uri(foundryProjectEndpoint),
    tokenProvider: credential);

ProjectsAgentDefinition agentDefinition =
    new DeclarativeAgentDefinition("gpt-4.1")
    {
        Instructions = "You are a helpful assistant created from C#."
    };

ProjectsAgentVersion agent =
    projectClient.AgentAdministrationClient.CreateAgentVersion(
        foundryAgentName,
        options: new(agentDefinition));

Console.WriteLine(
    $"Agent created (id: {agent.Id}, name: {agent.Name}, version: {agent.Version})");