# 🚀 Netlify Deployment Guide

Complete guide to deploy your E-Commerce application to Netlify.

---

## 📋 **Prerequisites**

Before deploying to Netlify, make sure you have:

1. ✅ A Netlify account (free): https://www.netlify.com/
2. ✅ Git repository with your code (GitHub, GitLab, or Bitbucket)
3. ✅ Backend deployed (Azure, AWS, or other hosting)
4. ✅ All files committed to your repository

---

## 🎯 **Deployment Options**

### **Option 1: Deploy via Netlify UI (Easiest)**

#### **Step 1: Prepare Your Repository**

1. **Initialize Git** (if not already done):
```bash
git init
git add .
git commit -m "Initial commit for Netlify deployment"
```

2. **Push to GitHub** (or GitLab/Bitbucket):
```bash
# Create a new repository on GitHub first, then:
git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO.git
git branch -M main
git push -u origin main
```

#### **Step 2: Connect to Netlify**

1. Go to https://app.netlify.com/
2. Click **"Add new site"** → **"Import an existing project"**
3. Choose your Git provider (GitHub/GitLab/Bitbucket)
4. Authorize Netlify to access your repositories
5. Select your e-commerce repository

#### **Step 3: Configure Build Settings**

```
Build settings:
├─ Base directory: (leave empty)
├─ Build command: (leave empty)
└─ Publish directory: . (or leave empty)
```

#### **Step 4: Deploy**

1. Click **"Deploy site"**
2. Wait for deployment to complete (1-2 minutes)
3. Your site is live! 🎉

---

### **Option 2: Deploy via Netlify CLI (Advanced)**

#### **Step 1: Install Netlify CLI**

```bash
npm install -g netlify-cli
```

#### **Step 2: Login to Netlify**

```bash
netlify login
```

#### **Step 3: Initialize Site**

```bash
cd your-project-folder
netlify init
```

Follow the prompts:
- Create & configure a new site
- Choose your team
- Give it a site name (or let Netlify generate one)

#### **Step 4: Deploy**

```bash
# Deploy to production
netlify deploy --prod

# Or deploy to preview first
netlify deploy
```

---

### **Option 3: Drag & Drop (Quick Test)**

1. Go to https://app.netlify.com/drop
2. Drag your project folder onto the page
3. Instant deployment! (but no Git connection)

---

## ⚙️ **Configuration Files**

Your project now includes these Netlify configuration files:

### **1. netlify.toml**
Main configuration file with:
- Build settings
- Redirect rules (API proxy)
- Custom headers
- Cache control

### **2. _redirects**
Simple redirect rules:
- Proxies `/api/*` requests to your backend
- SPA fallback to `claude.html`

---

## 🔧 **Backend Configuration**

### **Important: Update Backend URL**

You need to deploy your backend separately and update the redirect URL.

#### **Option A: Deploy Backend to Azure**

1. **Publish to Azure App Service:**
```bash
cd DecorHaven.API
dotnet publish -c Release
```

2. **Deploy using Azure CLI or Visual Studio**

3. **Get your backend URL:**
```
https://your-app-name.azurewebsites.net
```

#### **Option B: Deploy Backend to Another Service**

- AWS Elastic Beanstalk
- Google Cloud Run
- Heroku
- DigitalOcean App Platform

#### **Update Netlify Configuration**

1. **Edit netlify.toml:**
```toml
[[redirects]]
  from = "/api/*"
  to = "https://YOUR-ACTUAL-BACKEND-URL/api/:splat"
  status = 200
  force = true
```

2. **Edit _redirects:**
```
/api/*  https://YOUR-ACTUAL-BACKEND-URL/api/:splat  200
```

3. **Commit and redeploy:**
```bash
git add netlify.toml _redirects
git commit -m "Update backend URL"
git push
```

---

## 🔒 **Environment Variables**

### **Set Environment Variables in Netlify:**

1. Go to **Site settings** → **Environment variables**
2. Click **"Add a variable"**
3. Add these variables:

```
API_URL = https://your-backend-api.azurewebsites.net/api
ENVIRONMENT = production
```

### **Access in Your Code:**

If you want to use environment variables:

```javascript
const API_BASE_URL = process.env.API_URL || '/api';
```

Note: For static sites, you'll need a build process to inject environment variables.

---

## 🌐 **Custom Domain**

### **Add Your Custom Domain:**

1. Go to **Site settings** → **Domain management**
2. Click **"Add custom domain"**
3. Enter your domain (e.g., `shop.yourdomain.com`)
4. Follow DNS configuration instructions
5. Netlify will automatically provision SSL certificate

### **DNS Configuration:**

**Option A: Using Netlify DNS**
- Point your domain's nameservers to Netlify
- Netlify manages all DNS records

**Option B: Using External DNS**
- Add A record: `104.198.14.52`
- Or add CNAME record: `your-site.netlify.app`

---

## 🔐 **CORS Configuration**

### **Update Backend CORS Settings:**

Your backend needs to allow requests from your Netlify domain.

**In DecorHaven.API/Program.cs:**

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",  // Local development
                "https://your-site.netlify.app",  // Netlify domain
                "https://yourdomain.com"  // Custom domain
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

app.UseCors("AllowFrontend");
```

---

## 📊 **Testing Your Deployment**

### **1. Check Site is Live**

Visit your Netlify URL:
```
https://your-site-name.netlify.app
```

### **2. Test Features**

✅ Page loads correctly
✅ Products display
✅ Cart functionality works
✅ Search works
✅ Quick view works
✅ Account features work (if backend is connected)
✅ Checkout works (if backend is connected)

### **3. Check Console**

Open browser console (F12):
- Should see: `🌐 API Base URL: /api`
- Should see: `🏠 Environment: Production`
- Check for any errors (red messages)

### **4. Test API Connectivity**

If backend is deployed:
```javascript
// Test in browser console
fetch('/api/products')
  .then(res => res.json())
  .then(data => console.log('✅ Backend connected:', data))
  .catch(err => console.error('❌ Backend error:', err));
```

---

## 🐛 **Troubleshooting**

### **Issue: API calls fail with CORS error**

**Solution:**
1. Update backend CORS settings to include Netlify domain
2. Redeploy backend
3. Check browser console for exact error

### **Issue: 404 errors for API calls**

**Solution:**
1. Verify `_redirects` file is in root directory
2. Check backend URL in redirect is correct
3. Ensure backend is deployed and running
4. Test backend URL directly in browser

### **Issue: Page shows blank/white screen**

**Solution:**
1. Check browser console for JavaScript errors
2. Verify `claude.html` is in root directory
3. Check Netlify deploy log for errors
4. Try deploying again

### **Issue: Features work locally but not on Netlify**

**Common causes:**
1. API URL not updated for production
2. Backend not deployed or not accessible
3. CORS not configured on backend
4. Environment variables not set

### **Issue: "Site not found" error**

**Solution:**
1. Check deployment status in Netlify dashboard
2. Verify publish directory is correct (should be `.` or root)
3. Check that files are in repository root

---

## 🔄 **Continuous Deployment**

Netlify automatically deploys when you push to your repository!

### **Workflow:**

```bash
# Make changes to your code
git add .
git commit -m "Update feature"
git push

# Netlify automatically:
# 1. Detects the push
# 2. Builds your site (if needed)
# 3. Deploys to production
# 4. Done! ✅
```

### **Deploy Notifications:**

Enable notifications in Netlify:
1. Go to **Site settings** → **Build & deploy** → **Deploy notifications**
2. Add email, Slack, or webhook notifications
3. Get notified of successful/failed deployments

---

## 📱 **Netlify Features to Explore**

### **1. Deploy Previews**
- Automatic preview for every pull request
- Test changes before merging

### **2. Branch Deploys**
- Deploy multiple branches simultaneously
- Great for staging environments

### **3. Split Testing**
- A/B test different versions
- Split traffic between branches

### **4. Forms**
- Built-in form handling
- No backend needed for contact forms

### **5. Functions**
- Serverless functions
- Can replace some backend endpoints

### **6. Analytics**
- Built-in analytics (paid feature)
- Page views, unique visitors, etc.

---

## 📋 **Deployment Checklist**

Before going live, check:

- [ ] All files committed to Git
- [ ] Backend deployed and accessible
- [ ] Backend URL updated in netlify.toml and _redirects
- [ ] CORS configured on backend
- [ ] Environment variables set (if needed)
- [ ] Custom domain configured (if using)
- [ ] SSL certificate active (automatic on Netlify)
- [ ] All features tested on deployed site
- [ ] Console shows no errors
- [ ] Mobile responsive checked
- [ ] Forms working (if any)
- [ ] Payment integration tested (if using real payment)

---

## 🎉 **Your Site is Live!**

Congratulations! Your e-commerce site is now deployed on Netlify!

**Share your site:**
```
https://your-site-name.netlify.app
```

Or with custom domain:
```
https://yourdomain.com
```

---

## 📚 **Additional Resources**

- **Netlify Documentation:** https://docs.netlify.com/
- **Netlify Community:** https://answers.netlify.com/
- **Netlify Blog:** https://www.netlify.com/blog/
- **Support:** https://www.netlify.com/support/

---

## 🔐 **Security Best Practices**

### **1. Environment Variables**
- Never commit API keys or secrets
- Use Netlify environment variables for sensitive data

### **2. HTTPS**
- Netlify provides free SSL (automatic)
- Always use HTTPS in production

### **3. Headers**
- Custom security headers configured in netlify.toml
- Protects against common vulnerabilities

### **4. API Security**
- Use JWT tokens for authentication
- Validate all inputs on backend
- Rate limiting on backend APIs

---

## 💡 **Pro Tips**

1. **Use Deploy Previews:** Review changes before merging
2. **Enable Build Notifications:** Stay informed of deploy status
3. **Monitor Performance:** Use Netlify Analytics or Google Analytics
4. **Optimize Images:** Use CDN and modern formats (WebP)
5. **Cache Assets:** Configured in netlify.toml for fast loading
6. **Custom 404 Page:** Create `404.html` for better UX
7. **Redirects File:** Use `_redirects` for URL management
8. **Branch Deploys:** Test features in isolation

---

## 🚀 **Next Steps**

After successful deployment:

1. **Monitor:** Check Netlify dashboard regularly
2. **Optimize:** Use Lighthouse for performance insights
3. **SEO:** Add meta tags, sitemap, robots.txt
4. **Analytics:** Set up Google Analytics or similar
5. **Backup:** Keep Git repository up to date
6. **Scale:** Upgrade Netlify plan if needed
7. **Market:** Share your site!

---

**Your E-Commerce site is ready for the world!** 🌍🎊

Need help? Check Netlify docs or open an issue in your repository!

