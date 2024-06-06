using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("Age")]
public class AgeController : ControllerBase{
    private readonly DataContext _context;
    private static List<Age>Ages=new List<Age>{};
    public AgeController (DataContext context){
        _context = context;
    }
    //Obtengo todas las edades
    [HttpGet]
    [Route("GetAllAges")]
    public ActionResult<List<Age>>Get(){
        return Ok(_context.Ages);
    }
    //Obtencion de una edad
    [HttpGet]
    [Route("GetOneAge")]
    public ActionResult<List<Age>>GetAgeById(int Id)
    {
        var age = _context.Ages.Find(Id);
        return age==null ? NotFound() : Ok(age);
    }
    //Introducir una nueva edad
    [HttpPost]
    [Route("PostAge")]
    public ActionResult CreateAge(Age ageItem)
    {
        var existingAge =_context.Ages.Find(ageItem.Id);
        if (existingAge != null)
        {
            return Conflict("We alredy have an age with that id");
        }else
        {
            _context.Ages.Add(ageItem);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/"+ ageItem.Id;
            return Created(resourceUrl,ageItem);
        }
   
    }
//Modificar una edad
    [HttpPut]
    [Route("PutAge")]
    public ActionResult UpdateAge(Age ageItem)
    {
        var existingAge = _context.Ages.Find(ageItem.Id);
        if (existingAge == null)
        {
            return NotFound("We dont have an age with that id");
        }
        else
        {
             if (ageItem.Name!="string")
            {
                existingAge.Name = ageItem.Name;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
    //Eliminar una edad
    [HttpDelete]
    [Route("DeleteAge")]
    public ActionResult<Age> DeleteAge(int Id)
    {
        var existingAge = _context.Ages.Find(Id);
        if (existingAge == null)
        {
            return NotFound();
        }
        else
        {
            _context.Ages.Remove(existingAge);
            _context.SaveChanges();
            return NoContent();
        }
    }
}