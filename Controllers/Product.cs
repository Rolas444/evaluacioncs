using apidotnet.Data;
using apidotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apidotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly ProductContext _context;
    public ProductsController(ProductContext context){
        _context  = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductById (int id){
        var product =  await _context.Products.FindAsync(id);
        if(product ==null)
        {
            return NotFound();
        }

        return product;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.ID)
        {
            return  BadRequest();
        }

        _context.Entry(product).State = EntityState.Modified;

        try{

            await _context.SaveChangesAsync();
        }catch(DbUpdateConcurrencyException){
            if (_context.Products.Any(e=>e.ID ==id)){
                throw;
            }else{
                return NotFound();
           }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetProduct", new {id=product.ID}, product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    }
}