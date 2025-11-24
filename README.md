# 🛒 DecorHaven - E-Commerce Application

Modern, full-stack e-commerce application with ASP.NET Core backend and vanilla JavaScript frontend.

![Status](https://img.shields.io/badge/status-production--ready-green)
![Frontend](https://img.shields.io/badge/frontend-HTML%2FCSS%2FJS-blue)
![Backend](https://img.shields.io/badge/backend-ASP.NET%20Core%208.0-purple)
![Database](https://img.shields.io/badge/database-SQL%20Server-orange)

---

## ✨ Features

### 🛍️ **Shopping Experience**
- Product browsing with categories
- Quick view product details
- Shopping cart management
- Wishlist functionality
- Advanced product search with filters
- Real-time search suggestions

### 👤 **User Management**
- User registration & authentication (JWT)
- Profile management (view & edit)
- Secure login/logout
- Order history tracking
- Password encryption (BCrypt)

### 💳 **Checkout & Orders**
- Complete checkout process
- Multiple payment methods (Credit Card, PayPal, Cash on Delivery)
- Order summary with calculations
- Guest checkout support
- Order confirmation
- Backend integration with SQL Server

### 🎨 **Modern UI/UX**
- Responsive design (mobile, tablet, desktop)
- Smooth animations & transitions
- Modal-based interactions
- Loading states & notifications
- Beautiful confirmation screens

---

## 🚀 Quick Start

### **Local Development**

1. **Clone the repository**
   ```bash
git clone https://github.com/YOUR_USERNAME/YOUR_REPO.git
cd e-commerce
```

2. **Start the backend** (optional, works offline without it)
```bash
cd DecorHaven.API
dotnet run
```

3. **Open the frontend**
   ```bash
# Just open claude.html in your browser
# Or use Live Server extension in VS Code
```

4. **Access the application**
```
http://localhost:5500/claude.html
```

---

## 📁 Project Structure

```
e-commerce/
├── claude.html              # Main application (Frontend)
├── index.html               # Entry point (redirects to claude.html)
├── netlify.toml            # Netlify configuration
├── _redirects              # URL routing & API proxy
├── .gitignore              # Git ignore rules
├── README.md               # This file
│
├── assets/                 # Additional resources
│   ├── frontend-integration.js
│   └── test-backend-connection.html
│
├── docs/                   # Documentation
│   ├── ACCOUNT_FUNCTIONALITY_GUIDE.md
│   ├── CHECKOUT_FEATURE_GUIDE.md
│   ├── NETLIFY_DEPLOYMENT_GUIDE.md
│   ├── DEPLOY_NOW.md
│   └── ... (more guides)
│
├── DecorHaven.API/         # Backend (ASP.NET Core)
│   ├── Controllers/
│   ├── Services/
│   ├── Models/
│   ├── DTOs/
│   ├── Data/
│   └── Program.cs
│
└── e-commerce.sln          # Solution file

```

---

## 🌐 Deployment

### ✅ **Frontend - DEPLOYED!**

**Live at:** https://decoration-hyper.netlify.app

The frontend is already live on Netlify with:
- ✅ CDN configured
- ✅ SSL enabled
- ✅ API proxy ready

### ⏳ **Backend - Choose Your Platform**

Deploy the backend to any of these platforms:

| Platform | Time | Cost | Guide |
|----------|------|------|-------|
| **Railway** ⭐ | 5 min | FREE | [docs/DEPLOYMENT_STATUS.md](docs/DEPLOYMENT_STATUS.md) |
| **Render** | 10 min | FREE | [docs/DEPLOY_BACKEND_FREE.md](docs/DEPLOY_BACKEND_FREE.md) |
| **Azure** | 20 min | ~$18/mo | [docs/FULL_DEPLOYMENT_GUIDE.md](docs/FULL_DEPLOYMENT_GUIDE.md) |

**🎯 Quick Start:** Open [START_HERE.md](START_HERE.md) for complete deployment instructions!

---

## 🔧 Configuration

### **Backend API URL**

The app automatically detects the environment:

- **Local:** `http://localhost:5000/api`
- **Production:** `/api` (proxied via Netlify)

To update backend URL for production:

1. Edit `netlify.toml` (line 10)
2. Edit `_redirects` (line 4)
3. Replace with your actual backend URL

### **Environment Variables**

In Netlify dashboard → Site settings → Environment variables:

```
API_URL=https://your-backend-url/api
ENVIRONMENT=production
```

---

## 🛠️ Technology Stack

### **Frontend**
- HTML5
- CSS3 (Flexbox, Grid, Animations)
- Vanilla JavaScript (ES6+)
- Font Awesome Icons
- Fetch API for backend communication

### **Backend**
- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server
- JWT Authentication
- BCrypt Password Hashing
- Repository Pattern
- AutoMapper

### **Deployment**
- Netlify (Frontend)
- Azure App Service (Backend - optional)
- SQL Server (Database)

---

## 📊 API Endpoints

### **Authentication**
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login user
- `GET /api/auth/profile` - Get user profile
- `PUT /api/auth/profile` - Update profile

### **Products**
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `GET /api/categories` - Get categories

### **Cart**
- `GET /api/cart` - Get user's cart
- `POST /api/cart` - Add to cart
- `PUT /api/cart/{id}` - Update cart item
- `DELETE /api/cart/{id}` - Remove from cart

### **Orders**
- `GET /api/orders` - Get user's orders
- `GET /api/orders/{id}` - Get order by ID
- `POST /api/orders` - Create new order

---

## 🧪 Testing

### **Test the frontend:**

1. Open `claude.html` in browser
2. Open console (F12)
3. Test features:
   - ✅ Add to cart
   - ✅ Add to wishlist
   - ✅ Search products
   - ✅ Quick view
   - ✅ Account features
   - ✅ Checkout process

### **Test with backend:**

1. Start backend: `cd DecorHaven.API && dotnet run`
2. Check console for: "✅ Backend connected"
3. Test full integration:
   - ✅ User registration
   - ✅ Login
   - ✅ Place order
   - ✅ View order history

---

## 📖 Documentation

**🎯 START HERE:**
- **[START_HERE.md](START_HERE.md)** - Main deployment guide

**Deployment Guides:**
- `docs/DEPLOYMENT_STATUS.md` - Current status + Railway quickstart ⭐
- `docs/DEPLOY_BACKEND_FREE.md` - Free deployment options
- `docs/FULL_DEPLOYMENT_GUIDE.md` - Complete Azure setup

**Project Info:**
- `docs/PROJECT_STRUCTURE.md` - File organization
- `README.md` - This file

---

## 🔐 Security

- ✅ JWT token authentication
- ✅ BCrypt password hashing
- ✅ HTTPS (automatic on Netlify)
- ✅ CORS configuration
- ✅ Input validation
- ✅ SQL injection prevention (EF Core)
- ✅ XSS protection headers

---

## 🌟 Features Checklist

- [x] Product catalog with categories
- [x] Shopping cart functionality
- [x] Wishlist
- [x] Product search & filters
- [x] Quick view modal
- [x] User authentication (register/login)
- [x] User profile management
- [x] Order placement & checkout
- [x] Order history
- [x] Guest checkout
- [x] Responsive design
- [x] Loading states & notifications
- [x] Backend integration
- [x] Database persistence
- [x] Production deployment ready

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

---

## 👨‍💻 Author

**Your Name**
- GitHub: [@YourUsername](https://github.com/YourUsername)

---

## 🙏 Acknowledgments

- Font Awesome for icons
- ASP.NET Core team
- Netlify for hosting

---

## 📧 Support

For support, email your-email@example.com or open an issue in the repository.

---

## 🚀 Live Demo

**Frontend (LIVE):** ✅ https://decoration-hyper.netlify.app  
**Backend:** Deploy using guides in `docs/`

---

## 📊 Status

**Version:** 1.0.0  
**Frontend:** ✅ DEPLOYED on Netlify  
**Backend:** ⏳ Ready to deploy (Railway/Render/Azure)  
**Last Updated:** November 24, 2025  

---

**Built with ❤️ using ASP.NET Core, SQL Server, and vanilla JavaScript**

---

## 🎯 Quick Links

- **[START_HERE.md](START_HERE.md)** - Complete deployment guide
- **[Live Frontend](https://decoration-hyper.netlify.app)** - See it in action
- **[Deploy Backend](docs/DEPLOYMENT_STATUS.md)** - 5-minute Railway setup
- **[Project Structure](docs/PROJECT_STRUCTURE.md)** - Understand the codebase

---

**🚀 Frontend is LIVE!** Deploy backend in 5 minutes → [START_HERE.md](START_HERE.md)
