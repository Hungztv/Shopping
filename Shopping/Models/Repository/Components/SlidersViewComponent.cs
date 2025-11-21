using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shopping.Models.Repository.Components
{
    public class SlidersViewComponent : ViewComponent
    {
        private readonly DataContext _dataContext;
        public SlidersViewComponent(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _dataContext.Sliders
                .Where(s => s.Status == 1)
                .OrderByDescending(s => s.Id)
                .ToListAsync();

            return View(sliders);
        }
    }
}
