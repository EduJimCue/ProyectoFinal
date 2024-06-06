namespace TiendaKarate.Models;

public class User{
    public int Id {get; set;}
    public string Email {get; set;}
    public string Name {get; set;}
    public string Password {get; set;}
    public bool Role {get; set;}
    public DateTime SignUpDate {get; set;}
    public string Adress {get; set;}
    
    public User(){
    }
}