#!/bin/bash
# This script will fix your 502 error once you provide Railway URL

# Usage: Replace YOUR_RAILWAY_URL with your actual URL from Railway
# Example: bash AUTO_FIX_502.sh https://decorhaven-api-production-abc.up.railway.app

RAILWAY_URL=$1

if [ -z "$RAILWAY_URL" ]; then
    echo "❌ Please provide your Railway URL"
    echo "Usage: bash AUTO_FIX_502.sh https://your-railway-url.up.railway.app"
    exit 1
fi

echo "🔧 Updating _redirects with Railway URL..."
sed -i "s|https://your-backend-api.azurewebsites.net|$RAILWAY_URL|g" _redirects

echo "🔧 Updating netlify.toml with Railway URL..."
sed -i "s|https://your-backend-api-url.com|$RAILWAY_URL|g" netlify.toml

echo "📦 Committing changes..."
git add _redirects netlify.toml
git commit -m "Fix 502 error - Connect to Railway backend at $RAILWAY_URL"

echo "🚀 Pushing to GitHub..."
git push

echo "☁️ Deploying to Netlify..."
netlify deploy --prod

echo "✅ DONE! 502 error should be fixed!"
echo "🌐 Test your site: https://decoration-hyper.netlify.app"

