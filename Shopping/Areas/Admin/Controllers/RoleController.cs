using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Role")]
    [Authorize]
    public class RoleController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(DataContext context, RoleManager<IdentityRole> roleManager)
        {
            _dataContext = context;
            _roleManager = roleManager;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Roles.OrderByDescending(p => p.Id).ToListAsync());
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                bool roleExists = await _roleManager.RoleExistsAsync(model.Name);
                if (!roleExists)
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole(model.Name));
                    if (result.Succeeded)
                    {
                        TempData["success"] = "Role created successfully!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Role already exists.");
                }
            }
            return View(model);
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(string id, IdentityRole model)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return NotFound();
                }

                role.Name = model.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    TempData["success"] = "Role updated successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            // Kiểm tra nếu role đang được sử dụng
            var usersInRole = await _dataContext.UserRoles.AnyAsync(ur => ur.RoleId == id);
            if (usersInRole)
            {
                TempData["error"] = "Cannot delete this role because it is assigned to users.";
                return RedirectToAction("Index");
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                TempData["success"] = "Role deleted successfully!";
            }
            else
            {
                TempData["error"] = "An error occurred while deleting the role.";
            }

            return RedirectToAction("Index");
        }
    }
}
