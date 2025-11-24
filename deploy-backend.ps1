# Backend Deployment Script for Azure

Write-Host "🚀 Deploying Backend to Azure..." -ForegroundColor Green

# Configuration
$resourceGroup = "decorhaven-rg"
$appName = "decorhaven-api"
$location = "eastus"
$sqlServer = "decorhaven-sql"
$sqlDb = "DecorHavenDB"
$sqlAdmin = "sqladmin"

Write-Host "📦 Step 1: Publishing .NET Application..." -ForegroundColor Cyan
cd DecorHaven.API
dotnet publish -c Release -o ./publish

Write-Host "📤 Step 2: Creating ZIP package..." -ForegroundColor Cyan
Compress-Archive -Path ./publish/* -DestinationPath ./app.zip -Force

Write-Host "☁️ Step 3: Deploying to Azure..." -ForegroundColor Cyan
Write-Host "You need to run these Azure CLI commands:" -ForegroundColor Yellow
Write-Host ""
Write-Host "# 1. Login to Azure" -ForegroundColor White
Write-Host "az login" -ForegroundColor Gray
Write-Host ""
Write-Host "# 2. Create Resource Group (if not exists)" -ForegroundColor White
Write-Host "az group create --name $resourceGroup --location $location" -ForegroundColor Gray
Write-Host ""
Write-Host "# 3. Create App Service Plan" -ForegroundColor White
Write-Host "az appservice plan create --name decorhaven-plan --resource-group $resourceGroup --sku B1 --is-linux" -ForegroundColor Gray
Write-Host ""
Write-Host "# 4. Create Web App" -ForegroundColor White
Write-Host "az webapp create --resource-group $resourceGroup --plan decorhaven-plan --name $appName --runtime `"DOTNETCORE:8.0`"" -ForegroundColor Gray
Write-Host ""
Write-Host "# 5. Deploy ZIP" -ForegroundColor White
Write-Host "az webapp deploy --resource-group $resourceGroup --name $appName --src-path ./app.zip --type zip" -ForegroundColor Gray
Write-Host ""
Write-Host "✅ Backend will be available at: https://$appName.azurewebsites.net" -ForegroundColor Green

cd ..

