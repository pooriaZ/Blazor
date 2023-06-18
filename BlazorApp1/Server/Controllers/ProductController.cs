using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductController(DataContext context) 
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Products>>> GetProduct()
        {
            var products=await _context.Products.ToListAsync();
            return Ok(products);
        }
    }
}
