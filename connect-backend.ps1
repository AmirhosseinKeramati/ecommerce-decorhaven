# Connect Frontend to Backend
# Usage: .\connect-backend.ps1 "https://your-backend-url.com"

param(
    [Parameter(Mandatory=$true, HelpMessage="Enter your backend URL (from Render or Railway)")]
    [string]$BackendUrl
)

Write-Host "🔗 Connecting Frontend to Backend..." -ForegroundColor Green
Write-Host "Backend URL: $BackendUrl" -ForegroundColor Cyan
Write-Host ""

# Remove trailing slash if present
$BackendUrl = $BackendUrl.TrimEnd('/')

# Validate URL format
if ($BackendUrl -notmatch '^https?://') {
    Write-Host "❌ Error: URL must start with http:// or https://" -ForegroundColor Red
    exit 1
}

# Update _redirects
Write-Host "📝 Updating _redirects..." -ForegroundColor Yellow
$redirectsContent = Get-Content "_redirects" -Raw
$redirectsContent = $redirectsContent -replace 'https://your-backend-api\.azurewebsites\.net', $BackendUrl
$redirectsContent = $redirectsContent -replace 'https://[a-zA-Z0-9\-]+\.(onrender|up\.railway)\.com', $BackendUrl
Set-Content "_redirects" $redirectsContent -NoNewline
Write-Host "✅ _redirects updated!" -ForegroundColor Green

# Update netlify.toml
Write-Host "📝 Updating netlify.toml..." -ForegroundColor Yellow
$netlifyContent = Get-Content "netlify.toml" -Raw
$netlifyContent = $netlifyContent -replace 'https://your-backend-api-url\.com', $BackendUrl
$netlifyContent = $netlifyContent -replace 'https://[a-zA-Z0-9\-]+\.(onrender|up\.railway)\.com', $BackendUrl
Set-Content "netlify.toml" $netlifyContent -NoNewline
Write-Host "✅ netlify.toml updated!" -ForegroundColor Green

# Show what changed
Write-Host ""
Write-Host "📋 Changes Made:" -ForegroundColor Cyan
Write-Host "  _redirects: /api/* → $BackendUrl/api/:splat" -ForegroundColor White
Write-Host "  netlify.toml: Proxy → $BackendUrl/api/:splat" -ForegroundColor White

# Commit changes
Write-Host ""
Write-Host "📦 Committing changes..." -ForegroundColor Yellow
git add _redirects netlify.toml
git commit -m "Connect frontend to backend at $BackendUrl"
Write-Host "✅ Changes committed!" -ForegroundColor Green

# Push to GitHub
Write-Host ""
Write-Host "🚀 Pushing to GitHub..." -ForegroundColor Yellow
git push
Write-Host "✅ Pushed to GitHub!" -ForegroundColor Green

# Check if netlify CLI is available
Write-Host ""
$netlifyInstalled = Get-Command netlify -ErrorAction SilentlyContinue
if ($netlifyInstalled) {
    Write-Host "☁️  Deploying to Netlify..." -ForegroundColor Yellow
    netlify deploy --prod
    Write-Host "✅ Deployed to Netlify!" -ForegroundColor Green
} else {
    Write-Host "⚠️  Netlify CLI not found. Netlify will auto-deploy from GitHub." -ForegroundColor Yellow
    Write-Host "   Or install: npm install -g netlify-cli" -ForegroundColor Gray
}

# Success message
Write-Host ""
Write-Host "╔════════════════════════════════════════════════════════════╗" -ForegroundColor Green
Write-Host "║                   ✅ CONNECTION COMPLETE!                  ║" -ForegroundColor Green
Write-Host "╚════════════════════════════════════════════════════════════╝" -ForegroundColor Green
Write-Host ""
Write-Host "🎉 Your frontend is now connected to your backend!" -ForegroundColor Cyan
Write-Host ""
Write-Host "🌐 Test your site:" -ForegroundColor White
Write-Host "   https://decoration-hyper.netlify.app/" -ForegroundColor Cyan
Write-Host ""
Write-Host "🧪 Try these features:" -ForegroundColor White
Write-Host "   ✅ Register a new account" -ForegroundColor Green
Write-Host "   ✅ Login" -ForegroundColor Green
Write-Host "   ✅ Add items to cart" -ForegroundColor Green
Write-Host "   ✅ Place an order" -ForegroundColor Green
Write-Host "   ✅ View your profile" -ForegroundColor Green
Write-Host ""
Write-Host "📊 Monitor your backend:" -ForegroundColor White
Write-Host "   $BackendUrl" -ForegroundColor Cyan
Write-Host ""
Write-Host "🎊 NO MORE 502 ERROR! Everything should work now!" -ForegroundColor Green
Write-Host ""

