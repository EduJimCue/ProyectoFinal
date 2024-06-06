using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("Product")]
public class ProductController : ControllerBase{
    private readonly DataContext _context;
    private static List<Product>Products=new List<Product>{};
    public ProductController (DataContext context){
        _context = context;
    }
    //Obtengo todos los productos
    [HttpGet]
    public ActionResult<List<Product>>Get(){
        return Ok(_context.Products);
    }
    //Obtencion de un producto
    [HttpGet]
    [Route("{Id}")]
    public ActionResult<List<Product>>GetProductById(int Id)
    {
        var product = _context.Products.Find(Id);
        return product==null ? NotFound() : Ok(product);
    }
    //Obtencion de los ultimos productos
    [HttpGet]
        [Route("LastFour")]
        public ActionResult<List<Product>> GetLastFourProducts()
        {
            var lastFourProducts = _context.Products
                                           .Where(p => p.TeamId == 0) 
                                           .OrderByDescending(p => p.Id) 
                                           .Take(4)
                                           .ToList();

            return lastFourProducts.Count == 0 ? NotFound() : Ok(lastFourProducts);
        }

        // Obtención de 4 productos aleatorios
        [HttpGet]
        [Route("RandomFour")]
        public ActionResult<List<Product>> GetRandomFourProducts()
        {
            var randomFourProducts = _context.Products
                                             .Where(p => p.TeamId == 0) 
                                             .OrderBy(p => Guid.NewGuid()) 
                                             .Take(4)
                                             .ToList();

            return randomFourProducts.Count == 0 ? NotFound() : Ok(randomFourProducts);
        }
    // Obtención de productos por nombre parcial
    [HttpGet]
    [Route("PartialName")]
    public ActionResult<List<Product>> GetProductsByPartialName(string partialName)
    {
        var products = _context.Products
            .Where(p => p.Name.Contains(partialName))
            .ToList();

        if (products == null || products.Count == 0)
        {
            return NotFound("No se encontraron productos con el nombre parcial proporcionado.");
        }

        return Ok(products);
    }
    //Obtencion de productos similares
    [HttpGet]
    [Route("GetSimilarProducts")]
    public ActionResult<List<Product>> GetSimilarProducts(int productId)
    {
        var product = _context.Products.Find(productId);
        var products = _context.Products
            .Where(p => p.CategoryId == product.CategoryId && p.SportId == product.SportId && p.AgeId == product.AgeId && p.GenderId == product.GenderId && p.TeamId == product.TeamId && p.Id != productId && p.IsActive == true)
            .OrderBy(x => Guid.NewGuid())
            .Take(4)
            .ToList();

        if (products == null || products.Count == 0)
        {
            return NotFound("No se encontraron productos similares.");
        }

        return Ok(products);
    }
    //Obtencion de mismo producto con distinto color
    [HttpGet]
    [Route("GetSimilarProductsByFullNameAndDifferentColors")]
    public ActionResult<List<Product>> GetSimilarProductsByFullNameAndDifferentColors(string name, int ColourId)
    {
        var products = _context.Products
            .Where(p => p.Name == name && p.ColourId != ColourId)
            .Take(4)
            .ToList();

        if (products == null || products.Count == 0)
        {
            return NotFound("No se encontraron productos similares con el nombre completo y colores diferentes.");
        }

        return Ok(products);
    }
    [HttpGet]
    [Route("Filter")]
    public ActionResult<List<Product>> GetFilteredProductsAndFilters([FromQuery] InputFilter filters)
    {
        if (filters.TeamId == null)
        {
            filters.TeamId = 0;
        }
        var filteredProducts = _context.Products
            .Where(p =>
                (p.IsActive == true) &&
                (filters.BrandId == null || p.BrandId == filters.BrandId) &&
                (filters.SportId == null || p.SportId == filters.SportId) &&
                (filters.CategoryId == null || p.CategoryId == filters.CategoryId) &&
                (filters.ColourId == null || p.ColourId == filters.ColourId) &&
                (filters.TeamId == null || p.TeamId == filters.TeamId) &&
                (filters.AgeId == null || p.AgeId == filters.AgeId) &&
                (filters.GenderId == null || p.GenderId == filters.GenderId) &&
                (filters.SizeId == null || _context.SizeProducts.Any(sp => sp.ProductId == p.Id && sp.SizeId == filters.SizeId && sp.Stock > 0))
            )
            .ToList();
    
        if (filteredProducts.Count == 0)
        {
            return Ok(filteredProducts);
        }
    
        var availableFilters = new Filters();
    
        foreach (var product in filteredProducts)
        {
            if (product.BrandId.HasValue && filters.BrandId == null && !availableFilters.BrandsIds.Contains(product.BrandId.Value))
                availableFilters.BrandsIds.Add(product.BrandId.Value);
            if (product.SportId.HasValue && filters.SportId == null & !availableFilters.SportsIds.Contains(product.SportId.Value))
                availableFilters.SportsIds.Add(product.SportId.Value);
            if (product.CategoryId.HasValue && filters.CategoryId == null & !availableFilters.CategoriesIds.Contains(product.CategoryId.Value))
                availableFilters.CategoriesIds.Add(product.CategoryId.Value);
            if (product.ColourId.HasValue && filters.ColourId == null && !availableFilters.ColourIds.Contains(product.ColourId.Value))
                availableFilters.ColourIds.Add(product.ColourId.Value);
            if (product.TeamId.HasValue && filters.TeamId == null && !availableFilters.TeamsIds.Contains(product.TeamId.Value))
                availableFilters.TeamsIds.Add(product.TeamId.Value);
            if (product.AgeId.HasValue && filters.AgeId == null && !availableFilters.AgesIds.Contains(product.AgeId.Value))
                availableFilters.AgesIds.Add(product.AgeId.Value);
            if (product.GenderId.HasValue && filters.GenderId == null && !availableFilters.GendersIds.Contains(product.GenderId.Value))
                availableFilters.GendersIds.Add(product.GenderId.Value);
        }
    
        return Ok(new { Products = filteredProducts, Filters = availableFilters });
    }


    //Introducir un nuevo producto
    [HttpPost]
    public ActionResult CreateProduct(Product productItem)
    {
        var existingProduct =_context.Products.Find(productItem.Id);
        if (existingProduct != null)
        {
            return Conflict("We alredy have a product with that id");
        }else
        {
            _context.Products.Add(productItem);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/"+ productItem.Id;
            return Created(resourceUrl,productItem);
        }
    
    }
    //Modificar un producto
    [HttpPut]
    public ActionResult UpdateProduct(Product productItem)
    {
        var existingProduct = _context.Products.Find(productItem.Id);
        if (existingProduct == null)
        {
            return NotFound("We dont have a product with that id");
        }
        else
        {
            existingProduct.IsActive=productItem.IsActive;
            if (productItem.Name!="string")
            {
                existingProduct.Name = productItem.Name;
            }
            if (productItem.Price!=0)
            {
                existingProduct.Price = productItem.Price;
            }
            if (productItem.SportId!=0)
            {
                existingProduct.SportId = productItem.SportId;
            }
            if (productItem.BrandId!=0)
            {
                existingProduct.BrandId = productItem.BrandId;
            }
            if (productItem.CategoryId!=0)
            {
                existingProduct.CategoryId = productItem.CategoryId;
            }
            if (productItem.ColourId!=0)
            {
                existingProduct.ColourId = productItem.ColourId;
            }
            if (productItem.TeamId!=0)
            {
                existingProduct.TeamId = productItem.TeamId;
            }
            if (productItem.GenderId!=0)
            {
                existingProduct.GenderId = productItem.GenderId;
            }
            if (productItem.AgeId!=0)
            {
                existingProduct.AgeId = productItem.AgeId;
            }
            if (productItem.PrincipalImage!="string")
            {
                existingProduct.PrincipalImage = productItem.PrincipalImage;
            }
            if (productItem.SecondImage!="string")
            {
                existingProduct.SecondImage = productItem.SecondImage;
            }
            if (productItem.ThirdImage!="string")
            {
                existingProduct.ThirdImage = productItem.ThirdImage;
            }
            if (productItem.FourthImage!="string")
            {
                existingProduct.FourthImage = productItem.FourthImage;
            }
            if (productItem.Description!="string")
            {
                existingProduct.Description = productItem.Description;
            }
            _context.SaveChanges();
            return Ok();
        }
    
    }
    //Eliminar un producto
    [HttpDelete]
    public ActionResult<Product> DeleteProduct(int Id)
    {
        var existingProduct = _context.Products.Find(Id);
        if (existingProduct == null)
        {
            return NotFound();
        }
        else
        {
            //Primero borramos sus relacion de talla
            var sizeproducts = _context.SizeProducts.Where(x => x.ProductId == existingProduct.Id).ToList();
            if (sizeproducts != null)
            {
                foreach (var sizeproduct in sizeproducts)
                {
                    _context.SizeProducts.Remove(sizeproduct);
                    _context.SaveChanges();
                }
            }

            _context.Products.Remove(existingProduct);
            _context.SaveChanges();
            return NoContent();
        }
    }
}