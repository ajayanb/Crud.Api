using CodPulse.API.Models.Domain;
using CodPulse.API.Models.DTO;
using CodPulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodPulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }
        //POST:{apibaseUrl}/api/blogposts
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            //Convert Dto to Domain Model

            var blogPost = new BlogPost
            {
                Author = request.Author,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle,
                Title = request.Title,
                Context = request.Context,
                FeaturedImageUrl = request.FeaturedImageUrl,
                PublishedDate = request.PublishedDate,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()

            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if (existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);

                }
            }

            blogPost = await blogPostRepository.CreateAsync(blogPost);

            var respose = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                Title = blogPost.Title,
                Context = blogPost.Context,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishedDate = blogPost.PublishedDate,
                IsVisible = blogPost.IsVisible,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(respose);
        }

        //GET:{apibaseUrl}/api/blogposts
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {

            var blogPosts = await blogPostRepository.GetAllAsync();


            //Convert Domain Model To DTO
            var response = new List<BlogPostDto>();
            foreach (var blogPost in blogPosts)
            {

                response.Add(new BlogPostDto

                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    ShortDescription = blogPost.ShortDescription,
                    UrlHandle = blogPost.UrlHandle,
                    Title = blogPost.Title,
                    Context = blogPost.Context,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    PublishedDate = blogPost.PublishedDate,
                    IsVisible = blogPost.IsVisible,

                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle,
                    }).ToList()



                });


            }
            return Ok(response);

        }

        //GET:{apibaseUrl}/api/blogposts/{id}
        [HttpGet]

        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {

            var blogPost = await blogPostRepository.GetByIdAsync(id);

            if (blogPost is null)
            {

                return NotFound();
            }

            //Convert Domain Model To DTO

            var response = new BlogPostDto

            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                Title = blogPost.Title,
                Context = blogPost.Context,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishedDate = blogPost.PublishedDate,
                IsVisible = blogPost.IsVisible,


                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);
        }

        //PUT: {apibaseurl}/blogposts/{id}
        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateBlogPostById([FromRoute] Guid id, UpdateBlogPostRequestDto request)
        {
            //convert DTO Domain Model



            var blogPost = new BlogPost
            {
                Id = id,
                Author = request.Author,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle,
                Title = request.Title,
                Context = request.Context,
                FeaturedImageUrl = request.FeaturedImageUrl,
                PublishedDate = request.PublishedDate,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()

            };

            //Foreach
            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);

                if (existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);

                }

            }

            //Call Repository To update BlogPost Domain Model 
            var updatedBlogPost = await blogPostRepository.UpdateAsync(blogPost);
            if (updatedBlogPost != null)
            {

                return NotFound();

            }
            //Convert Domain model back to DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                ShortDescription = blogPost.ShortDescription,
                UrlHandle = blogPost.UrlHandle,
                Title = blogPost.Title,
                Context = blogPost.Context,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                PublishedDate = blogPost.PublishedDate,
                IsVisible = blogPost.IsVisible,


                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()


            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {

            var deletedBlogPost = await blogPostRepository.DeleteAsync(id);
            if (deletedBlogPost == null)
            {

                return NotFound();


            }

            //Convert Domain Model To Dto
            var response = new BlogPostDto
            {


                Id = deletedBlogPost.Id,
                Author = deletedBlogPost.Author,
                ShortDescription = deletedBlogPost.ShortDescription,
                UrlHandle = deletedBlogPost.UrlHandle,
                Title = deletedBlogPost.Title,
                Context = deletedBlogPost.Context,
                FeaturedImageUrl = deletedBlogPost.FeaturedImageUrl,
                PublishedDate = deletedBlogPost.PublishedDate,
                IsVisible = deletedBlogPost.IsVisible,





            };
            return Ok(response);    


        }





    }
}

