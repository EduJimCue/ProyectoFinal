namespace TiendaKarate.Models;

public class InputFilter{
    public int? BrandId {get; set;}
    public int? SportId {get; set;}
    public int? CategoryId {get; set;}
    public int? ColourId {get; set;}
    public int? TeamId {get; set;}
    public int? AgeId {get; set;}
    public int? GenderId {get; set;}
    public int? SizeId {get; set;}
    public bool IsActive {get; set;}
    public int? MinPrice {get; set;}
    public int? MaxPrice {get; set;}

    public InputFilter(){
       
    }
}