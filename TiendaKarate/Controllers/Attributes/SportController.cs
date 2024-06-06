using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("Sport")]
public class SportController : ControllerBase{
    private readonly DataContext _context;
    private static List<Sport>Sport=new List<Sport>{};
    public SportController (DataContext context){
        _context = context;
    }
    //Obtengo todos los deportes
    [HttpGet]
    [Route("GetAllSports")]
    public ActionResult<List<Sport>>Get(){
        return Ok(_context.Sports);
    }
    //Obtencion de un deporte
    [HttpGet]
    [Route("GetOneSport")]
    public ActionResult<List<Sport>>GetSportById(int Id)
    {
        var sport = _context.Sports.Find(Id);
        return sport==null ? NotFound() : Ok(sport);
    }
    //Introducir un nuevo deporte
    [HttpPost]
    [Route("PostSport")]
    public ActionResult CreateSport(Sport sportItem)
    {
        var existingSport =_context.Sports.Find(sportItem.Id);
        if (existingSport != null)
        {
            return Conflict("We alredy have a sport with that id");
        }else
        {
            _context.Sports.Add(sportItem);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/"+ sportItem.Id;
            return Created(resourceUrl,sportItem);
        }
   
    }
    //Modificar un deporte
    [HttpPut]
    [Route("PutSport")]
    public ActionResult UpdateSport(Sport sportItem)
    {
        var existingSport = _context.Sports.Find(sportItem.Id);
        if (existingSport == null)
        {
            return NotFound("We dont have a sport with that id");
        }
        else
        {
             if (sportItem.Name!="string")
            {
                existingSport.Name = sportItem.Name;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
    //Eliminar un deporte
    [HttpDelete]
    [Route("DeleteSport")]
    public ActionResult<Sport> DeleteSport(int Id)
    {
        var existingSport = _context.Sports.Find(Id);
        if (existingSport == null)
        {
            return NotFound();
        }
        else
        {
            _context.Sports.Remove(existingSport);
            _context.SaveChanges();
            return NoContent();
        }
    }
}