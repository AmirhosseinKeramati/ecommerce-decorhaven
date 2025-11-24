# 🔗 Update Frontend with Railway Backend URL

## After You Get Your Railway URL:

Your Railway URL will look like:
```
https://decorhaven-api-production-XXXX.up.railway.app
```

---

## Files I'll Update:

### 1. _redirects

**Current (causes 502 error):**
```
/api/*  https://your-backend-api.azurewebsites.net/api/:splat  200
```

**New (your Railway URL):**
```
/api/*  https://YOUR-RAILWAY-URL/api/:splat  200
```

---

### 2. netlify.toml

**Current:**
```toml
to = "https://your-backend-api-url.com/api/:splat"
```

**New:**
```toml
to = "https://YOUR-RAILWAY-URL/api/:splat"
```

---

## Steps I'll Execute:

```bash
# 1. Update both files with your URL
# 2. Commit changes
git add _redirects netlify.toml
git commit -m "Connect frontend to Railway backend"
git push

# 3. Redeploy to Netlify
netlify deploy --prod

# 4. Test the connection
# Visit: https://decoration-hyper.netlify.app
# Try to login - NO MORE 502 ERROR! ✅
```

---

## ✅ Result:

After updating:
- ✅ No more 502 errors
- ✅ Login works
- ✅ Registration works
- ✅ Orders save to database
- ✅ Full backend integration

---

**Just paste your Railway URL and I'll do everything!** 🚀

