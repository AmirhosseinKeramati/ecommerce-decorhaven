# 🔗 Connect Backend & Frontend - Final Steps

## ⚡ Quick Connection Guide

---

## 📋 OPTION 1: Use Railway (Recommended - Easiest)

### **Step 1A: Create GitHub Repository**

1. Go to: https://github.com/new
2. Repository name: `ecommerce-decorhaven`
3. Make it **Public**
4. **Don't check** "Initialize with README"
5. Click **Create repository**

### **Step 1B: Push Your Code**

GitHub will show commands. Run them:

```bash
# Replace YOUR-USERNAME with your actual GitHub username
git remote add origin https://github.com/YOUR-USERNAME/ecommerce-decorhaven.git
git branch -M main
git push -u origin main
```

**Example:**
```bash
git remote add origin https://github.com/john-doe/ecommerce-decorhaven.git
git branch -M main
git push -u origin main
```

---

### **Step 2: Deploy Backend to Railway**

1. **Go to:** https://railway.app
2. **Sign in** with GitHub
3. Click **"New Project"**
4. Choose **"Deploy from GitHub repo"**
5. Select **"ecommerce-decorhaven"**

6. **Configure:**
   - Click your service
   - Settings → Root Directory
   - Enter: `DecorHaven.API`
   - Save

7. **Add Database:**
   - Click **"+ New"**
   - Database → PostgreSQL
   - Wait 30 seconds

8. **Set Environment Variables:**
   Click API service → Variables → Add:
   
   ```
   ASPNETCORE_ENVIRONMENT = Production
   ```
   ```
   JWT_SECRET = DecorHaven2025!SecureKey#ChangeThisLater
   ```
   ```
   ConnectionStrings__DefaultConnection = <Copy from PostgreSQL DATABASE_URL>
   ```

9. **Get Your URL:**
   - Settings → Domains
   - Generate Domain
   - **COPY THIS URL!** (e.g., `decorhaven-api.up.railway.app`)

---

### **Step 3: Connect Frontend to Backend**

Now update your frontend with the Railway URL:

**A. Update _redirects:**

Change line 5 from:
```
/api/*  https://your-backend-api.azurewebsites.net/api/:splat  200
```

To (use YOUR Railway URL):
```
/api/*  https://decorhaven-api.up.railway.app/api/:splat  200
```

**B. Update netlify.toml:**

Around line 10, change:
```toml
to = "https://your-backend-api-url.com/api/:splat"
```

To (use YOUR Railway URL):
```toml
to = "https://decorhaven-api.up.railway.app/api/:splat"
```

**C. Deploy:**
```bash
git add _redirects netlify.toml
git commit -m "Connect to Railway backend"
git push
netlify deploy --prod
```

✅ **DONE!**

---

## 📋 OPTION 2: Run Backend Locally (Quick Test)

If you want to test locally first:

```bash
# Terminal 1: Run backend
cd DecorHaven.API
dotnet run

# Terminal 2: Frontend already on Netlify
# Just update _redirects to: http://localhost:5000/api/:splat
```

---

## 🧪 Test Your Connection

1. Visit: https://decoration-hyper.netlify.app
2. Press **F12** → Console
3. Look for:
   - ✅ "Environment: Production"
   - ✅ "Backend connected"
   - ✅ Products loading

4. **Full Test:**
   - Register account
   - Login
   - Add to cart
   - Place order
   - Check order in profile

---

## 🆘 Troubleshooting

**Can't push to GitHub?**
```bash
# Make sure you're logged into GitHub
# Check remote:
git remote -v

# If remote exists, remove and re-add:
git remote remove origin
git remote add origin https://github.com/YOUR-USERNAME/ecommerce-decorhaven.git
git push -u origin main
```

**Railway not deploying?**
- Check root directory is `DecorHaven.API`
- View logs: Click service → Logs tab
- Make sure PostgreSQL is running

**Frontend not connecting?**
- Verify Railway URL is correct in _redirects
- Check Railway service is running (green status)
- Test backend directly: https://your-railway-url/api/products

---

## ✅ Success Checklist

- [ ] Code pushed to GitHub
- [ ] Backend deployed to Railway
- [ ] PostgreSQL database created
- [ ] Environment variables set
- [ ] Railway domain generated
- [ ] _redirects updated
- [ ] netlify.toml updated
- [ ] Frontend redeployed
- [ ] Site tested and working

---

**You're almost there! Follow the steps above and you'll be 100% deployed!** 🚀


