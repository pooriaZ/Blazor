namespace BlazorApp1.Server.Services.ProductServices
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Products>>> GetProductsAsync();
        Task<ServiceResponse<Products>> GetProductAsync(int productId);
        Task<ServiceResponse<List<Products>>> GetProductsByCategory(string categoryUrl);
        Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText, int page);
        Task<ServiceResponse<List<string>>> GetProuctSearchSuggestions(string searchText);
        Task<ServiceResponse<List<Products>>> GetFeaturedProducts();
    }
}
