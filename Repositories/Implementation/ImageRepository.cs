using CodPulse.API.Data;
using CodPulse.API.Models.Domain;
using CodPulse.API.Repositories.Interface;

namespace CodPulse.API.Repositories.Implementation
{
    public class ImageRepository : IImageRepository

    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContex dbContex;

        public ImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,ApplicationDbContex dbContex)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContex = dbContex;
        }



        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            //1-Upload the Image to API/Images
            var  localpath = Path.Combine(webHostEnvironment.ContentRootPath ,"Images", $"{blogImage.FileName}{blogImage.FileExtension}");
            using var stream = new FileStream(localpath, FileMode.Create);
            await  file.CopyToAsync(stream);

            // 2-Update the database
            // https://codepulse.com/images/somefilename.jpg

            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";

            blogImage.Url = urlPath;
             
            await dbContex.BlogImages.AddAsync(blogImage);

            await dbContex.SaveChangesAsync();

            return blogImage;



        }
    }
}
