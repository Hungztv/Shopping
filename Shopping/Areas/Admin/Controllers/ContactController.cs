using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models.Repository;

namespace Shopping.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("Admin/Contact")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly DataContext _dataContext;
        public ContactController(DataContext context)
        {
            _dataContext = context;
        }

        public IActionResult Index()
        {
            var contact = _dataContext.Contact.ToList();
            return View(contact);
        }
    }
}
