# 🚂 Deploy to Railway - Complete Guide

## ✅ What's Ready:
- [x] Git repository initialized & committed
- [x] Backend supports both SQL Server AND PostgreSQL
- [x] All packages updated
- [x] Frontend deployed to Netlify

---

## 🎯 Deploy in 3 Steps (10 Minutes Total)

### **Step 1: Push to GitHub** ⏱️ 3 minutes

#### **Create GitHub Repository:**

1. Go to: https://github.com/new
2. Repository name: `ecommerce-decorhaven`  
3. Description: `Full-stack e-commerce application with ASP.NET Core & JavaScript`
4. Keep it **Public**
5. **DO NOT** check "Initialize with README"
6. Click **Create repository**

#### **Push Your Code:**

GitHub will show you commands. Copy and run them:

```bash
git remote add origin https://github.com/YOUR-USERNAME/ecommerce-decorhaven.git
git branch -M main
git push -u origin main
```

**Example (replace with your username):**
```bash
git remote add origin https://github.com/ali-dev/ecommerce-decorhaven.git
git branch -M main
git push -u origin main
```

✅ **Done!** Your code is now on GitHub!

---

### **Step 2: Deploy to Railway** ⏱️ 5 minutes

#### **A. Sign Up & Create Project:**

1. Go to: https://railway.app
2. Click **"Login with GitHub"**
3. Authorize Railway
4. Click **"New Project"**
5. Select **"Deploy from GitHub repo"**
6. Choose **"ecommerce-decorhaven"** repository
7. Railway starts building...

#### **B. Configure Root Directory:**

Railway needs to know where your backend code is:

1. Click on your service card (should say "Building...")
2. Click **"Settings"** tab
3. Scroll to **"Root Directory"**
4. Enter: `DecorHaven.API`
5. Click outside to save
6. Service will redeploy automatically

#### **C. Add PostgreSQL Database:**

1. In your project dashboard, click **"+ New"**
2. Select **"Database"**
3. Choose **"Add PostgreSQL"**
4. Wait ~30 seconds for provisioning
5. ✅ Database ready!

#### **D. Configure Environment Variables:**

1. Click on your **API service** (NOT the database)
2. Go to **"Variables"** tab
3. Click **"+ New Variable"**

Add these variables one by one:

**Variable 1:**
- Name: `ASPNETCORE_ENVIRONMENT`
- Value: `Production`

**Variable 2:**
- Name: `JWT_SECRET`
- Value: `DecorHaven2025!ChangeThisLater#SecureKey`

**Variable 3:**
- Name: `ConnectionStrings__DefaultConnection`
- Value: Click on PostgreSQL database → Variables tab → Copy `DATABASE_URL` → Paste here

4. Click **"Add"** after each variable
5. Service will redeploy automatically

#### **E. Generate Domain:**

1. Click on your **API service**
2. Go to **"Settings"** tab
3. Scroll to **"Domains"** section  
4. Click **"Generate Domain"**
5. Copy your URL (e.g., `decorhaven-api.up.railway.app`)
6. **✅ Save this URL!**

---

### **Step 3: Connect Frontend to Backend** ⏱️ 2 minutes

Now link your Netlify frontend to Railway backend:

#### **A. Update _redirects File:**

Open `_redirects` and replace line 5:

**OLD:**
```
/api/*  https://your-backend-api.azurewebsites.net/api/:splat  200
```

**NEW (use your Railway URL):**
```
/api/*  https://decorhaven-api.up.railway.app/api/:splat  200
```

#### **B. Update netlify.toml:**

Open `netlify.toml` and update around line 10:

**OLD:**
```toml
to = "https://your-backend-api-url.com/api/:splat"
```

**NEW (use your Railway URL):**
```toml
to = "https://decorhaven-api.up.railway.app/api/:splat"
```

#### **C. Deploy Updated Frontend:**

```bash
git add _redirects netlify.toml
git commit -m "Connect frontend to Railway backend"
git push
netlify deploy --prod
```

✅ **Done!** Frontend and backend are now connected!

---

## 🧪 Test Your Full Stack!

### **1. Visit Your Site:**
https://decoration-hyper.netlify.app

### **2. Open Console (F12):**
Look for:
- ✅ "Environment: Production"
- ✅ "Backend connected"
- ✅ Products loading

### **3. Test Complete Flow:**

1. **Register:**
   - Click account icon
   - Go to "Register" tab
   - Fill in details
   - Click "Register"
   - Should see: "Registration successful!"

2. **Login:**
   - Enter your credentials
   - Click "Login"
   - Should switch to Profile tab

3. **Browse & Shop:**
   - Search for products
   - Click "Quick View"
   - Add to cart
   - View cart

4. **Checkout:**
   - Click "Proceed to Checkout"
   - Fill shipping details
   - Select payment method
   - Click "Place Order"
   - Should see: "Order placed successfully!"

5. **View Orders:**
   - Go to account → Orders tab
   - See your order listed
   - ✅ Order saved in database!

---

## 🎉 SUCCESS CHECKLIST

### **GitHub:**
- [ ] Repository created
- [ ] Code pushed successfully
- [ ] Can see files on GitHub

### **Railway:**
- [ ] Account created
- [ ] Project deployed
- [ ] Root directory set to `DecorHaven.API`
- [ ] PostgreSQL database added
- [ ] Environment variables configured
- [ ] Domain generated
- [ ] Service running (check status)

### **Netlify:**
- [ ] `_redirects` updated with Railway URL
- [ ] `netlify.toml` updated
- [ ] Frontend redeployed
- [ ] Site accessible

### **Testing:**
- [ ] Site loads without errors
- [ ] Can register account
- [ ] Can login
- [ ] Products load from backend
- [ ] Can add to cart (syncs with backend)
- [ ] Can place orders
- [ ] Orders appear in profile
- [ ] Orders saved in database

---

## 🐛 Troubleshooting

### **Problem: Railway build fails**

**Check:**
1. Is root directory set to `DecorHaven.API`?
2. Check Railway logs (click service → Logs)
3. Look for red error messages

**Solution:**
- Make sure root directory is exactly: `DecorHaven.API`
- Verify `DecorHaven.API.csproj` exists in that folder

---

### **Problem: Service keeps restarting**

**Check:**
1. Click service → Logs
2. Look for database connection errors

**Solution:**
- Verify `ConnectionStrings__DefaultConnection` variable
- Make sure you copied the full `DATABASE_URL` from PostgreSQL database
- Check that PostgreSQL database is running

---

### **Problem: Frontend can't connect to backend**

**Symptoms:**
- Products don't load
- "Failed to fetch" errors in console
- CORS errors

**Solution:**
1. Verify `_redirects` has correct Railway URL
2. Make sure Railway service is running (green status)
3. Test backend directly: Visit `https://YOUR-RAILWAY-URL/api/products`
4. Should return JSON with products

---

### **Problem: Can't register/login**

**Check:**
1. Open browser console (F12)
2. Network tab → Look for failed requests
3. Click failed request → Preview tab

**Solution:**
- Check backend logs in Railway
- Verify JWT_SECRET is set
- Make sure database is connected

---

## 📊 Your URLs

After deployment, save these:

```
┌──────────────────────────────────────────────────────────┐
│                                                          │
│  Frontend:   https://decoration-hyper.netlify.app       │
│  Backend:    https://YOUR-RAILWAY-URL                   │
│  Database:   (Internal PostgreSQL on Railway)           │
│                                                          │
│  Dashboards:                                             │
│  - Netlify:  https://app.netlify.com                    │
│  - Railway:  https://railway.app/dashboard              │
│  - GitHub:   https://github.com/YOUR-USERNAME/YOUR-REPO │
│                                                          │
└──────────────────────────────────────────────────────────┘
```

---

## 💰 Cost Breakdown

### **Railway Free Tier:**
- **$5 credit per month** (FREE)
- Covers ~500 hours of usage
- PostgreSQL database included
- No credit card required initially

### **Netlify:**
- **100% FREE**
- 100GB bandwidth/month
- Unlimited sites

### **Total Monthly Cost: $0** ✅

---

## 🎯 Quick Command Reference

```bash
# Check Railway logs
# (Go to Railway dashboard → Click service → Logs tab)

# Redeploy frontend
netlify deploy --prod

# Push changes to GitHub
git add .
git commit -m "Your message"
git push

# Check Railway status
# (Go to Railway dashboard → Check service status)
```

---

## 📈 Next Steps

After successful deployment:

1. ✅ **Test thoroughly** - Try all features
2. ✅ **Add products** - Populate your database
3. ✅ **Customize** - Update branding, colors
4. ✅ **Monitor** - Check Railway logs regularly
5. ✅ **Share** - Send your site link to friends!

**Optional:**
- Set up custom domain
- Add more payment methods
- Implement email notifications
- Add product reviews
- Create admin dashboard

---

## 🌟 You're Almost There!

Everything is ready. Just follow the 3 steps above and you'll have a fully deployed full-stack e-commerce application!

**Time needed:** 10 minutes  
**Difficulty:** Easy  
**Cost:** FREE  

---

## 🚀 Ready? Let's Deploy!

**Start with Step 1:** Create GitHub repository and push your code!

Then come back for Steps 2 & 3.

**You got this!** 💪

---

*Need help? Check:*
- *Railway Docs: https://docs.railway.app*
- *Netlify Docs: https://docs.netlify.com*
- *Or see: `docs/DEPLOYMENT_STATUS.md` for more options*

