namespace BlazorApp1.Server.Services.ProductServices
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Products>>> GetProductsAsync();
    }
}
