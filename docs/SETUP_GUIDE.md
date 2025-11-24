# Setup Guide - Décor Haven E-Commerce Backend

This guide will walk you through setting up and running the C# backend for your e-commerce site.

## Prerequisites

Before you begin, ensure you have the following installed:

1. **.NET 8.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/8.0)
2. **SQL Server** - One of the following:
   - SQL Server Express (free) - [Download here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
   - SQL Server LocalDB (comes with Visual Studio)
   - Full SQL Server

3. **Optional but Recommended:**
   - Visual Studio 2022 Community Edition (free) - [Download here](https://visualstudio.microsoft.com/)
   - OR Visual Studio Code with C# extension

## Step-by-Step Setup

### Step 1: Verify .NET Installation

Open a terminal/command prompt and run:

```bash
dotnet --version
```

You should see version 8.0.x or higher.

### Step 2: Navigate to the Project Directory

```bash
cd DecorHaven.API
```

### Step 3: Install Dependencies

```bash
dotnet restore
```

This will download all required NuGet packages.

### Step 4: Configure Database Connection

1. Open `appsettings.json`
2. Update the connection string if needed:

**For SQL Server LocalDB (Default):**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DecorHavenDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

**For SQL Server Express:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=DecorHavenDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

**For Full SQL Server with authentication:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=DecorHavenDB;User Id=YOUR_USERNAME;Password=YOUR_PASSWORD;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

### Step 5: Update JWT Secret Key (Important!)

1. Open `appsettings.json`
2. Change the JWT secret key to a long random string:

```json
"JwtSettings": {
  "SecretKey": "ChangeThisToALongRandomSecureString123!@#$%^&*",
  "Issuer": "DecorHavenAPI",
  "Audience": "DecorHavenClient",
  "ExpiryInMinutes": 60
}
```

### Step 6: Install Entity Framework Tools (if not already installed)

```bash
dotnet tool install --global dotnet-ef
```

Or update if already installed:

```bash
dotnet tool update --global dotnet-ef
```

### Step 7: Create Database Migration

```bash
dotnet ef migrations add InitialCreate
```

This creates the migration files that will set up your database schema.

### Step 8: Apply Migration to Database

```bash
dotnet ef database update
```

This command will:
- Create the database if it doesn't exist
- Create all tables (Users, Products, Categories, Orders, etc.)
- Seed initial data (4 categories and 8 products)

### Step 9: Build the Project

```bash
dotnet build
```

Make sure there are no errors. All warnings can typically be ignored.

### Step 10: Run the Application

```bash
dotnet run
```

You should see output similar to:

```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

### Step 11: Test the API

Open your browser and go to:

```
http://localhost:5000
```

or

```
https://localhost:5001
```

You should see the Swagger UI with all the API endpoints documented.

## Testing the API with Swagger

### 1. Register a User

1. In Swagger UI, find `POST /api/auth/register`
2. Click "Try it out"
3. Enter the following JSON:

```json
{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "password": "Password123!",
  "phoneNumber": "+1234567890"
}
```

4. Click "Execute"
5. Copy the token from the response

### 2. Authorize Swagger

1. Click the "Authorize" button at the top of Swagger UI
2. Enter: `Bearer YOUR_TOKEN_HERE` (replace with your actual token)
3. Click "Authorize"

### 3. Test Protected Endpoints

Now you can test any endpoint that requires authentication:
- Get cart items
- Add items to cart
- Create orders
- Get user profile

### 4. Test Product Endpoints

Try these without authentication:
- `GET /api/products` - Get all products
- `GET /api/categories` - Get all categories
- `GET /api/products/1` - Get specific product

## Connecting Your HTML Frontend

### Step 1: Update Frontend JavaScript

1. Copy the `frontend-integration.js` file to your project
2. Include it in your `claude.html`:

```html
<script src="frontend-integration.js"></script>
```

### Step 2: Update Existing Functions

Replace the existing JavaScript functions in your HTML with API calls:

```javascript
// OLD: Local cart counter
function addToCart(productId) {
    cartCount++;
    cartCountElement.textContent = cartCount;
}

// NEW: API-based cart
async function addToCart(productId) {
    if (!getToken()) {
        showNotification('Please login first', 'error');
        return;
    }
    await addToCartAPI(productId, 1);
}
```

### Step 3: Enable CORS (if needed)

If you get CORS errors, the backend is already configured to allow all origins in development.

For production, update `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://yourdomain.com")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});
```

## Common Issues and Solutions

### Issue 1: Database Connection Failed

**Error:** `Microsoft.Data.SqlClient.SqlException: A network-related or instance-specific error...`

**Solutions:**
1. Verify SQL Server is running
2. Check connection string in `appsettings.json`
3. For LocalDB, try: `sqllocaldb start mssqllocaldb`
4. For SQL Server Express, ensure SQL Server service is running

### Issue 2: Migration Failed

**Error:** `The migration has already been applied...`

**Solution:**
```bash
dotnet ef database drop
dotnet ef database update
```

### Issue 3: Port Already in Use

**Error:** `Address already in use`

**Solution:**
Change ports in `Properties/launchSettings.json`:

```json
"applicationUrl": "https://localhost:5002;http://localhost:5003"
```

### Issue 4: JWT Token Invalid

**Error:** `401 Unauthorized`

**Solutions:**
1. Check if token is included in Authorization header
2. Verify token hasn't expired (default: 60 minutes)
3. Ensure format is: `Bearer YOUR_TOKEN`
4. Check JWT settings in `appsettings.json`

### Issue 5: CORS Error in Browser

**Error:** `Access to fetch at 'http://localhost:5000/api/...' from origin '...' has been blocked by CORS policy`

**Solution:**
The backend is already configured for CORS. If still getting errors:
1. Make sure the backend is running
2. Check browser console for exact error
3. Clear browser cache
4. Try accessing API directly first (without frontend)

## Verifying Database

To verify your database was created correctly:

### Using SQL Server Management Studio (SSMS):
1. Connect to `(localdb)\mssqllocaldb` or your SQL Server instance
2. Look for `DecorHavenDB` database
3. Check tables: Users, Products, Categories, Orders, etc.

### Using Visual Studio:
1. View → SQL Server Object Explorer
2. Expand SQL Server → (localdb)\mssqllocaldb → Databases
3. Find DecorHavenDB

### Using Command Line:
```bash
dotnet ef database update --connection "YOUR_CONNECTION_STRING"
```

## Development Tips

### Hot Reload

The application supports hot reload. Just save your C# files and changes will be reflected automatically (most of the time).

### Viewing Logs

All logs appear in the console where you ran `dotnet run`. Adjust log levels in `appsettings.Development.json`.

### Debugging in Visual Studio

1. Open `DecorHaven.API.csproj` in Visual Studio
2. Press F5 to start debugging
3. Set breakpoints in your code
4. Use the debugging tools to inspect variables

### Testing with Postman

Instead of Swagger, you can use Postman:

1. Install Postman
2. Import the API endpoints
3. Set environment variables for base URL and token
4. Create requests for each endpoint

## Production Deployment

When deploying to production:

1. **Update `appsettings.Production.json`:**
   - Use production database connection string
   - Change JWT secret to a secure value
   - Disable detailed error messages

2. **Publish the application:**
   ```bash
   dotnet publish -c Release -o ./publish
   ```

3. **Deploy to:**
   - Azure App Service
   - AWS Elastic Beanstalk
   - Docker container
   - IIS (Windows Server)
   - Linux server with Nginx

4. **Set up HTTPS:**
   - Use Let's Encrypt for free SSL certificates
   - Or use certificates from your hosting provider

5. **Configure environment variables:**
   - Never commit secrets to source control
   - Use Azure Key Vault, AWS Secrets Manager, or environment variables

## Next Steps

1. ✅ Backend is running
2. ✅ Database is set up with seed data
3. ✅ API endpoints are documented in Swagger
4. 📝 Integrate frontend with backend using `frontend-integration.js`
5. 📝 Add authentication UI (login/register forms)
6. 📝 Implement cart page
7. 📝 Implement checkout flow
8. 📝 Add order history page

## Support

If you encounter any issues:

1. Check this guide first
2. Review error messages in console
3. Check Swagger UI for API documentation
4. Review the README.md for API endpoint details
5. Check application logs

## Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [JWT Authentication Tutorial](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/)
- [SQL Server Documentation](https://docs.microsoft.com/en-us/sql/sql-server/)

Congratulations! Your backend is now running! 🎉

