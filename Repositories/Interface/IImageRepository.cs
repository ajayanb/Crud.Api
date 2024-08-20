using CodPulse.API.Models.Domain;

namespace CodPulse.API.Repositories.Interface
{
    public interface IImageRepository
    {
             Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);


    }
}
