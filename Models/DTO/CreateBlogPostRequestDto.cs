﻿namespace CodPulse.API.Models.DTO
{
    public class CreateBlogPostRequestDto
    {
        public string Title { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string Context { get; set; } = string.Empty;

        public string FeaturedImageUrl { get; set; } = string.Empty;

        public string UrlHandle { get; set; } = string.Empty;

        public DateTime PublishedDate { get; set; }

        public string Author { get; set; } = string.Empty;

        public bool IsVisible { get; set; }

        public Guid[] Categories { get; set; } = Array.Empty<Guid>();




    }
}
