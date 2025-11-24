# Registration Troubleshooting Guide

## Issue: "Registration Failed" Error

If you're seeing "Registration failed" when trying to register, follow these steps:

---

## Step 1: Check Backend Status

### Verify Backend is Running:
1. Check terminal where you ran `dotnet run`
2. You should see:
   ```
   Now listening on: http://localhost:5000
   Application started. Press Ctrl+C to shut down.
   ```

### If Backend is NOT Running:
```bash
cd DecorHaven.API
dotnet run
```

---

## Step 2: Check Browser Console

### Open Developer Console:
1. Press **F12** in your browser
2. Click the **Console** tab
3. Try to register again
4. Look for error messages

### Common Console Errors:

#### Error 1: "Network error: Failed to fetch"
**Problem:** Backend is not running or URL is wrong

**Solutions:**
- Start backend: `cd DecorHaven.API && dotnet run`
- Verify backend URL in console logs
- Check if http://localhost:5000 is accessible

#### Error 2: "CORS policy error"
**Problem:** Cross-Origin Resource Sharing issue

**Solution:** Backend CORS is already configured, but verify:
```csharp
// In Program.cs
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

#### Error 3: "Server error: 500"
**Problem:** Backend error (check backend terminal)

**Solution:** Look at backend terminal for error details

#### Error 4: "Email already exists"
**Problem:** Email is already registered

**Solution:** Use a different email or login instead

---

## Step 3: Check Detailed Error Messages

### New Debug Logs Added:

The frontend now shows detailed logs:
```javascript
API Call: POST http://localhost:5000/api/auth/register
API Response Status: 200 OK
API Response Data: { success: true, data: {...} }
```

### What to Look For:

1. **API Call URL**: Should be `http://localhost:5000/api/auth/register`
2. **Response Status**: Should be `200 OK` for success
3. **Response Data**: Should have `success: true`

---

## Step 4: Test Backend Directly

### Using Swagger UI:
1. Open http://localhost:5000 in browser
2. Find **POST /api/auth/register**
3. Click "Try it out"
4. Enter test data:
```json
{
  "firstName": "Test",
  "lastName": "User",
  "email": "test123@example.com",
  "password": "Password123!",
  "phoneNumber": ""
}
```
5. Click "Execute"
6. Should get 200 response with token

### If Swagger Works But Frontend Doesn't:
- Clear browser cache (Ctrl+Shift+R)
- Check browser console for errors
- Verify API_BASE_URL in claude.html is correct

---

## Step 5: Common Solutions

### Solution 1: Restart Backend
```bash
# Stop backend (Ctrl+C in terminal)
cd DecorHaven.API
dotnet run
```

### Solution 2: Clear Browser Data
```javascript
// In browser console:
localStorage.clear();
location.reload();
```

### Solution 3: Check Database Connection
```bash
cd DecorHaven.API
dotnet ef database update
```

### Solution 4: Verify Database Tables Exist
The backend terminal should show:
```
No migrations were applied. The database is already up to date.
```

If you see migration errors, run:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## Step 6: Registration Requirements

### Email:
- Must be valid email format
- Must be unique (not already registered)
- Example: `user@example.com`

### Password:
- Minimum 6 characters
- Must match confirmation
- Example: `Password123!`

### Name:
- Full name (first and last)
- Example: `John Doe`
- Frontend splits this into firstName and lastName

---

## Step 7: Test Registration Step-by-Step

### 1. Fill the form:
- **Name**: Test User
- **Email**: test001@example.com (use unique email)
- **Password**: Test123456
- **Confirm Password**: Test123456

### 2. Click Register

### 3. Watch for:
- Button changes to "Registering..."
- Console logs appear (F12)
- Backend terminal shows request

### 4. Expected Success:
- "Registration successful! Welcome!" notification
- Modal closes
- Profile and Orders tabs appear
- Console shows: `Registration response: { success: true, ... }`

### 5. If Failed:
- Error notification appears
- Console shows error details
- Backend terminal may show error
- Button returns to normal

---

## Debugging Checklist

- [ ] Backend is running (terminal shows "listening on")
- [ ] Frontend is opened in browser
- [ ] Browser console is open (F12)
- [ ] Email is unique (not used before)
- [ ] Password is 6+ characters
- [ ] Passwords match
- [ ] No console errors before registering
- [ ] Backend terminal shows no errors
- [ ] http://localhost:5000 is accessible
- [ ] Database tables are created

---

## Quick Test Script

Run this in browser console to test API directly:

```javascript
// Test registration API
async function testRegister() {
    const response = await fetch('http://localhost:5000/api/auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
            firstName: 'Test',
            lastName: 'User',
            email: `test${Date.now()}@example.com`,
            password: 'Password123!',
            phoneNumber: ''
        })
    });
    
    const data = await response.json();
    console.log('Status:', response.status);
    console.log('Response:', data);
    
    if (data.success) {
        console.log('✅ Registration works!');
        console.log('Token:', data.data.token);
    } else {
        console.log('❌ Registration failed:', data.message);
    }
}

testRegister();
```

---

## Still Not Working?

### Check These:

1. **Firewall**: Allow port 5000
2. **Antivirus**: May block localhost connections
3. **Browser**: Try different browser (Chrome, Edge, Firefox)
4. **Port**: Ensure port 5000 is not used by another app
5. **URL**: Verify API_BASE_URL is exactly `http://localhost:5000/api`

### Get More Info:

**Backend Logs:**
- Check terminal where `dotnet run` is running
- Should show incoming requests

**Frontend Logs:**
- Press F12 → Console tab
- Should show detailed API calls

**Network Tab:**
- Press F12 → Network tab
- Filter: XHR
- Try to register
- Click on the register request
- Check Headers, Response, Preview tabs

---

## Success Indicators

### You'll know it works when:

1. **Frontend Console:**
   ```
   API Call: POST http://localhost:5000/api/auth/register
   API Response Status: 200 OK
   API Response Data: { success: true, data: {...} }
   Registration response: { success: true, ... }
   ```

2. **Backend Terminal:**
   ```
   info: Microsoft.EntityFrameworkCore.Database.Command[20101]
         INSERT INTO [Users] ...
   ```

3. **Browser:**
   - "Registration successful! Welcome!" green notification
   - Modal closes
   - Profile and Orders tabs visible

4. **LocalStorage:**
   ```javascript
   localStorage.getItem('token') // Returns JWT token
   localStorage.getItem('user')  // Returns user data
   localStorage.getItem('isLoggedIn') // Returns 'true'
   ```

---

## Contact & Support

If you're still having issues:

1. Share the **browser console output** (F12 → Console)
2. Share the **backend terminal output**
3. Share the **Network tab** response (F12 → Network → XHR → register request)

This will help diagnose the exact issue!

---

## Quick Fix Commands

```bash
# Full reset
cd DecorHaven.API
dotnet build
dotnet ef database update
dotnet run

# In another terminal (or just open browser):
cd ..
start claude.html

# Then in browser console (F12):
localStorage.clear();
location.reload();
```

---

**🎯 Most Common Issue:** Backend not running or wrong URL. Always verify backend is listening on http://localhost:5000 first!

