# 🏠 Run Backend Locally & Connect

## Quick Local Testing Setup

---

## Step 1: Start Backend Locally

```bash
cd DecorHaven.API
dotnet run
```

Backend will start at: `http://localhost:5000`

---

## Step 2: Update Frontend for Local Backend

I'll update the _redirects file to point to your local backend:

In `_redirects`, change:
```
/api/*  https://your-backend-api.azurewebsites.net/api/:splat  200
```

To:
```
/api/*  http://localhost:5000/api/:splat  200
```

**Note:** This only works when both are running locally!

---

## Step 3: Test Locally

1. Keep backend running in one terminal
2. Open: `claude.html` in browser (or use Live Server)
3. Test all features locally

---

## For Production:

Use Railway deployment instead (see CONNECT_NOW.md)



