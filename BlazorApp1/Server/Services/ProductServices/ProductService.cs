namespace BlazorApp1.Server.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Products>> GetProductAsync(int productId)
        {
            var response = new ServiceResponse<Products>();
            var product = await _context.Products
                .Include(p => p.Variants)
                .ThenInclude(t => t.ProductType)
                .FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this product does not exist!";
            }
            else
            {
                response.Data = product;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Products>>> GetProductsAsync()
        {
            var response = new ServiceResponse<List<Products>>
            {
                Data = await _context.Products.Include(p => p.Variants).ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<Products>>> GetProductsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Products>>
            {
                Data = await _context.Products
                .Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
                .Include(p => p.Variants)
                .ToListAsync()
            };
            return response;
        }
    }
}
