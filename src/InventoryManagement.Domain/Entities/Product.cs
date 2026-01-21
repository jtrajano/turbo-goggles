// Domain/Entities/Product.cs
namespace InventoryManagement.Domain.Entities
{
    public class Product : IBaseEntity
    {
        public Guid Id { get; set; }
        public string SKU { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public string Category { get; private set; } = string.Empty;
        public string? Supplier { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal CostPrice { get; private set; }
        public int CurrentStock { get; private set; }
        public int ReorderLevel { get; private set; }
        public int MaxStockLevel { get; private set; }
        public string? Unit { get; private set; }
        public string? ImageUrl { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public string? CreatedBy { get; private set; }
        public string? UpdatedBy { get; private set; }

        // Private constructor for EF Core
        private Product() { }

        // Factory method for creating new products
        public static Product Create(
            string sku,
            string name,
            string category,
            decimal unitPrice,
            decimal costPrice,
            int currentStock,
            int reorderLevel,
            int maxStockLevel,
            string? description = null,
            string? supplier = null,
            string? unit = null,
            string? barcode = null,
            string? imageUrl = null,
            string? createdBy = null)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                SKU = sku,
                Name = name,
                Description = description,
                Category = category,
                Supplier = supplier,
                UnitPrice = unitPrice,
                CostPrice = costPrice,
                CurrentStock = currentStock,
                ReorderLevel = reorderLevel,
                MaxStockLevel = maxStockLevel,
                Unit = unit,
                ImageUrl = imageUrl,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = createdBy
            };

            return product;
        }

        // Business methods
        public void Update(
            string sku,
            string name,
            string category,
            decimal unitPrice,
            decimal costPrice,
            int reorderLevel,
            int maxStockLevel,
            string? description = null,
            string? supplier = null,
            string? unit = null,
            string? barcode = null,
            string? imageUrl = null,
            string? updatedBy = null)
        {
            SKU = sku;
            Name = name;
            Description = description;
            Category = category;
            Supplier = supplier;
            UnitPrice = unitPrice;
            CostPrice = costPrice;
            ReorderLevel = reorderLevel;
            MaxStockLevel = maxStockLevel;
            Unit = unit;
            ImageUrl = imageUrl;
            UpdatedAt = DateTime.UtcNow;
            UpdatedBy = updatedBy;
        }

        public void UpdateStock(int quantity)
        {
            CurrentStock = quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            IsActive = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool IsLowStock() => CurrentStock <= ReorderLevel;

        public decimal CalculateStockValue() => CurrentStock * CostPrice;
    }
}