using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("Team")]
public class TeamController : ControllerBase{
    private readonly DataContext _context;
    private static List<Team>Team=new List<Team>{};
    public TeamController (DataContext context){
        _context = context;
    }
    //Obtengo todos los equipos
    [HttpGet]
    [Route("GetAllTeams")]
    public ActionResult<List<Team>>Get(){
        return Ok(_context.Teams);
    }
    //Obtencion de un equipo
    [HttpGet]
    [Route("GetOneTeam")]
    public ActionResult<List<Sport>>GetTeamById(int Id)
    {
        var team = _context.Teams.Find(Id);
        return team==null ? NotFound() : Ok(team);
    }
    [HttpGet]
    [Route("VerifyTeamCredentials")]
    public ActionResult VerifyTeamCredentials(int teamId, string password)
    {
        var team = _context.Teams.Find(teamId);
        if (team == null)
        {
            return NotFound("Equipo no encontrado.");
        }
        if (team.Password != password)
        {
            return Unauthorized("Contraseña incorrecta.");
        }
        return Ok(team);
    }
    //Introducir un nuevo equipo
    [HttpPost]
    [Route("PostTeam")]
    public ActionResult CreateTeam(Team teamItem)
    {
        var existingTeam =_context.Teams.Find(teamItem.Id);
        if (existingTeam != null)
        {
            return Conflict("We alredy have a team with that id");
        }else
        {
            _context.Teams.Add(teamItem);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/"+ teamItem.Id;
            return Created(resourceUrl,teamItem);
        }
   
    }
    //Modificar un equipo
    [HttpPut]
    [Route("PutTeam")]
    public ActionResult UpdateTeam(Team teamItem)
    {
        var existingTeam = _context.Teams.Find(teamItem.Id);
        if (existingTeam == null)
        {
            return NotFound("We dont have a team with that id");
        }
        else
        {
             if (teamItem.Name!="string")
            {
                existingTeam.Name = teamItem.Name;
            }
            if (teamItem.Password!="string")
            {
                existingTeam.Password = teamItem.Password;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
    //Eliminar un equipo
    [HttpDelete]
    [Route("DeleteTeam")]
    public ActionResult<Team> DeleteTeam(int Id)
    {
        var existingTeam = _context.Teams.Find(Id);
        if (existingTeam == null)
        {
            return NotFound();
        }
        else
        {
            _context.Teams.Remove(existingTeam);
            _context.SaveChanges();
            return NoContent();
        }
    }
}