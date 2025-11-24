# Backend Connection Guide

## Overview
This guide explains how to connect the Décor Haven frontend to the ASP.NET Core backend API and SQL Server database.

---

## Prerequisites

### Required Software:
1. **.NET 8.0 SDK or later**
   - Download: https://dotnet.microsoft.com/download

2. **SQL Server** (one of the following):
   - SQL Server Express (Free): https://www.microsoft.com/sql-server/sql-server-downloads
   - SQL Server LocalDB (included with Visual Studio)
   - Full SQL Server

3. **Optional Tools**:
   - Visual Studio 2022 (Community Edition is free)
   - Visual Studio Code with C# extension
   - SQL Server Management Studio (SSMS)

---

## Step 1: Configure the Database

### Option A: Using SQL Server LocalDB (Recommended for Development)

The project is pre-configured to use LocalDB with this connection string:

```json
"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DecorHavenDB;Trusted_Connection=True;"
```

LocalDB is automatically installed with Visual Studio. No additional configuration needed!

### Option B: Using SQL Server Express

1. **Update appsettings.json:**

Open `DecorHaven.API/appsettings.json` and update the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=DecorHavenDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Option C: Using SQL Server with Authentication

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=DecorHavenDB;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
  }
}
```

---

## Step 2: Apply Database Migrations

### Navigate to the API folder:
```bash
cd DecorHaven.API
```

### Check if Entity Framework tools are installed:
```bash
dotnet ef
```

### If not installed, install them:
```bash
dotnet tool install --global dotnet-ef
```

### Create the database:
```bash
# This will create the database and apply all migrations
dotnet ef database update
```

### Expected Output:
```
Build started...
Build succeeded.
Applying migration '20241123_InitialCreate'.
Done.
```

---

## Step 3: Verify Database Creation

### Using SSMS (SQL Server Management Studio):
1. Open SSMS
2. Connect to your server:
   - LocalDB: `(localdb)\mssqllocaldb`
   - SQL Express: `localhost\SQLEXPRESS`
3. Expand "Databases"
4. You should see "DecorHavenDB"
5. Expand tables to verify:
   - Users
   - Products
   - Categories
   - CartItems
   - Orders
   - OrderItems
   - Newsletters
   - WishlistItems (if created)

---

## Step 4: Start the Backend Server

### From Terminal:
```bash
cd DecorHaven.API
dotnet run
```

### Expected Output:
```
Building...
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

### Access Swagger UI:
Open your browser and go to:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001

You should see the Swagger API documentation interface.

---

## Step 5: Test API Endpoints

### Test Health:
```bash
curl http://localhost:5000/api/products
```

### Expected Response:
```json
{
  "success": true,
  "message": "Products retrieved successfully",
  "data": [...]
}
```

### Test Registration (via Swagger UI):
1. Go to http://localhost:5000
2. Find **POST /api/auth/register**
3. Click "Try it out"
4. Enter test data:
```json
{
  "firstName": "Test",
  "lastName": "User",
  "email": "test@example.com",
  "password": "Password123!",
  "phoneNumber": "+1234567890"
}
```
5. Click "Execute"
6. You should get a 200 OK response with a JWT token

---

## Step 6: Connect Frontend to Backend

### Verify Frontend Configuration:

Open `claude.html` and check the API_BASE_URL:

```javascript
const API_BASE_URL = 'http://localhost:5000/api';
```

### Open the Frontend:
1. Simply open `claude.html` in your browser
2. Open browser console (F12)
3. Check for connection status messages:
   - ✅ Backend connected
   - ⚠️ Backend not available - using offline mode

---

## Step 7: Test the Connection

### Test Registration:
1. **Open Frontend**: Double-click `claude.html`
2. **Click Account Icon** (👤)
3. **Click Register Tab**
4. **Fill in the form**:
   - Name: Test User
   - Email: test@example.com
   - Password: Password123!
   - Confirm Password: Password123!
5. **Click Register**
6. **Check for**:
   - "Registration successful! Welcome!" notification
   - Automatic login
   - Profile and Orders tabs appear

### Test Login:
1. **Click Account Icon** (👤)
2. **Click Login Tab**
3. **Enter credentials**:
   - Email: test@example.com
   - Password: Password123!
4. **Click Login**
5. **Check for**:
   - "Login successful!" notification
   - Profile and Orders tabs appear

### Test Profile:
1. **Click Account Icon** (👤)
2. **Click Profile Tab**
3. **Verify**:
   - Your name is displayed
   - Email is displayed
   - Member since date is shown

### Test Orders:
1. **Click Account Icon** (👤)
2. **Click Orders Tab**
3. **You should see**:
   - "No orders yet" message (if no orders)
   - Or list of your orders

---

## Troubleshooting

### Issue: Backend won't start

**Error**: "Unable to connect to database"

**Solutions**:
1. Verify SQL Server is running:
   ```bash
   # For LocalDB
   sqllocaldb info
   sqllocaldb start mssqllocaldb
   ```

2. Check connection string in `appsettings.json`

3. Try updating database again:
   ```bash
   dotnet ef database update
   ```

### Issue: "Failed to connect to database"

**Solutions**:
1. Verify SQL Server is accessible
2. Check firewall settings
3. Verify connection string format
4. Test connection in SSMS

### Issue: Frontend shows "Network error"

**Solutions**:
1. Verify backend is running (check terminal)
2. Check API_BASE_URL in `claude.html`
3. Open browser console for detailed error messages
4. Verify CORS is properly configured in `Program.cs`

### Issue: "Unauthorized" error

**Solutions**:
1. Token may have expired (default 60 minutes)
2. Logout and login again
3. Check JWT configuration in `appsettings.json`

### Issue: Registration fails

**Common Errors**:
1. **"Email already exists"**: Use a different email
2. **"Password too weak"**: Use at least 6 characters
3. **"Invalid email format"**: Check email format

### Issue: Orders not loading

**Solutions**:
1. Verify you're logged in (check Profile tab)
2. Check token in localStorage (F12 > Application > Local Storage)
3. Check backend logs for errors
4. Verify Orders endpoint is working (Swagger UI)

---

## Features Connected to Backend

### ✅ Connected Features:
- **Authentication**
  - Register
  - Login
  - Logout
  - Profile retrieval
- **Orders**
  - Fetch user orders
  - Display order history
- **Cart** (partial)
  - Sync on login

### ⚠️ Using LocalStorage (Not Yet Connected):
- **Cart Operations**
  - Add to cart
  - Update quantity
  - Remove from cart
- **Wishlist**
  - Add to wishlist
  - Remove from wishlist
- **Products**
  - Product search
  - Product display

### 🔜 To Be Connected:
- Product fetching from API
- Cart CRUD operations via API
- Wishlist API endpoints (need to be created in backend)
- Order creation via API

---

## API Endpoints Reference

### Authentication:
```
POST   /api/auth/register     - Register new user
POST   /api/auth/login        - Login user
GET    /api/auth/profile      - Get user profile (requires auth)
PUT    /api/auth/profile      - Update profile (requires auth)
```

### Products:
```
GET    /api/products          - Get all products
GET    /api/products/{id}     - Get product by ID
GET    /api/products/category/{categoryId}  - Get products by category
POST   /api/products          - Create product (Admin only)
PUT    /api/products/{id}     - Update product (Admin only)
DELETE /api/products/{id}     - Delete product (Admin only)
```

### Cart:
```
GET    /api/cart              - Get user's cart
GET    /api/cart/total        - Get cart total
POST   /api/cart              - Add item to cart
PUT    /api/cart/{id}         - Update cart item
DELETE /api/cart/{id}         - Remove from cart
DELETE /api/cart              - Clear cart
```

### Orders:
```
GET    /api/orders            - Get user's orders
GET    /api/orders/{id}       - Get order by ID
POST   /api/orders            - Create order from cart
```

### Categories:
```
GET    /api/categories        - Get all categories
GET    /api/categories/{id}   - Get category by ID
```

### Newsletter:
```
POST   /api/newsletter/subscribe    - Subscribe to newsletter
POST   /api/newsletter/unsubscribe  - Unsubscribe
```

---

## Security Configuration

### JWT Token Settings:

In `appsettings.json`:

```json
{
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyForJWTTokenGeneration123!@#",
    "Issuer": "DecorHavenAPI",
    "Audience": "DecorHavenClient",
    "ExpiryInMinutes": 60
  }
}
```

**⚠️ IMPORTANT**: Change `SecretKey` to a secure random string in production!

### Generate Secure Secret Key:

```bash
# PowerShell
$bytes = New-Object byte[] 32
[Security.Cryptography.RandomNumberGenerator]::Create().GetBytes($bytes)
[Convert]::ToBase64String($bytes)
```

---

## Production Deployment

### Before deploying to production:

1. **Update Connection String**:
   - Use production SQL Server
   - Never commit passwords to version control
   - Use environment variables or Azure Key Vault

2. **Update JWT Secret**:
   - Generate a new secure key
   - Store in environment variables

3. **Update CORS Policy**:
   ```csharp
   builder.Services.AddCors(options =>
   {
       options.AddPolicy("AllowAll", policy =>
       {
           policy.WithOrigins("https://yourdomain.com")
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials();
       });
   });
   ```

4. **Enable HTTPS**:
   - Remove `app.UseHttpsRedirection()` comment if commented
   - Configure SSL certificate

5. **Update API URL in Frontend**:
   ```javascript
   const API_BASE_URL = 'https://api.yourdomain.com/api';
   ```

---

## Testing Checklist

### Backend Tests:
- [ ] Database connection successful
- [ ] Migrations applied successfully
- [ ] Backend server starts without errors
- [ ] Swagger UI accessible
- [ ] Registration endpoint works
- [ ] Login endpoint works
- [ ] Products endpoint returns data
- [ ] Protected endpoints require authentication

### Frontend Tests:
- [ ] Frontend loads without errors
- [ ] Backend connection detected
- [ ] User can register
- [ ] User can login
- [ ] Profile displays correctly
- [ ] Orders load (or show "no orders")
- [ ] Logout works correctly
- [ ] Session persists after page refresh

### Integration Tests:
- [ ] Register → auto-login → profile shows
- [ ] Login → orders load → logout clears data
- [ ] Token expiry handled gracefully
- [ ] Network errors handled gracefully

---

## Support & Resources

### Documentation:
- [ASP.NET Core Docs](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [JWT Authentication](https://jwt.io/introduction)

### Tools:
- [Postman](https://www.postman.com/) - API testing
- [SSMS](https://docs.microsoft.com/sql/ssms) - Database management
- [Visual Studio Code](https://code.visualstudio.com/) - Code editor

### Common Commands:
```bash
# Check .NET version
dotnet --version

# Restore packages
dotnet restore

# Build project
dotnet build

# Run project
dotnet run

# Create migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Rollback migration
dotnet ef database update PreviousMigrationName
```

---

## Conclusion

Your frontend is now connected to the backend! You have:
- ✅ Working authentication system
- ✅ User registration and login
- ✅ Profile management
- ✅ Order history viewing
- ✅ JWT token-based security
- ✅ SQL Server database
- ✅ RESTful API endpoints

The application automatically detects if the backend is available and falls back to localStorage mode if not connected.

**Next Steps**:
1. Connect remaining features (cart, wishlist, products)
2. Add error handling and loading states
3. Implement order creation
4. Add admin panel
5. Deploy to production

Happy coding! 🚀

