using ClientLibrary.Models.Category;
using ClientLibrary.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientLibrary.State
{
    public class CategoryStore
    {
        private readonly ICategoryService _categoryService;

        public CategoryStore(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IEnumerable<GetCategory> Categories { get; private set; } = new List<GetCategory>();
        
        public event Action? OnChange;

        public async Task LoadCategoriesAsync(bool forceLoad = false)
        {
            // Only load if not already loaded (simple caching) unless forced
            if (!forceLoad && Categories != null && ((List<GetCategory>)Categories).Any()) 
            {
                return;
            }

            var result = await _categoryService.GetAllAsync();
            if (result != null)
            {
                Categories = result;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
