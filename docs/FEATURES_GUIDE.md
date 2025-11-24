# E-Commerce Features Guide

## Overview
This document describes the five main sections of the Décor Haven e-commerce website: Search, Quick View, Account, Cart, and Wishlist.

---

## 1. Search Section

### Purpose
Provides comprehensive product search functionality with real-time results, category filtering, and search history.

### Features
- **Search Bar**
  - Large input field with search button
  - Real-time search as you type (2+ characters)
  - Enter key to search
  - Auto-focus when modal opens

- **Search History**
  - Tracks last 5 searches in localStorage
  - Click to re-search previous queries
  - Displays when search input is empty
  - Automatic deduplication

- **Category Filters**
  - All (default)
  - Furniture 🪑
  - Lighting 💡
  - Wall Art 🎨
  - Accessories 🎁
  - Visual active state

- **Sort Options**
  - Relevance (default)
  - Price: Low to High
  - Price: High to Low
  - Name: A to Z
  - Rating

- **Search Results**
  - Grid layout of matching products
  - Product icon, category, name, price
  - Quick actions: Add to Cart, View
  - Scrollable results area

- **No Results State**
  - Friendly message
  - Popular search suggestions
  - Click suggestions to search

### How to Use
1. Click the search icon (🔍) in navigation
2. Type your search query (or select from history)
3. Results appear automatically
4. Filter by category if needed
5. Sort results using dropdown
6. Click cart icon to add to cart
7. Click "View" for quick view

### Data Storage
```javascript
// Search history stored in localStorage:
["modern chair", "table lamp", "wall art", "ceramic vase", "mirror"]
```

### Integration
```javascript
// Open search modal
openSearchModal();

// Perform search
performSearch();

// Filter by category
filterSearchResults('furniture');

// Sort results
sortSearchResults();
```

---

## 2. Quick View Modal

### Purpose
Allows users to quickly preview product details without leaving the current page.

### Features
- **Product Information Display**
  - Product name and category
  - Star rating and review count
  - Current price and old price (if on sale)
  - Product description
  - Product icon/image

- **Quantity Selector**
  - Increment/decrement buttons
  - Manual quantity input
  - Minimum quantity of 1

- **Actions**
  - Add to Cart with selected quantity
  - Add to Wishlist
  - Close modal

### How to Use
1. Click the eye icon (👁️) on any product card overlay
2. View product details in the modal
3. Adjust quantity using +/- buttons
4. Click "Add to Cart" or "Wishlist" button
5. Close with X button or click outside modal

### Code Integration
```javascript
// Open quick view
quickView(productId);

// The function automatically:
// - Loads product data
// - Updates modal content
// - Opens the modal
```

---

## 3. Account Section

### Purpose
Manages user authentication, profile information, and order history.

### Features

#### Login Tab
- Email and password input
- Form validation
- Session management using localStorage
- Success notifications

#### Register Tab
- Full name input
- Email and password fields
- Password confirmation
- Validation for matching passwords
- Automatic login after registration

#### Profile Tab (Visible after login)
- Display user information:
  - Full name
  - Email address
  - Member since date
- Logout button

#### Orders Tab (Visible after login)
- Order history display
- Order details:
  - Order number
  - Status (Completed, Pending, Cancelled)
  - Order date
  - Total amount
  - Number of items

### How to Use
1. Click the user icon (👤) in navigation
2. Select Login or Register tab
3. Fill in the form and submit
4. After login, access Profile and Orders tabs
5. Logout when finished

### Data Storage
```javascript
// User data stored in localStorage:
{
  name: "User Name",
  email: "user@example.com",
  memberSince: "11/23/2024"
}
```

---

## 4. Shopping Cart

### Purpose
Manages selected products before checkout.

### Features

#### Cart Display
- List of all cart items with:
  - Product image/icon
  - Product name and category
  - Individual price
  - Quantity controls
  - Remove button

#### Quantity Management
- Increment quantity (+)
- Decrement quantity (-)
- Auto-remove when quantity reaches 0

#### Cart Summary
- Subtotal calculation
- Tax calculation (10%)
- Shipping cost (Free over $200)
- Grand total

#### Actions
- Continue Shopping
- Proceed to Checkout
- Remove individual items

#### Empty State
- Friendly message when cart is empty
- Button to return to shopping

### How to Use
1. Add products using "Add to Cart" button
2. Click cart icon (🛒) in navigation to view
3. Adjust quantities with +/- buttons
4. Remove items with Remove button
5. Click "Proceed to Checkout" when ready

### Cart Count Badge
- Displays total number of items in cart
- Updates automatically when items are added/removed
- Visible in navigation bar

### Data Persistence
```javascript
// Cart stored in localStorage as:
[
  {
    productId: 1,
    product: { /* product details */ },
    quantity: 2
  }
]
```

---

## 5. Wishlist

### Purpose
Allows users to save favorite products for later.

### Features

#### Wishlist Grid
- Visual grid of saved products
- Product cards showing:
  - Product image/icon
  - Product name
  - Price
  - Action buttons

#### Actions per Item
- Add to Cart (🛒)
- Quick View (👁️)
- Remove from Wishlist (❌)

#### Empty State
- Friendly message when wishlist is empty
- Button to browse products

### How to Use
1. Click heart icon (❤️) on any product
2. View wishlist by clicking heart icon in navigation
3. Add items to cart directly from wishlist
4. Quick view products for more details
5. Remove items with X button

### Data Persistence
```javascript
// Wishlist stored in localStorage as array of IDs:
[1, 3, 5, 7]
```

---

## Common Features

### Modal System
All sections use a consistent modal system with:
- Smooth fade-in animation
- Click outside to close
- Close button (X) in top-right
- Responsive design for mobile
- Scroll prevention when open

### Notifications
All actions trigger notifications:
- Success (green) - Item added, login successful, etc.
- Error (red) - Failed actions, validation errors
- Info (blue) - Coming soon features, informational messages

### Responsive Design
All sections are fully responsive:
- Desktop: Full-featured layouts
- Tablet: Adjusted grid layouts
- Mobile: Stacked layouts, full-width buttons

---

## Technical Implementation

### CSS Classes
- `.modal-overlay` - Full-screen overlay backdrop
- `.modal` - Modal container
- `.modal-close` - Close button
- Section-specific classes for content

### JavaScript Functions

#### Modal Management
```javascript
openModal(modalId)    // Opens specified modal
closeModal(modalId)   // Closes specified modal
```

#### Quick View
```javascript
quickView(productId)              // Opens quick view
increaseQuantity()                // Increment quantity
decreaseQuantity()                // Decrement quantity
addToCartFromQuickView()          // Add to cart
addToWishlistFromQuickView()      // Add to wishlist
```

#### Account
```javascript
switchAccountTab(tabName)         // Switch between tabs
handleLogin(event)                // Process login
handleRegister(event)             // Process registration
handleLogout()                    // Logout user
updateUIForLoggedInUser()         // Update UI based on auth state
loadUserProfile()                 // Load profile data
loadUserOrders()                  // Load order history
```

#### Cart
```javascript
getCart()                         // Get cart from storage
saveCart(cart)                    // Save cart to storage
addToCartLocal(productId, qty)    // Add item to cart
updateCartQuantity(productId, change) // Update item quantity
removeFromCartLocal(productId)    // Remove item
updateCartDisplay()               // Refresh cart UI
updateCartCount()                 // Update cart badge
proceedToCheckout()               // Start checkout
```

#### Wishlist
```javascript
getWishlist()                     // Get wishlist from storage
saveWishlist(wishlist)            // Save wishlist to storage
addToWishlistLocal(productId)     // Add item to wishlist
removeFromWishlistLocal(productId) // Remove item
updateWishlistDisplay()           // Refresh wishlist UI
```

---

## Data Flow

### Local Storage Structure
```javascript
{
  // User authentication
  "isLoggedIn": "true",
  "user": {
    "name": "John Doe",
    "email": "john@example.com",
    "memberSince": "11/23/2024"
  },
  
  // Shopping cart
  "cart": [
    {
      "productId": 1,
      "product": { /* product data */ },
      "quantity": 2
    }
  ],
  
  // Wishlist
  "wishlist": [1, 3, 5, 7]
}
```

---

## Browser Compatibility
- Modern browsers (Chrome, Firefox, Safari, Edge)
- ES6+ JavaScript features
- CSS Grid and Flexbox
- LocalStorage API
- CSS Custom Properties (Variables)

---

## Future Enhancements

### Quick View
- [ ] Image gallery/carousel
- [ ] Color/size variations
- [ ] Stock availability
- [ ] Shipping estimates

### Account
- [ ] Password reset
- [ ] Email verification
- [ ] Order tracking
- [ ] Address management
- [ ] Payment methods

### Cart
- [ ] Promo code support
- [ ] Save for later
- [ ] Product recommendations
- [ ] Multiple shipping addresses
- [ ] Gift wrapping options

### Wishlist
- [ ] Multiple wishlists
- [ ] Share wishlist
- [ ] Price drop alerts
- [ ] Stock notifications

---

## Integration with Backend API

To integrate with the DecorHaven.API backend:

1. Replace `sampleProducts` with API calls to `/api/products/{id}`
2. Update `handleLogin` to call `/api/auth/login`
3. Update `handleRegister` to call `/api/auth/register`
4. Update cart functions to call `/api/cart` endpoints
5. Update order functions to call `/api/orders` endpoints
6. Add wishlist endpoints to the backend API

Example integration:
```javascript
async function quickView(productId) {
    const product = await getProductById(productId); // From frontend-integration.js
    // Update modal with product data
}

async function handleLogin(event) {
    event.preventDefault();
    const email = document.getElementById('loginEmail').value;
    const password = document.getElementById('loginPassword').value;
    const response = await login(email, password); // From frontend-integration.js
    if (response) {
        // Handle successful login
    }
}
```

---

## Troubleshooting

### Modal not opening
- Check if modal ID matches
- Verify JavaScript is loaded
- Check browser console for errors

### Cart count not updating
- Clear localStorage and refresh
- Check if cart functions are called
- Verify localStorage is enabled

### Styles not applying
- Clear browser cache
- Check CSS is loaded
- Verify no CSS conflicts

### Data not persisting
- Check localStorage is enabled
- Verify browser supports localStorage
- Check for private/incognito mode

---

## Support
For issues or questions:
1. Check browser console for errors
2. Verify all CSS and JS files are loaded
3. Test in different browsers
4. Check localStorage contents using DevTools

---

## Credits
Created for Décor Haven E-Commerce Platform
Built with HTML5, CSS3, and Vanilla JavaScript

