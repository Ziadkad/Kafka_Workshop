using TaxService.Entities;

namespace TaxService.StaticClasses;

public static class StaticProduct
{
    public static List<Product> Products { get; private set; } = new();
    public static TaskCompletionSource Loading = new();

    
    
    public static void SetAndResetList(List<Product> products)
    {
        Products = new List<Product>();
        Products.AddRange(products);
        Loading.TrySetResult(); 
    }
    
    public static void ResetLoading()
    {
        Loading = new TaskCompletionSource();
    }
}