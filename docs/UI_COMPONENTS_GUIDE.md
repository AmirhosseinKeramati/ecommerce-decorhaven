# UI Components Visual Guide

## Navigation Bar Icons

The header contains four main interactive icons that open the new modals:

```
┌─────────────────────────────────────────────────────────────┐
│  🪑 Décor Haven    [Home] [Categories] [Products]           │
│                                                              │
│                    🔍  ❤️  🛒  👤                            │
│                   Search Wishlist Cart Account              │
└─────────────────────────────────────────────────────────────┘
```

### Icon Actions:
- **🔍 Search** - Search functionality (existing)
- **❤️ Wishlist** - Opens Wishlist Modal
- **🛒 Cart** - Opens Cart Modal (with count badge)
- **👤 Account** - Opens Account Modal

---

## 1. Quick View Modal

### Layout Structure:
```
┌────────────────────────────────────────────────────────┐
│  Quick View                                    [X]     │
├─────────────────────┬──────────────────────────────────┤
│                     │  FURNITURE                       │
│                     │  Modern Velvet Armchair          │
│      [Image]        │  ⭐⭐⭐⭐⭐ (128)                  │
│    or [Icon]        │  $599  $799                      │
│                     │                                  │
│    400x400px        │  Premium quality product...      │
│                     │                                  │
│                     │  [-]  [1]  [+]  Quantity         │
│                     │                                  │
│                     │  [Add to Cart] [Wishlist]        │
└─────────────────────┴──────────────────────────────────┘
```

### Key Features:
- **Split Layout**: Image on left, details on right
- **Responsive**: Stacks vertically on mobile
- **Interactive Quantity**: +/- buttons with numeric display
- **Dual Actions**: Add to Cart and Wishlist buttons
- **Price Display**: Shows current price and crossed-out old price

### Color Scheme:
- Primary: #2c3e50 (dark blue)
- Secondary: #e74c3c (red)
- Background: #ffffff (white)
- Light BG: #ecf0f1 (light gray)

---

## 2. Account Modal

### Tab Structure:
```
┌─────────────────────────────────────────────────────┐
│  [Login] [Register] [Profile] [Orders]         [X] │
├─────────────────────────────────────────────────────┤
│                                                     │
│  Active Tab Content:                                │
│                                                     │
│  ┌──────────────────────────────────────────┐      │
│  │  Email Address                           │      │
│  │  [________________________]               │      │
│  │                                          │      │
│  │  Password                                │      │
│  │  [________________________]               │      │
│  │                                          │      │
│  │  [        Login Button        ]          │      │
│  └──────────────────────────────────────────┘      │
│                                                     │
└─────────────────────────────────────────────────────┘
```

### Login Tab:
- Email input field
- Password input field
- Submit button with icon
- Form validation

### Register Tab:
- Full name input
- Email input
- Password input
- Confirm password input
- Submit button with icon
- Password matching validation

### Profile Tab (After Login):
```
┌─────────────────────────────────────────────────────┐
│  Account Information                                │
│  ┌───────────────────────────────────────────────┐  │
│  │  Name:           John Doe                     │  │
│  │  ────────────────────────────────────────────  │  │
│  │  Email:          john@example.com             │  │
│  │  ────────────────────────────────────────────  │  │
│  │  Member Since:   11/23/2024                   │  │
│  └───────────────────────────────────────────────┘  │
│                                                     │
│  [           Logout Button           ]              │
└─────────────────────────────────────────────────────┘
```

### Orders Tab (After Login):
```
┌─────────────────────────────────────────────────────┐
│  ┌───────────────────────────────────────────────┐  │
│  │  Order #12345          [Completed]            │  │
│  │  ─────────────────────────────────────────────│  │
│  │  Date:         Nov 20, 2024                   │  │
│  │  Total:        $1,299.00                      │  │
│  │  Items:        3 products                     │  │
│  └───────────────────────────────────────────────┘  │
│                                                     │
│  ┌───────────────────────────────────────────────┐  │
│  │  Order #12346          [Pending]              │  │
│  │  ─────────────────────────────────────────────│  │
│  │  Date:         Nov 22, 2024                   │  │
│  │  Total:        $599.00                        │  │
│  │  Items:        1 product                      │  │
│  └───────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────┘
```

### Status Badges:
- **Completed**: Green background (#27ae60)
- **Pending**: Orange background (#f39c12)
- **Cancelled**: Red background (#e74c3c)

---

## 3. Shopping Cart Modal

### Layout Structure:
```
┌────────────────────────────────────────────────────┐
│  🛒 Shopping Cart                            [X]   │
├────────────────────────────────────────────────────┤
│                                                    │
│  ┌──────────────────────────────────────────────┐ │
│  │ [Img] Product Name          [-] 2 [+]  [🗑️] │ │
│  │       CATEGORY              $599.00           │ │
│  └──────────────────────────────────────────────┘ │
│                                                    │
│  ┌──────────────────────────────────────────────┐ │
│  │ [Img] Another Product       [-] 1 [+]  [🗑️] │ │
│  │       CATEGORY              $249.00           │ │
│  └──────────────────────────────────────────────┘ │
│                                                    │
│  ┌──────────────────────────────────────────────┐ │
│  │  Subtotal:                        $848.00    │ │
│  │  Tax (10%):                       $84.80     │ │
│  │  Shipping:                        FREE       │ │
│  │  ────────────────────────────────────────    │ │
│  │  Total:                           $932.80    │ │
│  └──────────────────────────────────────────────┘ │
│                                                    │
│  [Continue Shopping]  [Proceed to Checkout]       │
└────────────────────────────────────────────────────┘
```

### Cart Item Structure:
```
┌─────────────────────────────────────────────────┐
│  ┌────┐                                         │
│  │    │  Product Name                           │
│  │IMG │  Category                               │
│  │    │  $599.00                                │
│  └────┘                                         │
│         [-] [2] [+]           [🗑️ Remove]       │
└─────────────────────────────────────────────────┘
```

### Empty Cart State:
```
┌────────────────────────────────────────────────┐
│                                                │
│              🛒                                │
│                                                │
│        Your cart is empty                      │
│                                                │
│   Looks like you haven't added anything        │
│           to your cart yet                     │
│                                                │
│      [   Continue Shopping   ]                 │
│                                                │
└────────────────────────────────────────────────┘
```

---

## 4. Wishlist Modal

### Grid Layout:
```
┌────────────────────────────────────────────────────┐
│  ❤️ My Wishlist                             [X]   │
├────────────────────────────────────────────────────┤
│                                                    │
│  ┌──────┐  ┌──────┐  ┌──────┐  ┌──────┐          │
│  │  [X] │  │  [X] │  │  [X] │  │  [X] │          │
│  │      │  │      │  │      │  │      │          │
│  │ Icon │  │ Icon │  │ Icon │  │ Icon │          │
│  │      │  │      │  │      │  │      │          │
│  │      │  │      │  │      │  │      │          │
│  ├──────┤  ├──────┤  ├──────┤  ├──────┤          │
│  │Name  │  │Name  │  │Name  │  │Name  │          │
│  │$599  │  │$249  │  │$189  │  │$129  │          │
│  │[🛒][👁️]│  │[🛒][👁️]│  │[🛒][👁️]│  │[🛒][👁️]│          │
│  └──────┘  └──────┘  └──────┘  └──────┘          │
│                                                    │
└────────────────────────────────────────────────────┘
```

### Wishlist Item Card:
```
┌──────────────────┐
│      [X]         │  ← Remove button (top-right)
│                  │
│    ┌────────┐    │
│    │        │    │
│    │  Icon  │    │  ← Product icon/image
│    │        │    │
│    └────────┘    │
│                  │
│  Product Name    │
│  $599.00         │  ← Price
│                  │
│  [🛒]    [👁️]    │  ← Actions: Add to Cart, Quick View
└──────────────────┘
```

### Empty Wishlist State:
```
┌────────────────────────────────────────────────┐
│                                                │
│              ❤️                                │
│                                                │
│       Your wishlist is empty                   │
│                                                │
│     Save your favorite items                   │
│          to your wishlist                      │
│                                                │
│      [   Browse Products   ]                   │
│                                                │
└────────────────────────────────────────────────┘
```

---

## Common UI Elements

### Button Styles

#### Primary Button (Red):
```
┌──────────────────────────┐
│  🛒  Add to Cart          │  ← Red background (#e74c3c)
└──────────────────────────┘
```

#### Secondary Button (Transparent with border):
```
┌──────────────────────────┐
│  ❤️  Wishlist             │  ← Transparent with white border
└──────────────────────────┘
```

#### Icon Button (Circular):
```
  ┌───┐
  │ 👁️ │  ← 45px diameter, white background
  └───┘
```

### Modal Close Button:
```
     ┌───┐
     │ × │  ← Top-right corner, 40px diameter
     └───┘      Light gray background, rotates on hover
```

### Notification Toast:
```
┌─────────────────────────────────────┐
│  Product added to cart!             │  ← Slides in from right
└─────────────────────────────────────┘    Auto-dismisses after 3s
```

**Notification Types:**
- Success: Green background (#27ae60)
- Error: Red background (#e74c3c)
- Info: Blue background (#3498db)

---

## Animations

### Modal Open:
- Background: Fade in (0.3s)
- Content: Slide up + fade in (0.4s)

### Modal Close:
- Reverse of open animation
- Background fade out
- Content slide down

### Cart Badge Pulse:
```
    (2)         (2)         (2)
   Normal    →  Scale    →  Normal
                1.2x
```
Animation duration: 0.5s

### Hover Effects:
- Product cards: Translate up 10px
- Buttons: Translate up 3px + deeper shadow
- Icons: Scale 1.1x + color change

---

## Responsive Breakpoints

### Desktop (> 768px):
- Quick View: 2 columns (image | details)
- Cart: Full layout with all elements visible
- Wishlist: 4 columns grid
- Account tabs: Single row

### Tablet (≤ 768px):
- Quick View: Stacked layout
- Cart: Adjusted spacing
- Wishlist: 3 columns grid
- Account tabs: May wrap

### Mobile (< 768px):
- Quick View: Full width single column
- Cart: Compact layout, stacked actions
- Wishlist: 2 columns grid
- Account tabs: Wrapped, smaller text
- All buttons: Full width

---

## Color Palette

```
Primary Color:    #2c3e50  ████  Dark Blue
Secondary Color:  #e74c3c  ████  Red
Accent Color:     #f39c12  ████  Orange
Light BG:         #ecf0f1  ████  Light Gray
Dark Text:        #2c3e50  ████  Dark Blue
Light Text:       #7f8c8d  ████  Gray
White:            #ffffff  ████  White
Success:          #27ae60  ████  Green
```

---

## Typography

```
Headings:
- Modal Title: 32px, Bold
- Section Title: 24px, Bold
- Product Name: 18px, Semi-bold

Body:
- Regular Text: 16px
- Small Text: 14px
- Tiny Text: 13px

Font Family:
'Segoe UI', Tahoma, Geneva, Verdana, sans-serif
```

---

## Icons Used (Font Awesome 6.4.0)

### Navigation:
- fa-search (🔍)
- fa-heart (❤️)
- fa-shopping-cart (🛒)
- fa-user (👤)

### Actions:
- fa-eye (👁️) - Quick View
- fa-plus (+) - Increase
- fa-minus (-) - Decrease
- fa-times (×) - Close/Remove
- fa-trash (🗑️) - Delete
- fa-sign-in-alt - Login
- fa-sign-out-alt - Logout
- fa-user-plus - Register

### Products:
- fa-chair, fa-couch, fa-bed - Furniture
- fa-lamp, fa-lightbulb, fa-chandelier - Lighting
- fa-image, fa-palette, fa-frame - Wall Art
- fa-gift, fa-vase, fa-candle - Accessories

### Status:
- fa-star (⭐) - Rating
- fa-star-half-alt (⭐) - Half star
- fa-check (✓) - Success
- fa-exclamation (!) - Warning

---

## Accessibility Features

### Implemented:
- Semantic HTML structure
- Proper button elements
- Form labels
- Alt text for icons
- Focus indicators on interactive elements
- Color contrast compliance

### To Be Implemented:
- ARIA labels for screen readers
- Keyboard navigation (ESC to close)
- Focus trap in modals
- Announcement regions for dynamic content
- Skip links

---

## Browser Support

✅ Chrome 90+
✅ Firefox 88+
✅ Safari 14+
✅ Edge 90+
✅ Opera 76+

**Features Used:**
- CSS Grid
- CSS Flexbox
- CSS Custom Properties
- ES6+ JavaScript
- LocalStorage API
- Fetch API (for future backend integration)

---

## Performance Metrics

**Target Metrics:**
- Modal Open Time: < 300ms
- Cart Update: < 100ms
- Wishlist Update: < 100ms
- Page Load Time: < 2s
- First Contentful Paint: < 1s

**Optimization Techniques:**
- CSS animations (GPU accelerated)
- Minimal DOM manipulation
- LocalStorage for client-side data
- Debounced event handlers (where needed)
- Lazy loading of modal content

---

## File Structure

```
project/
├── claude.html              # Main HTML file with all features
├── frontend-integration.js  # API integration functions
├── FEATURES_GUIDE.md       # Feature documentation
├── TESTING_GUIDE.md        # Testing procedures
├── UI_COMPONENTS_GUIDE.md  # This file
└── DecorHaven.API/         # Backend C# API
```

---

## Quick Reference: CSS Classes

### Modals:
- `.modal-overlay` - Full screen overlay
- `.modal` - Modal container
- `.modal-close` - Close button

### Quick View:
- `.quick-view-content` - Main container
- `.quick-view-image` - Product image area
- `.quick-view-details` - Product info area
- `.quantity-selector` - Quantity controls

### Account:
- `.account-content` - Main container
- `.account-tabs` - Tab navigation
- `.account-tab` - Individual tab button
- `.tab-content` - Tab content area
- `.user-profile-info` - Profile info card

### Cart:
- `.cart-content` - Main container
- `.cart-items` - Items list
- `.cart-item` - Individual item
- `.cart-summary` - Summary section
- `.cart-empty` - Empty state

### Wishlist:
- `.wishlist-content` - Main container
- `.wishlist-grid` - Items grid
- `.wishlist-item` - Individual item
- `.wishlist-empty` - Empty state

---

## Best Practices

### Adding New Features:
1. Follow existing naming conventions
2. Use CSS custom properties for colors
3. Add responsive breakpoints
4. Include loading states
5. Add error handling
6. Write accessible markup

### Modifying Existing Features:
1. Test across all breakpoints
2. Check console for errors
3. Verify localStorage compatibility
4. Test with empty states
5. Update documentation

### Code Style:
- Use camelCase for JavaScript
- Use kebab-case for CSS classes
- Add comments for complex logic
- Keep functions small and focused
- Use semantic HTML

---

This guide provides a comprehensive visual reference for all UI components in the e-commerce platform.

