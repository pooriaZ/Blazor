namespace BlazorApp1.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public List<Products> Products { get; set; } = new List<Products>();

        public event Action ProductsChanged;

        public async Task<ServiceResponse<Products>> GetProduct(int productId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Products>>($"api/product/{productId}");
            return result;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Products>>>("api/product"):
                await _http.GetFromJsonAsync<ServiceResponse<List<Products>>>($"api/product/category/{categoryUrl}");
            if (result != null && result.Data != null) 
            {
                Products = result.Data;
            }

            ProductsChanged.Invoke();
        }
    }
}
