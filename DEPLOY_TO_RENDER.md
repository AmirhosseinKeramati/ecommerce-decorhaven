# 🚀 DEPLOY TO RENDER - COMPLETE GUIDE

## Why Render?
- ✅ **FREE tier** for backend API
- ✅ **Automatic HTTPS**
- ✅ **Free PostgreSQL database** (90 days, then $7/month or use free tier)
- ✅ **Auto-deploy from GitHub**
- ✅ **Easier than Railway** (simpler UI)

---

## 📋 STEP-BY-STEP DEPLOYMENT (8 MINUTES)

### **STEP 1: Open Render**
👉 Go to: **https://render.com**

---

### **STEP 2: Sign Up / Login**
1. Click **"Get Started"** or **"Sign In"**
2. Choose **"Sign in with GitHub"**
3. Authorize Render to access your GitHub
4. ✅ You're in!

---

### **STEP 3: Create Web Service**
1. Click **"New +"** button (top right)
2. Select **"Web Service"**
3. Connect your GitHub repository:
   - If first time: Click **"Connect account"** → Authorize
   - Search for: **"ecommerce-decorhaven"**
   - Click **"Connect"**

---

### **STEP 4: Configure Your Service**

Fill in these details:

#### **Basic Settings:**
- **Name:** `decorhaven-api` (or any name you like)
- **Region:** Choose closest to you (e.g., Oregon, Frankfurt)
- **Branch:** `main`
- **Root Directory:** `DecorHaven.API` ⚠️ **IMPORTANT!**

#### **Build Settings:**
- **Runtime:** `Docker` OR `.NET`
- **Build Command:**
  ```
  dotnet restore && dotnet publish -c Release -o out
  ```
- **Start Command:**
  ```
  cd out && dotnet DecorHaven.API.dll
  ```

#### **Plan:**
- Select **"Free"** (Free tier is perfect for testing)

---

### **STEP 5: Add Environment Variables** ⚠️ CRITICAL!

Click **"Advanced"** → **"Add Environment Variable"**

Add these 3 variables:

**Variable 1:**
```
Key: ASPNETCORE_ENVIRONMENT
Value: Production
```

**Variable 2:**
```
Key: ASPNETCORE_URLS
Value: http://0.0.0.0:$PORT
```

**Variable 3:**
```
Key: JWT_SECRET
Value: DecorHaven2025!SecureKey#ChangeThisToSomethingRandom
```

**Variable 4:** (We'll add database later)
```
Key: ConnectionStrings__DefaultConnection
Value: [Add after creating database - see Step 6]
```

---

### **STEP 6: Create PostgreSQL Database**

#### **Option A: Render PostgreSQL (Free for 90 days)**
1. Go back to Dashboard
2. Click **"New +"** → **"PostgreSQL"**
3. Fill in:
   - **Name:** `decorhaven-db`
   - **Database:** `decorhaven`
   - **User:** `decorhaven`
   - **Region:** Same as your web service
   - **Plan:** Free
4. Click **"Create Database"**
5. Wait ~30 seconds
6. Click on your database
7. Scroll down to **"Connections"**
8. Copy the **"Internal Database URL"**
9. Go to your Web Service → **"Environment"** tab
10. Find `ConnectionStrings__DefaultConnection`
11. **Paste** the database URL
12. Click **"Save Changes"**

#### **Option B: External PostgreSQL (Fully Free - Recommended)**
If you want completely free forever:
1. Sign up at: https://supabase.com (Free PostgreSQL)
2. Create a new project
3. Get connection string from Settings → Database
4. Use it for `ConnectionStrings__DefaultConnection`

---

### **STEP 7: Deploy!**

1. Click **"Create Web Service"**
2. Render will start building... ⏳
3. Watch the logs (you'll see build progress)
4. Wait ~3-5 minutes for first build
5. ✅ **Look for:** "Your service is live 🎉"

---

### **STEP 8: Get Your URL**

1. After successful deployment, you'll see your service
2. At the top, you'll see your URL:
   ```
   https://decorhaven-api.onrender.com
   ```
3. **✅ COPY THIS URL!**

---

### **STEP 9: Test Your Backend**

Visit in browser:
```
https://decorhaven-api.onrender.com/health
```

You should see:
```json
{
  "status": "healthy",
  "timestamp": "2025-11-26T..."
}
```

Or test Swagger:
```
https://decorhaven-api.onrender.com/swagger
```

---

### **STEP 10: Give Me Your Render URL**

Paste here:
```
My Render URL is: https://decorhaven-api.onrender.com
```

---

## 🔗 WHAT HAPPENS NEXT (I'll do this):

Once you give me your Render URL, I'll instantly:

```bash
# 1. Update _redirects
/api/*  https://YOUR-RENDER-URL/api/:splat  200

# 2. Update netlify.toml
to = "https://YOUR-RENDER-URL/api/:splat"

# 3. Commit & push
git add _redirects netlify.toml
git commit -m "Connect to Render backend"
git push

# 4. Deploy to Netlify
netlify deploy --prod

# 5. ✅ DONE!
```

---

## ✅ RESULT:

After I update:
- ✅ Visit: https://decoration-hyper.netlify.app/
- ✅ No more 502 error!
- ✅ Register works!
- ✅ Login works!
- ✅ Orders save to database!
- ✅ Everything connected!

---

## 🆘 TROUBLESHOOTING:

### Build Failed?
**Check these:**
- Root Directory is set to `DecorHaven.API` ✓
- Build command is correct ✓
- .NET runtime is selected ✓
- Check build logs for specific errors

### Deploy Failed?
**Check these:**
- All environment variables are set ✓
- Database connection string is correct ✓
- PORT variable is set by Render automatically ✓

### Database Connection Failed?
**Check these:**
- Database URL is copied correctly ✓
- Using "Internal Database URL" not "External" ✓
- Connection string starts with `postgresql://` ✓

### Service Won't Start?
**Check these:**
- Start command: `cd out && dotnet DecorHaven.API.dll` ✓
- Health check endpoint: `/health` ✓
- Check logs in Render dashboard ✓

---

## 💡 PRO TIPS:

1. **First deploy takes 3-5 minutes** - be patient!
2. **Free tier sleeps after inactivity** - first request might be slow
3. **Auto-deploys on git push** - any commit to main triggers redeploy
4. **Check logs** - Render has excellent real-time logs
5. **Health checks** - Render uses `/health` endpoint to verify service

---

## 📊 DEPLOYMENT CHECKLIST:

```
□ Go to render.com
□ Sign in with GitHub
□ New + → Web Service
□ Connect: ecommerce-decorhaven
□ Name: decorhaven-api
□ Root Directory: DecorHaven.API
□ Runtime: .NET or Docker
□ Build & Start commands configured
□ Add 4 environment variables
□ Create PostgreSQL database
□ Copy database URL to web service
□ Click "Create Web Service"
□ Wait for build to complete
□ Copy your Render URL
□ Paste URL here in chat
```

---

## ⏱️ ESTIMATED TIME:

- Render setup: **5 minutes**
- First build: **3-5 minutes**
- URL handoff: **30 seconds**
- My updates: **2 minutes**
- **TOTAL: ~10 minutes** ⏰

---

## 🎯 WHAT TO DO NOW:

1. **Open:** https://render.com
2. **Follow** steps above
3. **Copy** your Render URL
4. **Paste** it here

**Format:**
```
My Render URL is: https://decorhaven-api.onrender.com
```

---

**Ready? Go to https://render.com and start deploying!** 🚀

I'll be waiting for your Render URL to fix the 502 error! 💪

