using Amazon.S3;
using Microsoft.AspNetCore.Mvc;
using TiendaKarate.Data;
using Microsoft.EntityFrameworkCore;
using TiendaKarate.Models;

namespace TiendaKarate.Controllers;

[ApiController]
[Route("S3Images")]
public class ImageController : ControllerBase
{
    private readonly IAmazonS3 _s3Client;
    private readonly DataContext _context;
    
    public ImageController(IAmazonS3 s3Client, DataContext context)
    {
        _s3Client = s3Client;
        _context = context;
    }
    
    //Obtener todas las imagenes
    [HttpGet]
    [Route("GetAllImages")]
    public ActionResult<List<Image>>Get(){
        return Ok(_context.Images);
    }
    
    // Obtengo imagenes por url parcial
    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> SearchImages(string partialName)
    {
        try
        {
            if (string.IsNullOrEmpty(partialName))
            {
                return BadRequest("No se proporcionó un nombre parcial de la imagen.");
            }

            // Filtrar imágenes en tu contexto de base de datos por un nombre parcial
            var images = await _context.Images
                .Where(i => i.Url.Contains(partialName))
                .ToListAsync();

            if (images == null || !images.Any())
            {
                return NotFound($"No se encontraron imágenes que coincidan con el nombre parcial '{partialName}'.");
            }

            return Ok(images);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error al buscar imágenes por nombre parcial: {ex.Message}");
        }
    }

    //Subir una nueva imagen
    [HttpPost]
    [Route("Upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No se proporcionó ninguna imagen.");
            }

            string bucketName = "tiendakarate";
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string s3Key = "imagenes/" + file.FileName;

            using (var newMemoryStream = new MemoryStream())
            {
                file.CopyTo(newMemoryStream);

                var uploadRequest = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = s3Key,
                    InputStream = newMemoryStream,
                    ContentType = file.ContentType,
                    CannedACL = Amazon.S3.S3CannedACL.PublicRead
                };

                await _s3Client.PutObjectAsync(uploadRequest);
            }

            var imageUrl = $"https://{bucketName}.s3.amazonaws.com/{s3Key}";

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);

            var image = new Image
            {
                Url = imageUrl,
                Name = fileNameWithoutExtension
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();
            return Ok(new { imageUrl });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error al subir la imagen: {ex.Message}");
        }
    }
    //Eliminar imagenes

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteImage(int imageId)
    {
        var image = await _context.Images.FindAsync(imageId);
        if (image == null)
            {
                return NotFound($"No se encontró ninguna imagen con la ID {imageId}.");
            }
        try
        {
            string bucketName = "tiendakarate";
            var deleteRequest = new Amazon.S3.Model.DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = image.Url.Replace("https://tiendakarate.s3.amazonaws.com/", "")
            };
            await _s3Client.DeleteObjectAsync(deleteRequest);
    
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
    
            var imageProductsToDelete = await _context.ImageProducts.Where(ip => ip.ImageId == imageId).ToListAsync();
            _context.ImageProducts.RemoveRange(imageProductsToDelete);
            await _context.SaveChangesAsync();
    
            return Ok($"La imagen {image.Url} se eliminó correctamente de S3 y de la base de datos.");
        }
        catch (Amazon.S3.AmazonS3Exception ex)when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            
            var imageProductsToDelete = await _context.ImageProducts.Where(ip => ip.ImageId == imageId).ToListAsync();
            _context.ImageProducts.RemoveRange(imageProductsToDelete);
            await _context.SaveChangesAsync();
    
            return StatusCode(500, $"Ocurrió un error al eliminar la imagen de S3: {ex.Message}. La imagen se eliminó de la base de datos.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error al eliminar la imagen: {ex.Message}");
        }
    }
}