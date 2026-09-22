[CmdletBinding()]
param(
    [string]$EnvironmentName = "dev",
    [Parameter(Mandatory = $true)] [string]$SubscriptionId,
    [Parameter(Mandatory = $true)] [string]$ProjectId,
    [Parameter(Mandatory = $true)] [string]$ProjectEndpoint,
    [string]$Location = "switzerlandnorth",
    [string]$ModelDeploymentName = "gpt-4.1",
    [switch]$Deploy
)

$ErrorActionPreference = "Stop"

if (-not (Get-Command azd -ErrorAction SilentlyContinue)) { throw "Azure Developer CLI (azd) is not installed or is not on PATH." }
if (-not (Get-Command az -ErrorAction SilentlyContinue)) { throw "Azure CLI (az) is not installed or is not on PATH." }
if ($ProjectId -notmatch '^/subscriptions/[^/]+/.+/projects/[^/]+$') { throw "ProjectId must be the complete ARM resource ID ending in /projects/<project-name>." }
$parsedEndpoint = $null
if (-not [Uri]::TryCreate($ProjectEndpoint, [UriKind]::Absolute, [ref]$parsedEndpoint) -or $parsedEndpoint.Scheme -ne "https") { throw "ProjectEndpoint must be an absolute HTTPS URL." }

function Invoke-Azd {
    param([Parameter(Mandatory = $true)][string[]]$Arguments)
    $previousUserAgent = $env:AZURE_DEV_USER_AGENT
    try {
        $env:AZURE_DEV_USER_AGENT = "microsoft_foundry_skill"
        & azd @Arguments
        if ($LASTEXITCODE -ne 0) { throw "azd $($Arguments -join ' ') failed with exit code $LASTEXITCODE." }
    }
    finally {
        if ($null -eq $previousUserAgent) { Remove-Item Env:AZURE_DEV_USER_AGENT -ErrorAction SilentlyContinue }
        else { $env:AZURE_DEV_USER_AGENT = $previousUserAgent }
    }
}

Write-Host "Selecting azd environment '$EnvironmentName'..."
try {
    Invoke-Azd @("env", "select", $EnvironmentName)
}
catch {
    Write-Host "Environment '$EnvironmentName' was not found in this project; creating it..."
    Invoke-Azd @("env", "new", $EnvironmentName)
}
Invoke-Azd @("env", "set", "AZURE_SUBSCRIPTION_ID=$SubscriptionId")
Invoke-Azd @("env", "set", "AZURE_LOCATION=$Location")
Invoke-Azd @("env", "set", "FOUNDRY_PROJECT_ENDPOINT=$ProjectEndpoint")
Invoke-Azd @("env", "set", "AZURE_AI_PROJECT_ID=$ProjectId")
Invoke-Azd @("env", "set", "AZURE_AI_MODEL_DEPLOYMENT_NAME=$ModelDeploymentName")

Write-Host "Configured azd environment values:"
Invoke-Azd @("env", "get-values")

if ($Deploy) {
    Write-Host "Deploying csharp-test-agent-managed..."
    Invoke-Azd @("deploy", "--no-prompt")
}
else {
    Write-Host "Configuration complete. Re-run with -Deploy to deploy the agent."
}

