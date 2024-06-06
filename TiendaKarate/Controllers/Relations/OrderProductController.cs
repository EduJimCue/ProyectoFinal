using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("OrderProduct")]
public class OrderProductController : ControllerBase{
    private readonly DataContext _context;
    private static List<OrderProduct>OrderProducts=new List<OrderProduct>{};
    public OrderProductController (DataContext context){
        _context = context;
    }
    //Obtengo todos los pedidos con productos
    [HttpGet]
    [Route("GetAllOrderProduct")]
    public ActionResult<List<OrderProduct>>Get(){
        return Ok(_context.OrderProducts);
    }
    //Obtencion de un pedido con producto por id
    [HttpGet]
    [Route("GetOneOrder")]
    public ActionResult<List<SizeProduct>>GetSizeProductById(int Id)
    {
        var orderProduct = _context.OrderProducts.Find(Id);
        return orderProduct ==null ? NotFound() : Ok(orderProduct);
    }

    //Obtencion de los productos con talla de un pedido
    [HttpGet]
    [Route("GetProductsByOrder")]
    public ActionResult<List<int>> GetProductByOrder(int orderId)
    {
        var orderProducts = _context.OrderProducts
            .Where(op => op.OrderId == orderId)
            .ToList();

        return orderProducts.Count == 0 ? NotFound() : Ok(orderProducts);
    }

    [HttpGet]
    [Route("CheckSizeProductQuantity")]
    public bool CheckSizeProductQuantity(int sizeProductId, int Quantity)
    {
        var sizeProduct = _context.SizeProducts.Find(sizeProductId);

        if (sizeProduct == null)
        {
            return false;
        }

        return Quantity <= sizeProduct.Stock;
    }

    //Introducir una nueva relacion de producto con talla y stock con el numero de pedido
    [HttpPost]
    [Route("PostOrderProduct")]
    public ActionResult CreateOrderProduct(OrderProduct orderProduct)
    {
         bool isQuantityValid = CheckSizeProductQuantity(orderProduct.SizeProductId, orderProduct.Quantity);

        if (!isQuantityValid)
        {
            return Conflict("La cantidad excede el stock disponible.");
        }
    
        var existingOrderProduct =_context.OrderProducts.Find(orderProduct.Id);
        if (existingOrderProduct != null)
        {
            return Conflict("We alredy have a relation of order and sizeProduct with that id");
        }else
        {
            var sizeProduct = _context.SizeProducts.Find(orderProduct.SizeProductId);
            if (sizeProduct != null)
            {
                sizeProduct.Stock -= orderProduct.Quantity;
            }
            else
            {
                return NotFound("No se encontró el SizeProduct asociado.");
            }

            _context.OrderProducts.Add(orderProduct);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/"+ orderProduct.Id;
            return Created(resourceUrl,orderProduct);
        }
    }

    //Modificar una relacion de producto y talla(Solo el stock?)
    [HttpPut]
    [Route("PutOrderProduct")]
    public ActionResult UpdateSizeProduct(OrderProduct orderProductItem)
    {
        var existingOrderProduct = _context.OrderProducts.Find(orderProductItem.Id);
        if (existingOrderProduct == null)
        {
            return NotFound("We dont have a relation of order and product with that id");
        }
        else
        {
            if (orderProductItem.OrderId!=0)
            {
                existingOrderProduct.OrderId = orderProductItem.OrderId;
            }
            if (orderProductItem.SizeProductId!=0)
            {
                existingOrderProduct.SizeProductId = orderProductItem.SizeProductId;
            }
            if (orderProductItem.Quantity!=0)
            {
                existingOrderProduct.Quantity = orderProductItem.Quantity;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
    //Eliminar una relacion de tallas y productos
    [HttpDelete]
    [Route("DeleteOrderProduct")]
    public ActionResult<SizeProduct> DeleteOrderProduct(int Id)
    {
        var existingOrderProduct = _context.OrderProducts.Find(Id);
        if (existingOrderProduct == null)
        {
            return NotFound();
        }
        else
        {
            _context.OrderProducts.Remove(existingOrderProduct);
            _context.SaveChanges();
            return NoContent();
        }
    }
}