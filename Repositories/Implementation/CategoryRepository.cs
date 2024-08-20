using CodPulse.API.Data;
using CodPulse.API.Models.Domain;
using CodPulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Metadata.Ecma335;

namespace CodPulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContex dbContex;

        public CategoryRepository(ApplicationDbContex dbContex )
        {
            this.dbContex = dbContex;
        }


        public async Task<Category> CreateAsync(Category category)
        {


            await dbContex.Categories.AddAsync(category);
            await dbContex.SaveChangesAsync();

            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
           var existingCategory = await dbContex.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory is null) 
            {
                return null;
            
            }
            dbContex.Categories.Remove(existingCategory);
            await dbContex.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
           return    await dbContex.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await dbContex.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
           var existingCategory = await dbContex.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
            if(existingCategory != null)
            {
                dbContex.Entry(existingCategory).CurrentValues.SetValues(category);
                await dbContex.SaveChangesAsync();
                return category;
            }

            return null;
        }
    }
}
