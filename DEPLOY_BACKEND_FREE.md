# 🆓 Deploy ASP.NET Core Backend for FREE

## Best Free Options for Your DecorHaven Backend

---

## 🎯 **Option 1: Railway.app (Easiest & Free)**

### **Why Railway?**
- ✅ FREE $5 credit/month (enough for small apps)
- ✅ Automatic deployments from GitHub
- ✅ Built-in PostgreSQL database (free)
- ✅ Easy setup, no credit card required
- ✅ Supports .NET 8

### **Step-by-Step:**

1. **Sign up at:** https://railway.app

2. **Create New Project:**
   - Click **"New Project"**
   - Select **"Deploy from GitHub repo"**
   - Connect your GitHub account
   - Select your e-commerce repository

3. **Configure:**
   - Railway auto-detects .NET project
   - Root directory: `/DecorHaven.API`
   - Build command: `dotnet publish -c Release -o out`
   - Start command: `dotnet DecorHaven.API.dll`

4. **Add Database:**
   - In project dashboard, click **"+ New"**
   - Select **"Database"** → **"PostgreSQL"**
   - Copy connection string

5. **Set Environment Variables:**
   ```
   ASPNETCORE_ENVIRONMENT=Production
   JWT_SECRET=your-secret-key-here
   ConnectionStrings__DefaultConnection=<your-postgres-connection>
   ```

6. **Deploy:**
   - Push to GitHub
   - Railway auto-deploys
   - Get your URL: `https://decorhaven-api.railway.app`

7. **Update Frontend:**
   ```javascript
   // In claude.html, the API detection will automatically use the Railway URL
   ```

**Cost:** FREE (up to $5/month usage) 💰

---

## 🎯 **Option 2: Render.com (Free Tier)**

### **Why Render?**
- ✅ Completely FREE tier
- ✅ Automatic SSL
- ✅ Auto-deploy from GitHub
- ✅ Built-in PostgreSQL (free)

### **Step-by-Step:**

1. **Sign up at:** https://render.com

2. **Create Web Service:**
   - Dashboard → **New** → **Web Service**
   - Connect GitHub repo
   - Select your repository

3. **Configure:**
   ```
   Name: decorhaven-api
   Region: Oregon (free)
   Branch: main
   Root Directory: DecorHaven.API
   Runtime: .NET
   Build Command: dotnet publish -c Release -o out
   Start Command: cd out && dotnet DecorHaven.API.dll
   Plan: Free
   ```

4. **Add PostgreSQL Database:**
   - Dashboard → **New** → **PostgreSQL**
   - Name: `decorhaven-db`
   - Plan: Free
   - Copy internal connection string

5. **Environment Variables:**
   ```
   ASPNETCORE_ENVIRONMENT=Production
   ASPNETCORE_URLS=http://+:10000
   JWT_SECRET=your-secret-key
   ConnectionStrings__DefaultConnection=<postgres-connection>
   ```

6. **Deploy:**
   - Render starts building automatically
   - Wait 5-10 minutes
   - Your API: `https://decorhaven-api.onrender.com`

**Cost:** FREE forever (with limitations) 💰

**Note:** Free tier sleeps after 15 min of inactivity (wakes in ~30 sec on request)

---

## 🎯 **Option 3: Fly.io (Most Flexible)**

### **Why Fly.io?**
- ✅ Generous free tier
- ✅ Great performance
- ✅ Multiple regions
- ✅ Dockerfile support

### **Step-by-Step:**

1. **Install Flyctl:**
   ```powershell
   iwr https://fly.io/install.ps1 -useb | iex
   ```

2. **Login:**
   ```bash
   flyctl auth login
   ```

3. **Navigate to API:**
   ```bash
   cd DecorHaven.API
   ```

4. **Launch App:**
   ```bash
   flyctl launch
   
   # Answer prompts:
   # - App name: decorhaven-api
   # - Region: Choose closest
   # - Database: Yes (PostgreSQL)
   # - Deploy now: Yes
   ```

5. **Set Secrets:**
   ```bash
   flyctl secrets set JWT_SECRET="your-secret-key"
   flyctl secrets set ASPNETCORE_ENVIRONMENT="Production"
   ```

6. **Deploy:**
   ```bash
   flyctl deploy
   ```

7. **Your API:**
   - `https://decorhaven-api.fly.dev`

**Cost:** FREE for small apps 💰

---

## 🎯 **Option 4: Azure Free Tier**

### **Free Resources:**
- ✅ Free App Service (F1 tier)
- ✅ 1GB storage
- ✅ 60 CPU minutes/day
- ✅ Free SQL Database (limited)

### **Quick Deploy:**

```powershell
# Install Azure CLI
winget install Microsoft.AzureCLI

# Login
az login

# Create resources
az group create --name decorhaven-rg --location eastus

# Create free web app
az appservice plan create --name decorhaven-plan --resource-group decorhaven-rg --sku FREE
az webapp create --resource-group decorhaven-rg --plan decorhaven-plan --name decorhaven-api --runtime "DOTNETCORE:8.0"

# Deploy
cd DecorHaven.API
dotnet publish -c Release
Compress-Archive -Path ./bin/Release/net8.0/publish/* -DestinationPath ./app.zip -Force
az webapp deploy --resource-group decorhaven-rg --name decorhaven-api --src-path ./app.zip --type zip
```

**Your API:** `https://decorhaven-api.azurewebsites.net`

**Cost:** FREE (F1 tier) 💰

**Limitation:** App sleeps if idle

---

## 📊 **Comparison Table**

| Platform | Free Tier | Sleep? | Database | Setup Difficulty | Best For |
|----------|-----------|--------|----------|------------------|----------|
| **Railway** | $5 credit/month | No | PostgreSQL | ⭐⭐⭐⭐⭐ Easy | Quickest start |
| **Render** | Forever free | Yes (15 min) | PostgreSQL | ⭐⭐⭐⭐ Easy | Long-term free |
| **Fly.io** | 3 VMs free | No | PostgreSQL | ⭐⭐⭐ Medium | Best performance |
| **Azure** | F1 tier | Yes (idle) | Limited | ⭐⭐ Medium | Microsoft stack |

---

## 🚀 **My Recommendation: Railway.app**

**Why?**
1. 🎯 **Easiest setup** (5 minutes)
2. ✅ **No sleep** (always responsive)
3. 💾 **Free PostgreSQL** included
4. 🔄 **Auto-deploys** from GitHub
5. 📊 **Great dashboard** to monitor

---

## 🔧 **After Backend Deploys**

### **1. Update Backend URL in Frontend:**

The frontend already has dynamic API detection, but you can verify:

```javascript
// In claude.html, around line 2500
const API_BASE_URL = (() => {
    const hostname = window.location.hostname;
    if (hostname === 'localhost' || hostname === '127.0.0.1') {
        return 'http://localhost:5000/api';
    }
    // For production, use relative URL (Netlify will proxy)
    return '/api';
})();
```

### **2. Configure Netlify Proxy:**

Edit `_redirects`:

```
/api/*  https://decorhaven-api.railway.app/api/:splat  200
```

Replace `railway.app` with your actual backend URL.

### **3. Update Backend CORS:**

Edit `DecorHaven.API/Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5500",
                "https://decoration-hyper.netlify.app"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});
```

### **4. Redeploy Frontend:**

```bash
netlify deploy --prod
```

---

## ✅ **Quick Start: Railway (5 Minutes)**

```bash
# 1. Push your code to GitHub (if not already)
git add .
git commit -m "Prepare for Railway deployment"
git push

# 2. Go to https://railway.app
# 3. Sign in with GitHub
# 4. New Project → Deploy from GitHub
# 5. Select your repo → DecorHaven.API
# 6. Add PostgreSQL database
# 7. Copy railway URL

# 8. Update Netlify redirects
echo "/api/*  https://your-railway-url/api/:splat  200" > _redirects

# 9. Redeploy frontend
netlify deploy --prod

# Done! 🎉
```

---

## 🎯 **Test Full Stack**

```bash
# Test backend
curl https://decorhaven-api.railway.app/api/products

# Test frontend
https://decoration-hyper.netlify.app

# Open console (F12) and verify:
# ✅ API calls working
# ✅ Products loading
# ✅ Can register/login
# ✅ Cart syncing
# ✅ Orders placing
```

---

## 💡 **Pro Tips**

1. **Database Migrations:**
   - Railway/Render automatically run migrations on deploy
   - Or set startup command: `dotnet ef database update && dotnet DecorHaven.API.dll`

2. **Logs:**
   - Railway: Click on service → Logs tab
   - Render: Service → Logs
   - Fly: `flyctl logs`

3. **Secrets:**
   - Never commit JWT secrets to GitHub
   - Use platform's environment variables
   - Generate strong secrets: `openssl rand -base64 32`

4. **Performance:**
   - Free tiers are slower
   - Consider upgrading for production
   - Add caching for better performance

---

## 🆘 **Need Help?**

**Railway doesn't work?** → Try Render  
**Render too slow?** → Try Fly.io  
**Want Microsoft stack?** → Use Azure  

**All platforms have great documentation and support!**

---

## 🎉 **Success Checklist**

- [ ] Backend deployed to Railway/Render/Fly/Azure
- [ ] Database created and migrations applied
- [ ] Backend URL added to Netlify `_redirects`
- [ ] CORS configured for frontend domain
- [ ] Frontend redeployed
- [ ] Full stack tested
- [ ] Registration works
- [ ] Login works
- [ ] Orders save to database

**Your full stack is now LIVE!** 🚀🎊

---

**Questions? Check the FULL_DEPLOYMENT_GUIDE.md for detailed instructions!**

