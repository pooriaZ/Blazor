namespace BlazorApp1.Server.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;
        public ProductService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<Products>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Products>>
            {
                Data = await _context.Products.ToListAsync()
            };
            return response;
        }
    }
}
