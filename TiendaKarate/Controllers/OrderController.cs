using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("Order")]
public class OrderController : ControllerBase{
    private readonly DataContext _context;
    private static List<Order>Orders=new List<Order>{};
    public OrderController (DataContext context){
        _context = context;
    }
    //Obtengo todos los pedidos
    [HttpGet]
    [Route("GetAllOrders")]
    public ActionResult<List<Order>>Get(){
        return Ok(_context.Orders);
    }
    //Obtencion de un pedido
    [HttpGet]
    [Route("GetOneOrder")]
    public ActionResult<List<Order>>GetOrderById(int Id)
    {
        var order = _context.Orders.Find(Id);
        return order==null ? NotFound() : Ok(order);
    }
    // Obtención de IDs de pedidos por UserId
    [HttpGet]
    [Route("GetOrdersByUser")]
    public ActionResult<List<int>> GetOrdersByUser(int userId)
    {
        var orderIds = _context.Orders
                            .Where(o => o.UserId == userId)
                            .Select(o => o.Id)
                            .ToList();

        if (orderIds == null || orderIds.Count == 0)
        {
            return NotFound("No se encontraron pedidos para el usuario especificado.");
        }

        return Ok(orderIds);
    }
    //Introducir un nuevo pedido
    [HttpPost]
    [Route("PostOrder")]
    public ActionResult CreateOrder(Order orderItem)
    {
        var existingOrder =_context.Orders.Find(orderItem.Id);
        if (existingOrder != null)
        {
            return Conflict("We alredy have an order with that id");
        }else
        {
            _context.Orders.Add(orderItem);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/"+ orderItem.Id;
            return Created(resourceUrl,orderItem);
        }
   
    }
//Modificar un pedido
    [HttpPut]
    [Route("PutOrder")]
    public ActionResult UpdateOrder(Order orderItem)
    {
        var existingOrder = _context.Orders.Find(orderItem.Id);
        if (existingOrder == null)
        {
            return NotFound("We dont have an order with that id");
        }
        else
        {
            if (orderItem.UserId!=0)
            {
                existingOrder.UserId = orderItem.UserId;
            }
            if (orderItem.Paid)
            {
                existingOrder.Paid = orderItem.Paid;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
    //Eliminar un pedido
    [HttpDelete]
    [Route("DeleteOrder")]
    public ActionResult<Order> DeleteOrder(int Id)
    {
        var existingOrder = _context.Orders.Find(Id);
        if (existingOrder == null)
        {
            return NotFound();
        }
        else
        {
            _context.Orders.Remove(existingOrder);
            _context.SaveChanges();
            return NoContent();
        }
    }
}