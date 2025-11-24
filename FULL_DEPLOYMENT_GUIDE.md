# 🚀 Complete Frontend & Backend Deployment Guide

## Deploy Your E-Commerce App to Production

---

## 📋 Overview

**Frontend** → Netlify (https://decoration-hyper.netlify.app)  
**Backend** → Azure App Service (ASP.NET Core)  
**Database** → Azure SQL Database  

---

## 🎯 Part 1: Deploy Frontend to Netlify

### **Step 1: Login to Netlify**

```bash
netlify login
# Browser will open - authorize the CLI
```

### **Step 2: Link to Your Existing Site**

```bash
netlify link
# Choose: "Enter a site ID"
# Enter: decoration-hyper
```

### **Step 3: Deploy Frontend**

```bash
netlify deploy --prod
# When asked for publish directory: press Enter (uses current directory)
```

**Your frontend will be live at:**  
🌐 https://decoration-hyper.netlify.app

---

## 🔧 Part 2: Deploy Backend to Azure

### **Option A: Deploy via Azure CLI (Recommended)**

#### **Install Azure CLI:**

```bash
# Download from: https://aka.ms/installazurecliwindows
# Or use winget:
winget install Microsoft.AzureCLI
```

#### **Login to Azure:**

```bash
az login
```

#### **Create Resource Group:**

```bash
az group create --name decorhaven-rg --location eastus
```

#### **Create SQL Database:**

```bash
# Create SQL Server
az sql server create \
  --name decorhaven-sql \
  --resource-group decorhaven-rg \
  --location eastus \
  --admin-user sqladmin \
  --admin-password YourSecurePassword123!

# Create Database
az sql db create \
  --resource-group decorhaven-rg \
  --server decorhaven-sql \
  --name DecorHavenDB \
  --service-objective S0

# Configure firewall (allow Azure services)
az sql server firewall-rule create \
  --resource-group decorhaven-rg \
  --server decorhaven-sql \
  --name AllowAzure \
  --start-ip-address 0.0.0.0 \
  --end-ip-address 0.0.0.0
```

#### **Create App Service:**

```bash
# Create App Service Plan
az appservice plan create \
  --name decorhaven-plan \
  --resource-group decorhaven-rg \
  --sku B1 \
  --is-linux

# Create Web App
az webapp create \
  --resource-group decorhaven-rg \
  --plan decorhaven-plan \
  --name decorhaven-api \
  --runtime "DOTNETCORE:8.0"
```

#### **Configure Connection String:**

```bash
# Get connection string
$connectionString = "Server=tcp:decorhaven-sql.database.windows.net,1433;Database=DecorHavenDB;User ID=sqladmin;Password=YourSecurePassword123!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# Set connection string
az webapp config connection-string set \
  --resource-group decorhaven-rg \
  --name decorhaven-api \
  --connection-string-type SQLAzure \
  --settings DefaultConnection="$connectionString"
```

#### **Deploy Backend:**

```bash
# Navigate to backend
cd DecorHaven.API

# Publish
dotnet publish -c Release

# Deploy to Azure
az webapp deploy \
  --resource-group decorhaven-rg \
  --name decorhaven-api \
  --src-path ./bin/Release/net8.0/publish \
  --type zip
```

**Your backend will be at:**  
🌐 https://decorhaven-api.azurewebsites.net

---

### **Option B: Deploy via Visual Studio**

1. **Open Solution:**
   - Open `e-commerce.sln` in Visual Studio

2. **Right-click on DecorHaven.API project**
   - Select **Publish**

3. **Choose Target:**
   - Select **Azure**
   - Click **Next**

4. **Select Azure App Service:**
   - Choose **Azure App Service (Windows)** or **Linux**
   - Click **Next**

5. **Create New or Select Existing:**
   - Click **+ Create New**
   - Fill in details:
     - Name: `decorhaven-api`
     - Resource Group: Create new or select existing
     - Hosting Plan: Create new (B1 or higher)
   - Click **Create**

6. **Configure SQL Database:**
   - In Azure Portal, create SQL Database
   - Copy connection string
   - Add to App Service → Configuration → Connection strings

7. **Publish:**
   - Click **Publish** button
   - Wait for deployment
   - Done! ✅

---

### **Option C: Deploy via Azure Portal**

1. **Go to:** https://portal.azure.com

2. **Create App Service:**
   - Click **Create a resource**
   - Search for **Web App**
   - Fill in:
     - Name: `decorhaven-api`
     - Runtime: **.NET 8**
     - Region: Choose nearest
     - Pricing: B1 or higher
   - Click **Review + Create**

3. **Create SQL Database:**
   - Create a resource → **SQL Database**
   - Fill in details
   - Configure firewall rules
   - Get connection string

4. **Configure App:**
   - Go to your App Service
   - Configuration → Connection strings
   - Add your SQL connection string

5. **Deploy Code:**
   - Use Deployment Center
   - Connect to GitHub
   - Or use FTP/ZIP deploy

---

## 🔗 Part 3: Connect Frontend to Backend

### **Update Netlify Configuration**

#### **1. Edit `netlify.toml`:**

```toml
[[redirects]]
  from = "/api/*"
  to = "https://decorhaven-api.azurewebsites.net/api/:splat"
  status = 200
  force = true
  headers = {X-From = "Netlify"}
```

#### **2. Edit `_redirects`:**

```
/api/*  https://decorhaven-api.azurewebsites.net/api/:splat  200
```

#### **3. Update Backend CORS:**

Edit `DecorHaven.API/Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5500",
                "https://decoration-hyper.netlify.app",
                "https://yourdomain.com"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

app.UseCors("AllowFrontend");
```

#### **4. Redeploy Both:**

```bash
# Redeploy backend
cd DecorHaven.API
dotnet publish -c Release
az webapp deploy --resource-group decorhaven-rg --name decorhaven-api --src-path ./bin/Release/net8.0/publish --type zip

# Redeploy frontend
cd ..
git add .
git commit -m "Update API URL"
netlify deploy --prod
```

---

## 🧪 Part 4: Test Full Stack

### **1. Test Backend API:**

```bash
# Test API is running
curl https://decorhaven-api.azurewebsites.net/api/products

# Or visit in browser:
https://decorhaven-api.azurewebsites.net/api/products
```

### **2. Test Frontend:**

Visit: https://decoration-hyper.netlify.app

**Open Console (F12) and check:**
- ✅ Environment: Production
- ✅ API Base URL: /api
- ✅ Backend connected

**Test features:**
- ✅ Products load from backend
- ✅ Can register/login
- ✅ Can add to cart (syncs with backend)
- ✅ Can place orders
- ✅ Orders save to database

---

## 📊 Part 5: Database Migrations

### **Apply Migrations to Azure SQL:**

```bash
# Update connection string in appsettings.json temporarily
# Or set environment variable:
$env:ConnectionStrings__DefaultConnection = "Server=tcp:decorhaven-sql.database.windows.net,1433;Database=DecorHavenDB;User ID=sqladmin;Password=YourSecurePassword123!;..."

# Apply migrations
cd DecorHaven.API
dotnet ef database update

# Or include in startup code (already done in Program.cs)
# context.Database.Migrate() runs on app start
```

---

## 🔐 Part 6: Environment Variables

### **Set in Netlify:**

1. Go to: Site settings → Environment variables
2. Add:
   ```
   API_URL=https://decorhaven-api.azurewebsites.net/api
   ENVIRONMENT=production
   ```

### **Set in Azure App Service:**

1. Go to: Configuration → Application settings
2. Add:
   ```
   ASPNETCORE_ENVIRONMENT=Production
   JWT_SECRET=your-super-secret-jwt-key-here
   ```

---

## 💰 Cost Breakdown

### **Netlify (Frontend):**
- **Free Tier:** ✅ Sufficient for most needs
- 100GB bandwidth/month
- 300 build minutes/month

### **Azure App Service (Backend):**
- **B1 Plan:** ~$13/month
- 1.75 GB RAM
- 10 GB storage
- Custom domains
- SSL certificates

### **Azure SQL Database:**
- **Basic Tier:** ~$5/month
- 2GB storage
- Good for development/small production

**Total Monthly Cost:** ~$18-20

### **Free Alternatives:**

**Backend:**
- Railway.app (Free tier with limits)
- Fly.io (Free tier available)
- Render.com (Free tier for web services)

**Database:**
- ElephantSQL (Free PostgreSQL)
- MongoDB Atlas (Free tier)
- Supabase (Free tier)

---

## 🎯 Quick Deploy Script

Create `deploy.ps1`:

```powershell
# Deploy script for full stack

Write-Host "🚀 Deploying Full Stack..." -ForegroundColor Green

# Deploy Backend
Write-Host "📦 Publishing Backend..." -ForegroundColor Cyan
cd DecorHaven.API
dotnet publish -c Release

Write-Host "☁️ Deploying to Azure..." -ForegroundColor Cyan
az webapp deploy `
  --resource-group decorhaven-rg `
  --name decorhaven-api `
  --src-path ./bin/Release/net8.0/publish `
  --type zip

# Deploy Frontend
Write-Host "🌐 Deploying Frontend..." -ForegroundColor Cyan
cd ..
netlify deploy --prod

Write-Host "✅ Deployment Complete!" -ForegroundColor Green
Write-Host "Frontend: https://decoration-hyper.netlify.app"
Write-Host "Backend: https://decorhaven-api.azurewebsites.net"
```

Run with:
```bash
.\deploy.ps1
```

---

## 🔄 CI/CD Setup (Optional)

### **Netlify Auto-Deploy:**

1. Connect GitHub repository
2. Netlify auto-deploys on push to main

### **Azure DevOps Pipeline:**

Create `azure-pipelines.yml`:

```yaml
trigger:
  - main

pool:
  vmImage: 'ubuntu-latest'

variables:
  buildConfiguration: 'Release'

steps:
- task: UseDotNet@2
  inputs:
    version: '8.x'

- script: dotnet build --configuration $(buildConfiguration)
  displayName: 'Build'
  workingDirectory: DecorHaven.API

- script: dotnet publish -c $(buildConfiguration) -o $(Build.ArtifactStagingDirectory)
  displayName: 'Publish'
  workingDirectory: DecorHaven.API

- task: AzureWebApp@1
  inputs:
    azureSubscription: 'Your-Subscription'
    appName: 'decorhaven-api'
    package: '$(Build.ArtifactStagingDirectory)/**/*.zip'
```

---

## 🐛 Troubleshooting

### **Backend not accessible:**
1. Check Azure App Service is running
2. Verify firewall rules on SQL Server
3. Check application logs in Azure Portal
4. Test API endpoint directly

### **Frontend can't reach backend:**
1. Verify CORS configuration
2. Check `netlify.toml` and `_redirects` files
3. Make sure API URL is correct
4. Check Network tab in browser (F12)

### **Database connection fails:**
1. Verify connection string is correct
2. Check SQL Server firewall allows Azure services
3. Verify credentials
4. Check migrations have been applied

### **Orders not saving:**
1. Check backend logs
2. Verify JWT token is being sent
3. Check database connection
4. Verify user is authenticated

---

## 📚 Useful Commands

```bash
# Check Netlify deploy status
netlify status

# View Netlify logs
netlify logs

# Azure: View app logs
az webapp log tail --name decorhaven-api --resource-group decorhaven-rg

# Azure: Restart app
az webapp restart --name decorhaven-api --resource-group decorhaven-rg

# Check database connection
az sql db show --resource-group decorhaven-rg --server decorhaven-sql --name DecorHavenDB
```

---

## ✅ Deployment Checklist

### **Frontend:**
- [ ] Netlify CLI installed
- [ ] Logged into Netlify
- [ ] Site linked
- [ ] Frontend deployed
- [ ] Site accessible

### **Backend:**
- [ ] Azure account created
- [ ] Resource group created
- [ ] App Service created
- [ ] SQL Database created
- [ ] Backend deployed
- [ ] Database migrations applied
- [ ] API accessible

### **Integration:**
- [ ] CORS configured
- [ ] API URLs updated in frontend
- [ ] Connection strings configured
- [ ] Environment variables set
- [ ] Full stack tested

---

## 🎉 Success!

Your full stack is now deployed:

**Frontend:** https://decoration-hyper.netlify.app  
**Backend:** https://decorhaven-api.azurewebsites.net  
**Status:** ✅ Production Ready

---

**Next Steps:**
1. Test all features end-to-end
2. Set up monitoring (Azure Application Insights)
3. Configure custom domain (optional)
4. Set up automated backups
5. Monitor costs and usage

**Congratulations on deploying your full stack e-commerce application!** 🎊🚀

