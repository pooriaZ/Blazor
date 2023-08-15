namespace BlazorApp1.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductsChanged;
        List<Products> Products { get; set; }
        Task GetProducts(string? categoryUrl = null);
        Task<ServiceResponse<Products>> GetProduct(int productId);
    }
}
