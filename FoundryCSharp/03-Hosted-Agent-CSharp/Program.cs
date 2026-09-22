using Azure.AI.AgentServer.Core;
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Foundry.Hosting;
using Microsoft.Extensions.AI;

var projectEndpoint = new Uri(
    Environment.GetEnvironmentVariable("FOUNDRY_PROJECT_ENDPOINT")
    ?? throw new InvalidOperationException("FOUNDRY_PROJECT_ENDPOINT is not set."));
var deployment = Environment.GetEnvironmentVariable("AZURE_AI_MODEL_DEPLOYMENT_NAME") ?? "gpt-4.1";
var agentName = Environment.GetEnvironmentVariable("FOUNDRY_AGENT_NAME") ?? "csharp-test-agent-managed";

AIAgent agent = new AIProjectClient(projectEndpoint, new DefaultAzureCredential())
    .AsAIAgent(
        model: deployment,
        instructions:
            "You are a helpful assistant created from C#. " +
            "Use web search for current information when appropriate. " +
            "Never claim to have searched unless the web-search tool actually ran. " +
            "If web search is unavailable or fails, say so clearly. " +
            "Include source URLs returned by web search when available.",
        name: agentName,
        tools: [new HostedWebSearchTool()]);

var builder = AgentHost.CreateBuilder(args);
builder.Services.AddFoundryResponses(agent);
builder.RegisterProtocol("responses", endpoints => endpoints.MapFoundryResponses());
var app = builder.Build();
app.Run();


