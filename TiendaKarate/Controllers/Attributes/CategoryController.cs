using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("Category")]
public class CategoryController : ControllerBase{
    private readonly DataContext _context;
    private static List<Category>Categories=new List<Category>{};
    public CategoryController (DataContext context){
        _context = context;
    }
    //Obtengo todas las categorias
    [HttpGet]
    [Route("GetAllCategories")]
    public ActionResult<List<Category>>Get(){
        return Ok(_context.Categories);
    }
    //Obtencion de una categoria
    [HttpGet]
    [Route("GetOneCategory")]
    public ActionResult<List<Category>>GetCategoryById(int Id)
    {
        var category = _context.Categories.Find(Id);
        return category==null ? NotFound() : Ok(category);
    }
    //Introducir una nueva categoria
    [HttpPost]
    [Route("PostCategory")]
    public ActionResult CreateCategory(Category categoryItem)
    {
        var existingCategory =_context.Categories.Find(categoryItem.Id);
        if (existingCategory != null)
        {
            return Conflict("We alredy have a category with that id");
        }else
        {
            _context.Categories.Add(categoryItem);
            _context.SaveChanges();
            var resourceUrl = Request.Path.ToString() + "/"+ categoryItem.Id;
            return Created(resourceUrl,categoryItem);
        }
   
    }
//Modificar una categoria
    [HttpPut]
    [Route("PutCategory")]
    public ActionResult UpdateCategory(Category categoryItem)
    {
        var existingCategory = _context.Categories.Find(categoryItem.Id);
        if (existingCategory == null)
        {
            return NotFound("We dont have a category with that id");
        }
        else
        {
             if (categoryItem.Name!="string")
            {
                existingCategory.Name = categoryItem.Name;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
    //Eliminar una categoria
    [HttpDelete]
    [Route("DeleteCategory")]
    public ActionResult<Category> DeleteCategory(int Id)
    {
        var existingCategory = _context.Categories.Find(Id);
        if (existingCategory == null)
        {
            return NotFound();
        }
        else
        {
            _context.Categories.Remove(existingCategory);
            _context.SaveChanges();
            return NoContent();
        }
    }
}