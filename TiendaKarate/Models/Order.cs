namespace TiendaKarate.Models;

public class Order{
    public int Id {get; set;}
    public int UserId {get; set;}
    public DateTime CreationDate {get; set;}
    public bool Paid {get; set;}
    
    public Order(){
    }
}