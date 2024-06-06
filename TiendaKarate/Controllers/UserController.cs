using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("User")]
public class UserController : ControllerBase
{
    private readonly DataContext _context;
    private static List<User> Users = new List<User> { };
    public UserController(DataContext context)
    {
        _context = context;
    }

    //Obtengo todos los usuarios
    [HttpGet]
    public ActionResult<List<User>> Get()
    {
        return Ok(_context.Users);
    }

    [HttpPost]
    public ActionResult Post(User user)
    {
        var existingUserName = _context.Users.FirstOrDefault(x => x.Name == user.Name);
        var existingUserEmail = _context.Users.FirstOrDefault(x => x.Email == user.Email);

        if (existingUserName != null && existingUserEmail != null)
        {
            return Conflict("There is already a user with that name and email.");
        }
        else if (existingUserName != null)
        {
            return Conflict("There is already a user with that name.");
        }
        else if (existingUserEmail != null)
        {
            return Conflict("There is already a user with that email.");
        }
        else
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/" + user.Id;
            return Created(resourceUrl, user);
        }
    }

    //Loguear usuario
    [HttpPost]
    [Route("{Name}/{Password}")]
    public ActionResult<User> Login(string Name, string Password)
    {
        User user = _context.Users.SingleOrDefault(u => (u.Name == Name || u.Email == Name) && u.Password == Password);
        return user == null ? NotFound("No existe un usuario con esas credenciales, por favor revise sus datos") : Ok(user);
    }

    //Borrar usuario
    [HttpDelete]
    [Route("{Id}")]
    public ActionResult<User> DeleteUser(int Id)
    {
        var existingUser = _context.Users.Find(Id);
        if (existingUser == null)
        {
            return NotFound();
        }
        else
        {
            var orders = _context.Orders.Where(x => x.UserId == existingUser.Id).ToList();
            if (orders != null)
            {
                foreach (var order in orders)
                {
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                }
            }

            _context.Users.Remove(existingUser);
            _context.SaveChanges();
            return NoContent();
        }
    }

    //Editar usuario
    [HttpPut]
    public ActionResult UpdateUser(User userItem)
    {
        var existingUser = _context.Users.Find(userItem.Id);
        if (existingUser == null)
        {
            return NotFound("We dont have an user with that Id");
        }
        else
        {
            if (userItem.Name != "string")
            {
                existingUser.Name = userItem.Name;
            }
            if (userItem.Email != "string")
            {
                existingUser.Email = userItem.Email;
            }
            if (userItem.Password != "string")
            {
                existingUser.Password = userItem.Password;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}