﻿namespace CodPulse.API.Models.Domain
{
    public class Category
    {

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;


        public string UrlHandle { get; set; } = string.Empty;

        public ICollection<BlogPost> BlogPosts { get; set; } 
            //= new List<BlogPost>();

        public Category() {
            BlogPosts = new List<BlogPost>();
        }
    }  

}
