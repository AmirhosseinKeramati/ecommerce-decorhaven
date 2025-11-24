# Fix 502 Error by Connecting Frontend to Railway Backend
# Usage: .\Fix-502-Error.ps1 "https://your-railway-url.up.railway.app"

param(
    [Parameter(Mandatory=$true)]
    [string]$RailwayUrl
)

Write-Host "🔧 Fixing 502 Error - Connecting to Railway Backend" -ForegroundColor Green
Write-Host "Backend URL: $RailwayUrl" -ForegroundColor Cyan

# Update _redirects
Write-Host "`n📝 Updating _redirects file..." -ForegroundColor Yellow
$redirectsContent = Get-Content "_redirects" -Raw
$redirectsContent = $redirectsContent -replace "https://your-backend-api.azurewebsites.net", $RailwayUrl
Set-Content "_redirects" $redirectsContent

# Update netlify.toml
Write-Host "📝 Updating netlify.toml file..." -ForegroundColor Yellow
$netlifyContent = Get-Content "netlify.toml" -Raw
$netlifyContent = $netlifyContent -replace "https://your-backend-api-url.com", $RailwayUrl
Set-Content "netlify.toml" $netlifyContent

# Commit changes
Write-Host "`n📦 Committing changes..." -ForegroundColor Yellow
git add _redirects netlify.toml
git commit -m "Fix 502 error - Connect to Railway backend at $RailwayUrl"

# Push to GitHub
Write-Host "🚀 Pushing to GitHub..." -ForegroundColor Yellow
git push

# Deploy to Netlify
Write-Host "`n☁️ Deploying to Netlify..." -ForegroundColor Yellow
netlify deploy --prod

Write-Host "`n✅ DONE! 502 Error Fixed!" -ForegroundColor Green
Write-Host "🌐 Test your site: https://decoration-hyper.netlify.app" -ForegroundColor Cyan
Write-Host "🧪 Try logging in - it should work now!" -ForegroundColor Green

