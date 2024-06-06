namespace TiendaKarate.Models;

public class Filters{
    public List<int>?BrandsIds {get; set;}
    public List<int>?SportsIds {get; set;}
    public List<int>?CategoriesIds {get; set;}
    public List<int>?ColourIds {get; set;}
    public List<int>?TeamsIds {get; set;}
    public List<int>?AgesIds {get; set;}
    public List<int>?GendersIds {get; set;}


    public Filters(){
        BrandsIds = new List<int>();
        SportsIds = new List<int>();
        CategoriesIds = new List<int>();
        ColourIds = new List<int>();
        TeamsIds = new List<int>();
        AgesIds = new List<int>();
        GendersIds = new List<int>();
    }
}