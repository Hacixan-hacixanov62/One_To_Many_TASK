
using Fiorello_PB101_Demo.Models;
using Fiorello_PB101_Demo.ViewModels.Categories;
using Fiorello_PB101_Demo.ViewModels.Products;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Fiorello_PB101_Demo.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> GetAllPaginateAsync(int page, int take);

        Task<IEnumerable<Category>> GetAllPaginateArchiveAsync(int page, int take);


        Task<IEnumerable<CategoryProductVM>> GetAllWithProductCountAsync();
        Task<Category> GetByIdAsync(int id);
        Task<CategoryDetailVM> GetByIdWithProductsAsync(int id);
        Task<bool> ExistAsync(string name);
        Task CreateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<bool> ExistExceptByIdAsync(int id, string name);
        Task<IEnumerable<CategoryArchiveVM>> GetAllArchiveProductCountAsync();

        IEnumerable<CategoryProductVM> GetMappedDatas(IEnumerable<Category> categories);

        IEnumerable<CategoryArchiveVM> GetMappedAddDatas(IEnumerable<Category> archives);
        Task<int> GetCountAsync();

        Task<int> GetAllCountAsync();
        Task<SelectList> GetAllSelectedAsync();
    }
}
