1. py -m venv .venv
2.  .\.venv\Scripts\Activate.ps1
3. python -m pip install --upgrade pip

##install
#pip install "azure-ai-projects>=2.3.0" azure-identity

## removing and adding environment
Remove-Item -Recurse -Force .venv

python -m venv .venv
.\.venv\Scripts\python.exe -m pip install --upgrade pip
.\.venv\Scripts\python.exe -m pip install "azure-ai-projects>=2.3.0" azure-identity

## activate environment
.\.venv\Scripts\Activate.ps1
python -m pip install "azure-ai-projects>=2.3.0" azure-identit