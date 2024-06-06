using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("Gender")]
public class GenderController : ControllerBase{
    private readonly DataContext _context;
    private static List<Gender>Gender=new List<Gender>{};
    public GenderController (DataContext context){
        _context = context;
    }
    //Obtengo todos los sexos
    [HttpGet]
    [Route("GetAllGenders")]
    public ActionResult<List<Gender>>Get(){
        return Ok(_context.Genders);
    }
    //Obtencion de un sexo
    [HttpGet]
    [Route("GetOneGender")]
    public ActionResult<List<Gender>>GetGenderById(int Id)
    {
        var gender = _context.Genders.Find(Id);
        return gender==null ? NotFound() : Ok(gender);
    }
    //Introducir un nuevo sexo
    [HttpPost]
    [Route("PostGender")]
    public ActionResult CreateGender(Gender genderItem)
    {
        var existingGender =_context.Genders.Find(genderItem.Id);
        if (existingGender != null)
        {
            return Conflict("We alredy have a gender with that id");
        }else
        {
            _context.Genders.Add(genderItem);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/"+ genderItem.Id;
            return Created(resourceUrl,genderItem);
        }
   
    }
//Modificar un sexo
    [HttpPut]
    [Route("PutGender")]
    public ActionResult UpdateGender(Gender genderItem)
    {
        var existingGender = _context.Genders.Find(genderItem.Id);
        if (existingGender == null)
        {
            return NotFound("We dont have a gender with that id");
        }
        else
        {
             if (genderItem.Name!="string")
            {
                existingGender.Name = genderItem.Name;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
    //Eliminar un sexo
    [HttpDelete]
    [Route("DeleteGender")]
    public ActionResult<Gender> DeleteGender(int Id)
    {
        var existingGender = _context.Genders.Find(Id);
        if (existingGender == null)
        {
            return NotFound();
        }
        else
        {
            _context.Genders.Remove(existingGender);
            _context.SaveChanges();
            return NoContent();
        }
    }
}