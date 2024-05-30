using Fiorello_PB101_Demo.Data;
using Fiorello_PB101_Demo.Models;
using Fiorello_PB101_Demo.Services.Interfaces;
using Fiorello_PB101_Demo.ViewModels.Categories;
using Fiorello_PB101_Demo.ViewModels.Products;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_PB101_Demo.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }


        public async Task<IEnumerable<Category>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Categories.Include(m => m.Products)
                                              .Skip((page - 1) * take)
                                              .Take(take)
                                              .ToListAsync();
        }


        public async Task<IEnumerable<CategoryProductVM>> GetAllWithProductCountAsync()
        {
            IEnumerable<Category> categories = await _context.Categories.Include(m => m.Products)
                                                                        .OrderByDescending(m => m.Id)
                                                                        .ToListAsync();

            return categories.Select(m => new CategoryProductVM
            {
                Id = m.Id,
                CategoryName = m.Name,
                CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy"),
                ProductCount = m.Products.Count
            });
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.IgnoreQueryFilters().FirstOrDefaultAsync(m=>m.Id == id);
        }


        public async Task<CategoryDetailVM> GetByIdWithProductsAsync(int id)
        {
            var category = await _context.Categories
                .Where(m => m.Id == id)
                .Include(m => m.Products)
                .FirstOrDefaultAsync();

            return new CategoryDetailVM
            {
                Name = category.Name,
                CreatedDate = category.CreatedDate.ToString("MM.dd.yyyy"),
                Products = category.Products.Select(m => m.Name).ToList(),
                ProductCount = category.Products.Count
            };
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistExceptByIdAsync(int id, string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name == name && m.Id != id);
        }

        //=============================================================Archive Methods===============================
        public async Task<IEnumerable<CategoryArchiveVM>> GetAllArchiveProductCountAsync()
        {
            IEnumerable<Category> archives = await _context.Categories.IgnoreQueryFilters()
                                                                        // .Include(m => m.Products)
                                                                         .Where(m=>m.SoftDeleted)
                                                                         .OrderByDescending(m=>m.Id)
                                                                         .ToListAsync();
            return archives.Select(m=>new CategoryArchiveVM
            {
                Id = m.Id,
                CategoryName = m.Name,
                CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy")
            });


        }

        public IEnumerable<CategoryArchiveVM> GetMappedAddDatas(IEnumerable<Category> archives)
        {
            return archives.Select(m => new CategoryArchiveVM
            {
                Id = m.Id,
                CategoryName = m.Name,
                CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy")
            });
        }

        public async Task<IEnumerable<Category>> GetAllPaginateArchiveAsync(int page, int take)
        {
            return await _context.Categories.Include(m => m.Products)
                                                .Skip((page - 1) * take)
                                                .Take(take)
                                                .ToListAsync();
        }

        //===================================================================================================


        public IEnumerable<CategoryProductVM> GetMappedDatas(IEnumerable<Category> catigories)
        {
            return catigories.Select(m => new CategoryProductVM
            {
                Id = m.Id,
                CategoryName = m.Name,
                CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy"),
                ProductCount = m.Products.Count
            });
        }


        public async Task<int> GetCountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<int> GetAllCountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<SelectList> GetAllSelectedAsync()
        {
           var categories = await _context.Categories.ToListAsync();

            return new SelectList(categories, "Id", "Name");
        }

 
    }
}
