using Producer.Entities;

namespace Producer.Data;

public static class Data
{
    public static List<Product> Products { get; } = new()
    {
        new Product { Id = Guid.NewGuid(), ProductName = "Apple iPhone 13", Price = 999 },
        new Product { Id = Guid.NewGuid(), ProductName = "Samsung Galaxy S21", Price = 799 },
        new Product { Id = Guid.NewGuid(), ProductName = "Google Pixel 6", Price = 599 },
        new Product { Id = Guid.NewGuid(), ProductName = "OnePlus 9", Price = 729 },
        new Product { Id = Guid.NewGuid(), ProductName = "Sony WH-1000XM4", Price = 349 },
        new Product { Id = Guid.NewGuid(), ProductName = "Apple MacBook Pro", Price = 1299 },
        new Product { Id = Guid.NewGuid(), ProductName = "Dell XPS 13", Price = 999 },
        new Product { Id = Guid.NewGuid(), ProductName = "HP Spectre x360", Price = 1199 },
        new Product { Id = Guid.NewGuid(), ProductName = "Microsoft Surface Laptop 4", Price = 999 },
        new Product { Id = Guid.NewGuid(), ProductName = "Asus ROG Strix", Price = 1499 },
        new Product { Id = Guid.NewGuid(), ProductName = "Nintendo Switch", Price = 299 },
        new Product { Id = Guid.NewGuid(), ProductName = "Sony PlayStation 5", Price = 499 },
        new Product { Id = Guid.NewGuid(), ProductName = "Xbox Series X", Price = 499 },
        new Product { Id = Guid.NewGuid(), ProductName = "Apple iPad Pro", Price = 799 },
        new Product { Id = Guid.NewGuid(), ProductName = "Samsung Galaxy Tab S7", Price = 649 },
        new Product { Id = Guid.NewGuid(), ProductName = "Kindle Paperwhite", Price = 129 },
        new Product { Id = Guid.NewGuid(), ProductName = "Fitbit Charge 5", Price = 179 },
        new Product { Id = Guid.NewGuid(), ProductName = "Garmin Fenix 6", Price = 699 },
        new Product { Id = Guid.NewGuid(), ProductName = "Bose QuietComfort 35 II", Price = 299 },
        new Product { Id = Guid.NewGuid(), ProductName = "JBL Flip 5", Price = 119 }
    };
}