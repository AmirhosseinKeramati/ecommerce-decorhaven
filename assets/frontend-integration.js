// Frontend Integration Example for Décor Haven E-Commerce
// This file shows how to integrate the HTML frontend with the C# backend

const API_BASE_URL = 'http://localhost:5000/api';

// Helper function to get token from localStorage
const getToken = () => localStorage.getItem('token');

// Helper function to get user from localStorage
const getUser = () => JSON.parse(localStorage.getItem('user') || 'null');

// Helper function to make authenticated API calls
const apiCall = async (endpoint, options = {}) => {
    const token = getToken();
    const headers = {
        'Content-Type': 'application/json',
        ...options.headers
    };

    if (token) {
        headers['Authorization'] = `Bearer ${token}`;
    }

    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
        ...options,
        headers
    });

    return await response.json();
};

// ============================================
// AUTHENTICATION APIs
// ============================================

// Register a new user
async function register(userData) {
    try {
        const response = await apiCall('/auth/register', {
            method: 'POST',
            body: JSON.stringify(userData)
        });

        if (response.success) {
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('user', JSON.stringify(response.data.user));
            updateCartCount();
            return response;
        } else {
            alert(response.message);
            return null;
        }
    } catch (error) {
        console.error('Registration error:', error);
        return null;
    }
}

// Login user
async function login(email, password) {
    try {
        const response = await apiCall('/auth/login', {
            method: 'POST',
            body: JSON.stringify({ email, password })
        });

        if (response.success) {
            localStorage.setItem('token', response.data.token);
            localStorage.setItem('user', JSON.stringify(response.data.user));
            updateCartCount();
            return response;
        } else {
            alert(response.message);
            return null;
        }
    } catch (error) {
        console.error('Login error:', error);
        return null;
    }
}

// Logout user
function logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    window.location.href = '/';
}

// Get user profile
async function getUserProfile() {
    try {
        const response = await apiCall('/auth/profile');
        if (response.success) {
            return response.data;
        }
        return null;
    } catch (error) {
        console.error('Get profile error:', error);
        return null;
    }
}

// ============================================
// PRODUCTS APIs
// ============================================

// Get all products
async function getAllProducts() {
    try {
        const response = await apiCall('/products');
        if (response.success) {
            return response.data;
        }
        return [];
    } catch (error) {
        console.error('Get products error:', error);
        return [];
    }
}

// Get product by ID
async function getProductById(productId) {
    try {
        const response = await apiCall(`/products/${productId}`);
        if (response.success) {
            return response.data;
        }
        return null;
    } catch (error) {
        console.error('Get product error:', error);
        return null;
    }
}

// Get products by category
async function getProductsByCategory(categoryId) {
    try {
        const response = await apiCall(`/products/category/${categoryId}`);
        if (response.success) {
            return response.data;
        }
        return [];
    } catch (error) {
        console.error('Get products by category error:', error);
        return [];
    }
}

// Search products
async function searchProducts(query) {
    try {
        const response = await apiCall(`/products/search?q=${encodeURIComponent(query)}`);
        if (response.success) {
            return response.data;
        }
        return [];
    } catch (error) {
        console.error('Search products error:', error);
        return [];
    }
}

// ============================================
// CATEGORIES APIs
// ============================================

// Get all categories
async function getAllCategories() {
    try {
        const response = await apiCall('/categories');
        if (response.success) {
            return response.data;
        }
        return [];
    } catch (error) {
        console.error('Get categories error:', error);
        return [];
    }
}

// ============================================
// CART APIs
// ============================================

// Get cart items
async function getCartItems() {
    try {
        const response = await apiCall('/cart');
        if (response.success) {
            return response.data;
        }
        return [];
    } catch (error) {
        console.error('Get cart items error:', error);
        return [];
    }
}

// Add item to cart
async function addToCart(productId, quantity = 1) {
    try {
        const response = await apiCall('/cart', {
            method: 'POST',
            body: JSON.stringify({ productId, quantity })
        });

        if (response.success) {
            showNotification('Product added to cart!', 'success');
            await updateCartCount();
            return response.data;
        } else {
            showNotification(response.message, 'error');
            return null;
        }
    } catch (error) {
        console.error('Add to cart error:', error);
        showNotification('Failed to add item to cart', 'error');
        return null;
    }
}

// Update cart item quantity
async function updateCartItem(cartItemId, quantity) {
    try {
        const response = await apiCall(`/cart/${cartItemId}`, {
            method: 'PUT',
            body: JSON.stringify(quantity)
        });

        if (response.success) {
            showNotification('Cart updated!', 'success');
            return true;
        }
        return false;
    } catch (error) {
        console.error('Update cart item error:', error);
        return false;
    }
}

// Remove item from cart
async function removeFromCart(cartItemId) {
    try {
        const response = await apiCall(`/cart/${cartItemId}`, {
            method: 'DELETE'
        });

        if (response.success) {
            showNotification('Item removed from cart', 'success');
            await updateCartCount();
            return true;
        }
        return false;
    } catch (error) {
        console.error('Remove from cart error:', error);
        return false;
    }
}

// Get cart total
async function getCartTotal() {
    try {
        const response = await apiCall('/cart/total');
        if (response.success) {
            return response.data;
        }
        return 0;
    } catch (error) {
        console.error('Get cart total error:', error);
        return 0;
    }
}

// Update cart count badge
async function updateCartCount() {
    const cartCountElement = document.querySelector('.cart-count');
    if (cartCountElement && getToken()) {
        try {
            const cartItems = await getCartItems();
            const count = Array.isArray(cartItems) ? cartItems.length : 0;
            cartCountElement.textContent = count;
        } catch (error) {
            console.error('Update cart count error:', error);
        }
    } else if (cartCountElement) {
        cartCountElement.textContent = '0';
    }
}

// ============================================
// ORDERS APIs
// ============================================

// Get user orders
async function getUserOrders() {
    try {
        const response = await apiCall('/orders');
        if (response.success) {
            return response.data;
        }
        return [];
    } catch (error) {
        console.error('Get orders error:', error);
        return [];
    }
}

// Create order from cart
async function createOrder(orderData) {
    try {
        const response = await apiCall('/orders', {
            method: 'POST',
            body: JSON.stringify(orderData)
        });

        if (response.success) {
            showNotification('Order placed successfully!', 'success');
            await updateCartCount();
            return response.data;
        } else {
            showNotification(response.message, 'error');
            return null;
        }
    } catch (error) {
        console.error('Create order error:', error);
        showNotification('Failed to create order', 'error');
        return null;
    }
}

// ============================================
// NEWSLETTER APIs
// ============================================

// Subscribe to newsletter
async function subscribeNewsletter(email) {
    try {
        const response = await apiCall('/newsletter/subscribe', {
            method: 'POST',
            body: JSON.stringify({ email })
        });

        if (response.success) {
            showNotification('Successfully subscribed to newsletter!', 'success');
            return true;
        } else {
            showNotification(response.message, 'error');
            return false;
        }
    } catch (error) {
        console.error('Newsletter subscription error:', error);
        showNotification('Failed to subscribe', 'error');
        return false;
    }
}

// ============================================
// UI HELPER FUNCTIONS
// ============================================

// Show notification
function showNotification(message, type = 'success') {
    const notification = document.createElement('div');
    notification.style.cssText = `
        position: fixed;
        top: 100px;
        right: 20px;
        background: ${type === 'success' ? '#27ae60' : '#e74c3c'};
        color: white;
        padding: 15px 25px;
        border-radius: 8px;
        box-shadow: 0 5px 15px rgba(0,0,0,0.3);
        z-index: 10000;
        animation: slideIn 0.3s ease;
    `;
    notification.textContent = message;
    document.body.appendChild(notification);

    setTimeout(() => {
        notification.style.animation = 'slideOut 0.3s ease';
        setTimeout(() => notification.remove(), 300);
    }, 3000);
}

// Load products on page load
async function loadProducts(categoryFilter = 'all') {
    const productGrid = document.getElementById('productGrid');
    if (!productGrid) return;

    try {
        let products;
        if (categoryFilter === 'all') {
            products = await getAllProducts();
        } else {
            // Map category names to IDs (based on seeded data)
            const categoryMap = {
                'furniture': 1,
                'lighting': 2,
                'wall-art': 3,
                'accessories': 4
            };
            const categoryId = categoryMap[categoryFilter];
            products = categoryId ? await getProductsByCategory(categoryId) : [];
        }

        // Clear existing products
        productGrid.innerHTML = '';

        // Render products
        products.forEach(product => {
            const productCard = createProductCard(product);
            productGrid.appendChild(productCard);
        });
    } catch (error) {
        console.error('Load products error:', error);
    }
}

// Create product card HTML
function createProductCard(product) {
    const card = document.createElement('div');
    card.className = 'product-card';
    card.dataset.category = product.categoryName.toLowerCase().replace(' ', '-');

    const badge = product.isNew ? '<span class="product-badge new">New</span>' :
                  product.oldPrice ? '<span class="product-badge">Sale</span>' : '';

    card.innerHTML = `
        ${badge}
        <div class="product-image">
            <i class="${product.iconClass || 'fas fa-box'}"></i>
            <div class="product-overlay">
                <button class="overlay-btn" onclick="quickView(${product.id})"><i class="fas fa-eye"></i></button>
                <button class="overlay-btn" onclick="handleAddToCart(${product.id})"><i class="fas fa-shopping-cart"></i></button>
            </div>
        </div>
        <div class="product-info">
            <div class="product-category">${product.categoryName}</div>
            <h3 class="product-name">${product.name}</h3>
            <div class="product-rating">
                <span class="stars">${getStarRating(product.averageRating)}</span>
                <span class="rating-count">(${product.reviewCount})</span>
            </div>
            <div class="product-price">
                <span class="current-price">$${product.price}</span>
                ${product.oldPrice ? `<span class="old-price">$${product.oldPrice}</span>` : ''}
            </div>
            <button class="add-to-cart" onclick="handleAddToCart(${product.id})">
                <i class="fas fa-shopping-cart"></i>
                Add to Cart
            </button>
        </div>
    `;

    return card;
}

// Generate star rating HTML
function getStarRating(rating) {
    const fullStars = Math.floor(rating);
    const hasHalfStar = rating % 1 >= 0.5;
    let stars = '';

    for (let i = 0; i < fullStars; i++) {
        stars += '<i class="fas fa-star"></i>';
    }
    if (hasHalfStar) {
        stars += '<i class="fas fa-star-half-alt"></i>';
    }
    const emptyStars = 5 - fullStars - (hasHalfStar ? 1 : 0);
    for (let i = 0; i < emptyStars; i++) {
        stars += '<i class="far fa-star"></i>';
    }

    return stars;
}

// Handle add to cart with authentication check
async function handleAddToCart(productId) {
    if (!getToken()) {
        showNotification('Please login to add items to cart', 'error');
        // Redirect to login or show login modal
        return;
    }

    await addToCart(productId, 1);
}

// Initialize on page load
document.addEventListener('DOMContentLoaded', function() {
    // Load products
    loadProducts();

    // Update cart count if user is logged in
    if (getToken()) {
        updateCartCount();
    }

    // Update newsletter form
    const newsletterForm = document.querySelector('.newsletter-form');
    if (newsletterForm) {
        newsletterForm.addEventListener('submit', async function(e) {
            e.preventDefault();
            const email = this.querySelector('input[type="email"]').value;
            const success = await subscribeNewsletter(email);
            if (success) {
                this.reset();
            }
        });
    }
});

// Update filter buttons to use API
function filterProducts(category) {
    loadProducts(category);

    // Update active button
    const filterBtns = document.querySelectorAll('.filter-btn');
    filterBtns.forEach(btn => btn.classList.remove('active'));
    event.target.classList.add('active');
}

// Quick view function
function quickView(productId) {
    // Implement modal or redirect to product details page
    console.log('Quick view for product:', productId);
    showNotification('Quick view feature coming soon!', 'info');
}

