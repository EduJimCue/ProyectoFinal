using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("SizeProduct")]
public class SizeProductController : ControllerBase{
    private readonly DataContext _context;
    private static List<Order>SizeProducts=new List<Order>{};
    public SizeProductController (DataContext context){
        _context = context;
    }
    //Obtengo todos los productos con sus tallas
    [HttpGet]
    [Route("GetAllSizeProduct")]
    public ActionResult<List<SizeProduct>>Get(){
        return Ok(_context.SizeProducts);
    }
    //Obtencion de un equipo
    [HttpGet]
    [Route("GetOneSizeProduct")]
    public ActionResult<SizeProduct>GetOneSizeProduct(int Id)
    {
        var sizeProduct = _context.SizeProducts.Find(Id);
        return sizeProduct==null ? NotFound() : Ok(sizeProduct);
    }
    //Obtencion de un producto con talla por su id
    [HttpGet]
    [Route("GetOneOrder")]
    public ActionResult<SizeProduct> GetSizesProductById(int productId, int sizeId)
    {
        var sizeProduct = _context.SizeProducts.FirstOrDefault(sp => sp.ProductId == productId && sp.SizeId == sizeId);
        return sizeProduct == null ? NotFound() : Ok(sizeProduct);
    }
    
    //Obtencion de las tallas disponibles de un producto
    [HttpGet]
    [Route("GetSizesByProduct")]
    public ActionResult<List<string>> GetSizesByProduct(int productId)
    {
        var sizeIds = _context.SizeProducts
            .Where(sp => sp.ProductId == productId && sp.Stock > 0)
            .Select(sp => sp.SizeId)
            .ToList();

        var sizeNames = _context.Sizes
            .Where(s => sizeIds.Contains(s.Id))
            .ToList();

        return sizeNames.Count == 0 ? NotFound() : Ok(sizeNames);
    }

    //Obtencion del stock disponible para un producto y una talla
    [HttpGet]
    [Route("GetStock")]
    public ActionResult<List<string>> GetStock(int productId, int sizeId)
    {
        var stock = _context.SizeProducts
            .Where(sp => sp.ProductId == productId && sp.SizeId == sizeId)
            .Select(sp => sp.Stock)
            .ToList();

        return stock.Count == 0 ? NotFound() : Ok(stock);
    }

    //Introducir una nueva relacion de producto y talla
    [HttpPost]
    [Route("PostSizeProduct")]
    public ActionResult CreateSizeProduct(SizeProduct sizeProduct)
    {
        var existingSizeProduct = _context.SizeProducts.FirstOrDefault(sp => sp.ProductId == sizeProduct.ProductId && sp.SizeId == sizeProduct.SizeId);

        if (existingSizeProduct != null)
        {
            sizeProduct.Id = existingSizeProduct.Id; 
            return UpdateSizeProduct(sizeProduct); 
        }
        else
        {
            _context.SizeProducts.Add(sizeProduct);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/" + sizeProduct.Id;
            return Created(resourceUrl, sizeProduct);
        }
    }
    //Modificar una relacion de producto y talla(Solo el stock?)
    [HttpPut]
    [Route("PutSizeProduct")]
    public ActionResult UpdateSizeProduct(SizeProduct sizeProductItem)
    {
        var existingSizeProduct = _context.SizeProducts.Find(sizeProductItem.Id);
        if (existingSizeProduct == null)
        {
            return NotFound("We dont have a relation of size and product with that id");
        }
        else
        {
            existingSizeProduct.Stock = sizeProductItem.Stock;
            
            if (sizeProductItem.ProductId!=0)
            {
                existingSizeProduct.ProductId = sizeProductItem.ProductId;
            }
            if (sizeProductItem.SizeId!=0)
            {
                existingSizeProduct.SizeId = sizeProductItem.SizeId;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
    //Eliminar una relacion de tallas y productos
    [HttpDelete]
    [Route("DeleteSizeProduct")]
    public ActionResult<SizeProduct> DeleteSizeProduct(int Id)
    {
        var existingSizeProduct = _context.SizeProducts.Find(Id);
        if (existingSizeProduct == null)
        {
            return NotFound();
        }
        else
        {
            _context.SizeProducts.Remove(existingSizeProduct);
            _context.SaveChanges();
            return NoContent();
        }
    }
}