namespace TiendaKarate.Models;

public class OrderProduct{
    public int Id {get; set;}
    public int OrderId {get; set;}
    public int SizeProductId {get; set;}
    public int Quantity {get; set;}

    public OrderProduct(){
    }
}