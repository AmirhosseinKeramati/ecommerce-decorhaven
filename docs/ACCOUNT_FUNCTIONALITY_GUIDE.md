# 🔐 Complete Account Functionality Guide

## ✅ Implemented Features

### 1. **User Registration** 
- New users can create an account with email and password
- Data is saved directly to SQL Server database
- User is automatically logged in after registration
- JWT token is generated for authentication
- Cart items are synced with backend

### 2. **User Login**
- Existing users can log in with email/password
- Credentials are verified against SQL Server
- JWT token is generated and stored
- Cart items are synced from backend
- Session persists across page refreshes

### 3. **Profile Management**
- **View Profile**: Display all account information
  - Name (First Name + Last Name)
  - Email
  - Phone Number
  - Address
  - City
  - Member Since date

- **Edit Profile**: Update account information
  - Edit first name and last name
  - Update phone number
  - Add/edit address
  - Add/edit city
  - Add postal code
  - Add country
  - Changes are saved to SQL Server immediately

### 4. **User Logout**
- Clear all session data
- Remove JWT token
- Reset UI to logged-out state
- Switch back to login tab
- User can login with another account

### 5. **Order History** (Tab available)
- View all past orders
- Order details including date, status, total
- Items in each order

---

## 🎯 How to Use

### **Register a New Account**

1. Click the "Account" icon in the navigation bar
2. Click "Register" tab
3. Fill in the registration form:
   - Full Name (will be split into First/Last name)
   - Email Address
   - Password (minimum 6 characters)
   - Confirm Password
4. Click "Register" button
5. You'll be automatically logged in!

### **Login to Existing Account**

1. Click the "Account" icon
2. Stay on "Login" tab (default)
3. Enter your credentials:
   - Email
   - Password
4. Click "Login" button
5. Your cart and wishlist will be synced

### **View Your Profile**

1. After logging in, click "Account" icon again
2. Click "Profile" tab
3. See all your account information:
   - Name
   - Email
   - Phone
   - Address
   - City
   - Member Since date

### **Edit Your Profile**

1. Go to Profile tab
2. Click "Edit Profile" button
3. Update any information you want:
   - First Name
   - Last Name
   - Phone Number
   - Address
   - City
   - Postal Code
   - Country
4. Click "Save Changes"
5. Or click "Cancel" to discard changes

### **Logout**

1. Go to Profile tab
2. Click "Logout" button (red button at bottom)
3. You'll be logged out and returned to login screen

### **Switch Accounts**

1. Logout from current account
2. Login with different credentials
3. Each account has its own cart, wishlist, and order history

---

## 🗄️ Data Storage

### **SQL Server Database**
All user data is stored in SQL Server with proper security:
- **Passwords**: Hashed using BCrypt (never stored as plain text)
- **JWT Tokens**: Generated on login/register, used for authentication
- **User Information**: Stored in `Users` table
- **Cart Items**: Stored in `CartItems` table (linked to user)
- **Orders**: Stored in `Orders` and `OrderItems` tables

### **Local Storage (Frontend)**
For quick access and offline capability:
- `token`: JWT authentication token
- `user`: Basic user information (name, email, etc.)
- `isLoggedIn`: Login status flag
- `cart`: Local cart backup
- `wishlist`: Wishlist items

---

## 🔒 Security Features

1. **Password Security**
   - Passwords are hashed with BCrypt before storing
   - Minimum 6 characters required
   - Never sent or stored in plain text

2. **JWT Authentication**
   - Secure token-based authentication
   - Token expires after set period
   - Token required for all protected endpoints

3. **Session Management**
   - Automatic logout on token expiration
   - Session persists on page refresh
   - Secure token storage

4. **API Security**
   - All user data requests require valid JWT token
   - CORS protection
   - Input validation on all forms

---

## 🔄 Data Synchronization

### **Cart Sync**
- When you register or login, your cart is synced with backend
- Any items in local cart are uploaded to server
- Server cart items are downloaded and displayed
- Keeps cart consistent across devices

### **Profile Sync**
- Profile is loaded from server when you view it
- Updates are immediately saved to database
- Latest data always displayed

---

## 📋 API Endpoints Used

| Endpoint | Method | Purpose |
|----------|--------|---------|
| `/auth/register` | POST | Create new account |
| `/auth/login` | POST | Login to account |
| `/auth/profile` | GET | Get profile data |
| `/auth/profile` | PUT | Update profile |
| `/cart` | GET | Get user's cart |
| `/cart` | POST | Add item to cart |
| `/orders` | GET | Get order history |

---

## 🧪 Testing Steps

### **Test Registration & Auto-Login**
1. Click Account → Register
2. Fill form with new email
3. Submit form
4. ✅ Should see "Registration successful!"
5. ✅ Should be automatically logged in
6. ✅ Profile and Orders tabs should appear

### **Test Profile View**
1. After login, click Profile tab
2. ✅ Should see your name, email, and other info
3. ✅ Member since date should be displayed

### **Test Profile Edit**
1. Click "Edit Profile" button
2. Change some information
3. Click "Save Changes"
4. ✅ Should see "Profile updated successfully!"
5. ✅ Updated info should be displayed
6. Check backend to verify data is saved

### **Test Logout & Re-login**
1. Click "Logout" button
2. ✅ Should see "Logged out successfully"
3. ✅ Should return to Login tab
4. ✅ Profile/Orders tabs should disappear
5. Login again with same credentials
6. ✅ All your data should still be there

### **Test Multiple Accounts**
1. Logout from Account A
2. Register/Login to Account B
3. ✅ Should see Account B's data
4. Add items to cart
5. Logout and login to Account A
6. ✅ Should see Account A's original data
7. ✅ Cart should be different

---

## 🐛 Troubleshooting

### **Registration Fails**
- **Error: "Email already exists"**
  - This email is already registered
  - Try logging in instead
  - Or use a different email

- **Error: "Passwords do not match"**
  - Make sure password and confirm password are identical

- **Error: "Password must be at least 6 characters"**
  - Use a longer password

### **Login Fails**
- **Error: "Invalid credentials"**
  - Check email and password
  - Make sure you registered first

- **Error: "Cannot connect to server"**
  - Backend is not running
  - Run: `cd DecorHaven.API && dotnet run`

### **Profile Update Fails**
- Check browser console for errors
- Make sure you're still logged in
- Try logging out and back in

### **Session Expires**
- JWT token has expired
- Simply log in again
- Your data is safe in database

---

## 💡 Tips

1. **Password Strength**: Use a strong password with letters, numbers, and special characters
2. **Profile Completeness**: Fill in all profile fields for better experience
3. **Regular Updates**: Keep your email and phone updated
4. **Secure Logout**: Always logout on shared computers
5. **Cart Persistence**: Your cart is saved even after logout

---

## 🎉 Success Criteria

✅ User can register and data is saved in SQL Server  
✅ User is automatically logged in after registration  
✅ User can view their complete profile information  
✅ User can edit and update their profile  
✅ Changes are immediately saved to database  
✅ User can logout successfully  
✅ User can login again with same or different account  
✅ Each account has separate data (cart, profile, orders)  
✅ Session persists across page refreshes  
✅ Security: passwords hashed, JWT authentication  

---

## 🚀 Next Steps

The complete account system is now fully functional! You can:
1. Register new users
2. Login/logout
3. View and edit profiles
4. Switch between accounts
5. All data persists in SQL Server database

**Everything is working perfectly!** 🎊

