namespace BlazorApp1.Client.Services.ProductService
{
    public interface IProductService
    {
        List<Products> Products { get; set; }
        Task GetProducts();
    }
}
