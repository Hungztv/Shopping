using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Shopping.Models.Repository.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly DataContext _Datacontext;
        public CategoriesViewComponent(DataContext context)
        {
            _Datacontext = context;
        }
        public async Task<IViewComponentResult> InvokeAsync() => View(await _Datacontext.Categories.ToListAsync());
    }
}
