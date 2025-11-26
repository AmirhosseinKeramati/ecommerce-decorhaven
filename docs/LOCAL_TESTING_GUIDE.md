# 🧪 LOCAL TESTING GUIDE

## ✅ What's Running Right Now:

### Backend API:
- **URL:** http://localhost:5000
- **Swagger UI:** http://localhost:5000/swagger
- **Status:** ✅ Running (Terminal 4)
- **Database:** SQL Server LocalDB (DecorHavenDB)

### Frontend:
- **File:** claude.html (opened in your browser)
- **Auto-detects:** Local environment
- **Connects to:** http://localhost:5000

---

## 🧪 Test the Full Application:

### 1. **Test Registration:**
1. Open the frontend (already opened)
2. Click the **Account** icon
3. Go to **Register** tab
4. Fill in:
   - Username: testuser
   - Email: test@example.com
   - Password: Test123!
   - Full Name: Test User
   - Phone: (optional)
5. Click **Register**
6. ✅ Should auto-login and show your profile

### 2. **Test Login:**
1. Click **Logout** (if logged in)
2. Go to **Login** tab
3. Enter credentials
4. Click **Login**
5. ✅ Should show profile with your details

### 3. **Test Shopping:**
1. Browse products on homepage
2. Click **Add to Cart**
3. View cart
4. Click **Proceed to Checkout**
5. Fill shipping info
6. Click **Place Order**
7. ✅ Order saved to database

### 4. **Test Profile:**
1. Click **Account** → **Profile**
2. Edit your details
3. Click **Update Profile**
4. ✅ Changes saved

### 5. **Test Orders:**
1. Click **Account** → **Orders**
2. ✅ See your order history

---

## 🔍 Check Database:

### View Database in SQL Server Management Studio:
1. Open SSMS
2. Connect to: `(localdb)\mssqllocaldb`
3. Database: `DecorHavenDB`
4. Tables to check:
   - `Users` - Your registered users
   - `Orders` - Placed orders
   - `OrderItems` - Order details

### Or use Swagger:
1. Go to: http://localhost:5000/swagger
2. Test endpoints directly:
   - `POST /api/auth/register`
   - `POST /api/auth/login`
   - `GET /api/auth/profile`
   - `POST /api/orders`

---

## 🐛 Debugging:

### Check Backend Logs:
- Terminal 4 shows all API requests
- Watch for any errors in red

### Check Browser Console:
- Press `F12` in browser
- Go to **Console** tab
- Look for API call logs

---

## ⏹️ Stop the Server:

When done testing:
```powershell
# Press Ctrl+C in Terminal 4
# Or just close the terminal
```

---

## 🚀 Next Steps:

### Option 1: Everything Works Locally? Deploy to Railway!
- Backend works? ✅
- Frontend works? ✅
- Ready to deploy? → Go to Railway

### Option 2: Found Issues?
- Tell me what's not working
- I'll fix it before we deploy

---

## 📊 Current Status:

| Component | Status | URL |
|-----------|--------|-----|
| Backend API | ✅ Running | http://localhost:5000 |
| Database | ✅ Connected | LocalDB |
| Frontend | ✅ Opened | file:///claude.html |
| Swagger | ✅ Available | http://localhost:5000/swagger |

---

## 💡 Tips:

1. **Auto-Detection:** Frontend automatically knows it's local
2. **No CORS Issues:** Backend allows localhost
3. **Database Persists:** Data saved between runs
4. **Hot Reload:** Frontend changes update on refresh

---

**Everything is ready to test! Try registering a new user now!** 🎉

