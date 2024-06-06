using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("ImageProduct")]
public class ImageProductController : ControllerBase{
    private readonly DataContext _context;
    private static List<Order>ImageProducts=new List<Order>{};
    public ImageProductController (DataContext context){
        _context = context;
    }
    //Obtengo todos los productos con sus imagenes
    [HttpGet]
    [Route("GetAllImageProduct")]
    public ActionResult<List<ImageProduct>>Get(){
        return Ok(_context.ImageProducts);
    }
    //Obtencion de un producto con imagen por su id
    [HttpGet]
    [Route("GetOneProduct")]
    public ActionResult<List<ImageProduct>>GetImageProductById(int Id)
    {
        var imageProduct = _context.ImageProducts.Find(Id);
        return imageProduct ==null ? NotFound() : Ok(imageProduct);
    }
    
    //Obtencion de las imagenes relacionadas con un producto
    [HttpGet]
    [Route("GetImagesByProduct")]
    public ActionResult<List<string>> GetImagesByProduct(int productId)
    {
        var imageUrls = _context.ImageProducts
            .Where(ip => ip.ProductId == productId)
            .Select(ip => ip.ImageId)
            .ToList()
            .Select(imageId => _context.Images.FirstOrDefault(i => i.Id == imageId)?.Url)
            .ToList();
    
        if (imageUrls == null || imageUrls.Count == 0)
        {
            return NotFound("No se encontraron imágenes para el producto especificado.");
        }
    
        return Ok(imageUrls);
    }

    [HttpPut]
    [Route("UpdateImage")]
    public ActionResult UpdateImage(int productId, int oldImageId, int newImageId)
    {
        var imageProduct = _context.ImageProducts.FirstOrDefault(ip => ip.ProductId == productId && ip.ImageId == oldImageId);

        if (imageProduct == null)
        {
            return NotFound("No se encontró ninguna imagen asociada al producto con la URL proporcionada.");
        }

        imageProduct.ImageId = newImageId;
        _context.SaveChanges();

        return Ok("La URL de la imagen ha sido actualizada exitosamente.");
    }

    //Introducir una nueva relacion de producto e imagen
    [HttpPost]
    [Route("PostImageProduct")]
    public ActionResult CreateImageProduct(ImageProduct imageProduct)
    {
        var existingImageProduct =_context.ImageProducts.Find(imageProduct.Id);
        if (existingImageProduct != null)
        {
            return Conflict("We alredy have a relation of image and product with that id");
        }else
        {
            _context.ImageProducts.Add(imageProduct);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/"+ imageProduct.Id;
            return Created(resourceUrl,imageProduct);
        }
   
    }
    
    //Eliminar una relacion de imagenes y productos
    [HttpDelete]
    [Route("DeleteImageProduct")]
    public ActionResult<ImageProduct> DeleteImageProduct(int Id)
    {
        var existingImageProduct = _context.ImageProducts.Find(Id);
        if (existingImageProduct == null)
        {
            return NotFound();
        }
        else
        {
            _context.ImageProducts.Remove(existingImageProduct);
            _context.SaveChanges();
            return NoContent();
        }
    }
}