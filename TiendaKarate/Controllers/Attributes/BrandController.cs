using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("Brand")]
public class BrandController : ControllerBase{
    private readonly DataContext _context;
    private static List<Brand>Brands=new List<Brand>{};
    public BrandController (DataContext context){
        _context = context;
    }
    //Obtengo todas las marcas
    [HttpGet]
    [Route("GetAllBrands")]
    public ActionResult<List<Brand>>Get(){
        return Ok(_context.Brands);
    }
    //Obtencion de una marca
    [HttpGet]
    [Route("GetOneBrand")]
    public ActionResult<List<Brand>>GetBrandById(int Id)
    {
        var brand = _context.Brands.Find(Id);
        return brand==null ? NotFound() : Ok(brand);
    }
    //Introducir una nueva marca
    [HttpPost]
    [Route("PostBrand")]
    public ActionResult CreateBrand(Brand brandItem)
    {
        var existingBrand =_context.Brands.Find(brandItem.Id);
        if (existingBrand != null)
        {
            return Conflict("We alredy have a brand with that id");
        }else
        {
            _context.Brands.Add(brandItem);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/"+ brandItem.Id;
            return Created(resourceUrl,brandItem);
        }
   
    }
//Modificar una marca
    [HttpPut]
    [Route("PutBrand")]
    public ActionResult UpdateBrand(Brand brandItem)
    {
        var existingBrand = _context.Brands.Find(brandItem.Id);
        if (existingBrand == null)
        {
            return NotFound("We dont have a brand with that id");
        }
        else
        {
             if (brandItem.Name!="string")
            {
                existingBrand.Name = brandItem.Name;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
    //Eliminar una marca
    [HttpDelete]
    [Route("DeleteBrand")]
    public ActionResult<Brand> DeleteBrand(int Id)
    {
        var existingBrand = _context.Brands.Find(Id);
        if (existingBrand == null)
        {
            return NotFound();
        }
        else
        {
            _context.Brands.Remove(existingBrand);
            _context.SaveChanges();
            return NoContent();
        }
    }
}