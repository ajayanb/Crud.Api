using CodPulse.API.Models.Domain;
using CodPulse.API.Models.DTO;
using CodPulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodPulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }








        //POST: {apibaseurl}/api/images
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string title) 
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                //File upload

                var blogImage = new BlogImage
                {
                    FileExtension = Path.GetExtension(file.FileName).ToLower(),
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now,
                };

               blogImage =await imageRepository.Upload(file, blogImage);

                //Convert Domain Model to DTO
                var response = new BlogImageDto
                {
                    Id = blogImage.Id,
                    Title = blogImage.Title,
                    DateCreated = blogImage.DateCreated,
                    FileExtension = blogImage.FileExtension,
                    FileName = blogImage.FileName,
                    Url = blogImage.Url,

                };

                return Ok(blogImage);


            }


            return BadRequest(ModelState);


        }


        private void ValidateFileUpload(IFormFile file) 
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.TryAddModelError("file", "Unsupported file format");
            }
            if(file.Length > 10485760)
            {
                ModelState.TryAddModelError("file", "File more cannot be more than 10MB");
            }
           
        
        }
    }
}
