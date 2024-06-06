using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("Size")]
public class SizeController : ControllerBase{
    private readonly DataContext _context;
    private static List<Size>Size=new List<Size>{};
    public SizeController (DataContext context){
        _context = context;
    }
    //Obtengo todas las tallas
    [HttpGet]
    [Route("GetAllSizes")]
    public ActionResult<List<Gender>>Get(){
        return Ok(_context.Sizes);
    }
    //Obtencion de una tala
    [HttpGet]
    [Route("GetOneSize")]
    public ActionResult<List<Size>>GetSizeById(int Id)
    {
        var size = _context.Sizes.Find(Id);
        return size==null ? NotFound() : Ok(size);
    }
    //Introducir una nueva talla
    [HttpPost]
    [Route("PostSize")]
    public ActionResult CreateSize(Size sizeItem)
    {
        var existingSize =_context.Sizes.Find(sizeItem.Id);
        if (existingSize != null)
        {
            return Conflict("We alredy have a gender with that id");
        }else
        {
            _context.Sizes.Add(sizeItem);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/"+ sizeItem.Id;
            return Created(resourceUrl,sizeItem);
        }
   
    }
//Modificar una talla
    [HttpPut]
    [Route("PutSize")]
    public ActionResult UpdateSize(Size sizeItem)
    {
        var existingSize = _context.Sizes.Find(sizeItem.Id);
        if (existingSize == null)
        {
            return NotFound("We dont have a size with that id");
        }
        else
        {
             if (sizeItem.Name!="string")
            {
                existingSize.Name = sizeItem.Name;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
    //Eliminar una talla
    [HttpDelete]
    [Route("DeleteSize")]
    public ActionResult<Size> DeleteSize(int Id)
    {
        var existingSize = _context.Sizes.Find(Id);
        if (existingSize == null)
        {
            return NotFound();
        }
        else
        {
            _context.Sizes.Remove(existingSize);
            _context.SaveChanges();
            return NoContent();
        }
    }
}