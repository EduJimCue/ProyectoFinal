using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("Colour")]
public class ColourController : ControllerBase{
    private readonly DataContext _context;
    private static List<Colour>Colours=new List<Colour>{};
    public ColourController (DataContext context){
        _context = context;
    }
    //Obtengo todos los colores
    [HttpGet]
    [Route("GetAllColours")]
    public ActionResult<List<Colour>>Get(){
        return Ok(_context.Colours);
    }
    //Obtencion de un color
    [HttpGet]
    [Route("GetOneColour")]
    public ActionResult<List<Colour>>GetColourById(int Id)
    {
        var colour = _context.Colours.Find(Id);
        return colour==null ? NotFound() : Ok(colour);
    }
    //Introducir un nuevo color
    [HttpPost]
    [Route("PostColour")]
    public ActionResult CreateColour(Colour colourItem)
    {
        var existingColour =_context.Colours.Find(colourItem.Id);
        if (existingColour != null)
        {
            return Conflict("We alredy have a colour with that id");
        }else
        {
            _context.Colours.Add(colourItem);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/"+ colourItem.Id;
            return Created(resourceUrl,colourItem);
        }
   
    }
//Modificar un color
    [HttpPut]
    [Route("PutColour")]
    public ActionResult UpdateColour(Colour colourItem)
    {
        var existingColour = _context.Colours.Find(colourItem.Id);
        if (existingColour == null)
        {
            return NotFound("We dont have a colour with that id");
        }
        else
        {
             if (colourItem.Name!="string")
            {
                existingColour.Name = colourItem.Name;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
    //Eliminar un color
    [HttpDelete]
    [Route("DeleteColour")]
    public ActionResult<Colour> DeleteColour(int Id)
    {
        var existingColour = _context.Colours.Find(Id);
        if (existingColour == null)
        {
            return NotFound();
        }
        else
        {
            _context.Colours.Remove(existingColour);
            _context.SaveChanges();
            return NoContent();
        }
    }
}