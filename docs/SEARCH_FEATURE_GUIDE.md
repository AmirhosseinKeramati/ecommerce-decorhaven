# Search Feature Guide

## Overview
The search feature provides comprehensive product search functionality with real-time results, category filtering, sorting options, and search history tracking.

---

## Features

### 1. **Search Bar**
- Large, prominent search input field
- Search button for manual search
- Real-time search as you type (2+ characters)
- Enter key to search
- Auto-focus when modal opens

### 2. **Search History**
- Tracks last 5 searches
- Stored in localStorage
- Displays when search input is empty
- Click to re-search previous queries
- Automatic deduplication

### 3. **Category Filters**
- Filter by product category
- Visual buttons with icons:
  - All (default)
  - Furniture 🪑
  - Lighting 💡
  - Wall Art 🎨
  - Accessories 🎁
- Active state indication
- Works with search query

### 4. **Sort Options**
- Sort results by:
  - Relevance (default)
  - Price: Low to High
  - Price: High to Low
  - Name: A to Z
  - Rating
- Dropdown selection
- Maintains filter when sorting

### 5. **Search Results**
- Grid layout of matching products
- Each result shows:
  - Product image/icon
  - Category tag
  - Product name
  - Price
  - Quick actions (Add to Cart, View)
- Scrollable results area
- Responsive grid

### 6. **No Results State**
- Friendly message when no matches found
- Popular search suggestions
- Click suggestions to search

### 7. **Real-time Search**
- Searches as you type (debounced 300ms)
- Minimum 2 characters to trigger
- Shows results instantly
- Smooth transitions

---

## How to Use

### Opening Search
1. Click the **search icon** (🔍) in the navigation bar
2. Search modal opens with search bar focused

### Searching
1. **Type your query** in the search bar
   - Or select from search history
   - Or click a suggestion
2. Press **Enter** or click the **search button**
3. Results appear below

### Filtering Results
1. Click any **category button** to filter
2. Results update immediately
3. Filter works with search query

### Sorting Results
1. Select sort option from **dropdown**
2. Results reorder instantly

### From Search Results
- **Add to Cart**: Click cart icon (🛒)
- **Quick View**: Click "View" button
- Both actions work seamlessly

---

## User Interface

### Search Modal Layout
```
┌──────────────────────────────────────────────────┐
│  🔍 Search Products                        [X]   │
├──────────────────────────────────────────────────┤
│                                                  │
│  Recent Searches (if available)                  │
│  [history] [history] [history]                   │
│                                                  │
│  ┌─────────────────────────────────────┐ [🔍]   │
│  │  Search for products, categories... │        │
│  └─────────────────────────────────────┘        │
│                                                  │
│  [All] [🪑 Furniture] [💡 Lighting] [🎨 Art]    │
│                                                  │
│  5 Results Found          Sort by: [Relevance]  │
│  ─────────────────────────────────────────────   │
│                                                  │
│  ┌───────┐  ┌───────┐  ┌───────┐  ┌───────┐   │
│  │ [Img] │  │ [Img] │  │ [Img] │  │ [Img] │   │
│  │ Name  │  │ Name  │  │ Name  │  │ Name  │   │
│  │ $599  │  │ $249  │  │ $189  │  │ $129  │   │
│  │[🛒][👁️]│  │[🛒][👁️]│  │[🛒][👁️]│  │[🛒][👁️]│   │
│  └───────┘  └───────┘  └───────┘  └───────┘   │
│                                                  │
└──────────────────────────────────────────────────┘
```

### Search Result Card
```
┌──────────────┐
│   ┌──────┐   │
│   │      │   │  ← Product Icon/Image
│   │ Icon │   │
│   └──────┘   │
│              │
│  FURNITURE   │  ← Category
│  Chair Name  │  ← Product Name
│  $599.00     │  ← Price
│              │
│  [🛒] [View] │  ← Actions
└──────────────┘
```

---

## Technical Implementation

### Data Structure

#### Search History (localStorage)
```javascript
[
  "modern chair",
  "table lamp",
  "wall art",
  "ceramic vase",
  "mirror"
]
```

### Key Functions

#### Search Functions
```javascript
performSearch()              // Execute search query
filterSearchResults(cat)     // Filter by category
sortSearchResults()          // Sort results
displaySearchResults()       // Render results
```

#### History Functions
```javascript
getSearchHistory()           // Get from localStorage
saveSearchHistory(query)     // Save search term
displaySearchHistory()       // Show history
searchFromHistory(query)     // Search from history
```

#### Action Functions
```javascript
addToCartFromSearch(id)      // Add product to cart
quickViewFromSearch(id)      // Open quick view
searchSuggestion(query)      // Search suggestion
```

---

## Search Algorithm

### Basic Search
Searches in product properties:
- Product name (case-insensitive)
- Product category (case-insensitive)

```javascript
searchResults = Object.values(sampleProducts).filter(product => {
    const searchLower = query.toLowerCase();
    return (
        product.name.toLowerCase().includes(searchLower) ||
        product.category.toLowerCase().includes(searchLower)
    );
});
```

### Category Filter
Applied after basic search:
```javascript
if (currentSearchCategory !== 'all') {
    searchResults = searchResults.filter(product => 
        product.category.toLowerCase().replace(' ', '-') === currentSearchCategory
    );
}
```

### Sort Logic
```javascript
switch (sortBy) {
    case 'price-low':
        searchResults.sort((a, b) => a.price - b.price);
        break;
    case 'price-high':
        searchResults.sort((a, b) => b.price - a.price);
        break;
    case 'name':
        searchResults.sort((a, b) => a.name.localeCompare(b.name));
        break;
    case 'rating':
        searchResults.sort((a, b) => b.rating - a.rating);
        break;
}
```

---

## Keyboard Shortcuts

- **Enter** - Execute search
- **Esc** - Close modal (to be implemented)
- **Tab** - Navigate through results

---

## Performance Features

### Debouncing
Real-time search uses 300ms debounce to reduce unnecessary searches:
```javascript
let searchTimeout;
searchInput.addEventListener('input', function() {
    clearTimeout(searchTimeout);
    searchTimeout = setTimeout(() => {
        if (this.value.length >= 2) {
            performSearch();
        }
    }, 300);
});
```

### Minimum Characters
Requires 2+ characters before searching to avoid too many results

### History Limit
Keeps only last 5 searches to save space

---

## Responsive Design

### Desktop (> 768px)
- 4 columns grid
- Full-sized buttons and text
- Side-by-side results header

### Mobile (< 768px)
- 2 columns grid
- Compact buttons
- Stacked results header
- Smaller text and spacing

---

## Integration with Other Features

### Quick View
Clicking "View" button on a search result:
1. Closes search modal
2. Opens quick view modal
3. Displays product details

### Shopping Cart
Clicking cart icon on a search result:
1. Adds product to cart
2. Shows success notification
3. Updates cart count badge
4. Keeps search modal open

---

## Sample Searches to Try

### By Product Name:
- "chair"
- "lamp"
- "vase"
- "mirror"
- "bed"

### By Category:
- "furniture"
- "lighting"
- "wall art"
- "accessories"

### Partial Matches:
- "mod" (finds Modern Velvet Armchair, Modern Chandelier)
- "crystal" (finds Crystal Table Lamp)
- "luxury" (finds Luxury Bed Frame, Luxury Candle Set)

---

## Future Enhancements

### Planned Features:
- [ ] Advanced filters (price range, rating)
- [ ] Search suggestions dropdown
- [ ] Fuzzy matching for typos
- [ ] Search highlighting in results
- [ ] Voice search
- [ ] Image search
- [ ] Search analytics
- [ ] Related products
- [ ] Search by color/material
- [ ] Recently viewed products

### Backend Integration:
When integrated with API:
```javascript
async function performSearch() {
    const query = document.getElementById('searchInput').value.trim();
    const response = await fetch(
        `${API_BASE_URL}/products/search?q=${encodeURIComponent(query)}`
    );
    const data = await response.json();
    searchResults = data.data;
    displaySearchResults();
}
```

---

## Troubleshooting

### Search Not Working
1. Check if search input has focus
2. Verify at least 2 characters entered
3. Check browser console for errors
4. Clear search history: `localStorage.removeItem('searchHistory')`

### No Results Found
1. Check spelling
2. Try different keywords
3. Use category filters
4. Try popular suggestions

### History Not Saving
1. Check if localStorage is enabled
2. Verify browser supports localStorage
3. Check for private/incognito mode

### Results Not Displaying
1. Check console for JavaScript errors
2. Verify product data is loaded
3. Clear browser cache
4. Refresh page

---

## Accessibility

### Implemented:
- Focus management (auto-focus search input)
- Keyboard navigation (Enter to search)
- Clear visual feedback
- Readable text sizes
- Sufficient color contrast

### To Be Implemented:
- ARIA labels for screen readers
- Keyboard navigation through results
- Escape key to close
- Focus trap in modal
- Announcement of result count

---

## CSS Classes Reference

### Main Containers:
- `.search-content` - Main search modal content
- `.search-bar` - Search input container
- `.search-filters` - Category filter buttons
- `.search-results` - Results grid container

### Results:
- `.search-result-item` - Individual result card
- `.search-result-image` - Product image area
- `.search-result-info` - Product details
- `.search-result-actions` - Action buttons

### States:
- `.search-no-results` - No results message
- `.search-history` - History section
- `.search-suggestions` - Popular searches
- `.active` - Active filter button

---

## Code Examples

### Opening Search Programmatically:
```javascript
openSearchModal();
```

### Search Specific Query:
```javascript
document.getElementById('searchInput').value = 'chair';
performSearch();
```

### Clear Search:
```javascript
document.getElementById('searchInput').value = '';
document.getElementById('searchResults').innerHTML = '';
```

### Clear Search History:
```javascript
localStorage.removeItem('searchHistory');
```

---

## Statistics

### Code Added:
- CSS: ~350 lines
- HTML: ~80 lines
- JavaScript: ~250 lines
- **Total: ~680 lines**

### Features Count:
- Search modes: 2 (manual, real-time)
- Category filters: 5 (all + 4 categories)
- Sort options: 5
- History items: 5 (max)
- Popular suggestions: 5

---

## Browser Support

✅ Chrome 90+
✅ Firefox 88+
✅ Safari 14+
✅ Edge 90+
✅ Mobile browsers

**Features Used:**
- ES6+ JavaScript
- CSS Grid
- LocalStorage API
- Modern event listeners

---

## Conclusion

The search feature provides a complete, user-friendly search experience with:
- Fast real-time search
- Flexible filtering and sorting
- Search history tracking
- Responsive design
- Seamless integration with other features

Perfect for helping users quickly find the products they're looking for!

