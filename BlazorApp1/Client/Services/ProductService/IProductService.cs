namespace BlazorApp1.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductsChanged;
        List<Products> Products { get; set; }
        string Message { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }
        Task GetProducts(string? categoryUrl = null);
        Task<ServiceResponse<Products>> GetProduct(int productId);
        Task SearchProducts(string searchText, int page);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
    }
}
