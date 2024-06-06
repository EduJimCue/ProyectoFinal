namespace TiendaKarate.Models;

public class Product{
    public int Id { get; set; }
    public string? Name { get; set; }
    public float? Price {get; set;}
    public int? SportId {get; set;}
    public int? BrandId {get; set;}
    public int? CategoryId {get; set;}
    public int? ColourId {get; set;}
    public int? TeamId {get; set;}
    public int? AgeId {get; set;}
    public int? GenderId {get; set;}
    public bool? IsActive {get; set;} 
    public string? PrincipalImage {get; set;}
    public string? SecondImage {get; set;}
    public string? ThirdImage {get; set;}
    public string? FourthImage {get; set;}
    public string? Description {get; set;}

    public Product(){
    }
}