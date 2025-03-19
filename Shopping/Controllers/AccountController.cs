
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Models.Repository;
using Shopping.Models.ViewModels;
using System.Security.Claims;

namespace Shopping.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUserModel> _userManager;
        private SignInManager<AppUserModel> _signInManager;
        private IEmailSender _emailSender;
        private readonly DataContext _dataContext;
        public AccountController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signInManager, DataContext dataContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dataContext = dataContext;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl});
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> History()
        {
            if ((bool)!User.Identity?.IsAuthenticated)
            {
                // User is not logged in, redirect to login
                return RedirectToAction("Login", "Account"); // Replace "Account" with your controller name
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var Orders = await _dataContext.Orders
                .Where(od => od.UserName == userEmail).OrderByDescending(od => od.Id).ToListAsync();
            ViewBag.UserEmail = userEmail;
            return View(Orders);
        }
        public async Task<IActionResult> CancelOrder(string ordercode)
        {
            if ((bool)!User.Identity?.IsAuthenticated)
            {
                // User is not logged in, redirect to login
                return RedirectToAction("Login", "Account");
            }
            try
            {
                var order = await _dataContext.Orders.Where(o => o.OrderCode == ordercode).FirstAsync();
                order.Status = 3;
                _dataContext.Update(order);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                return BadRequest("An error occurred while canceling the order.");
            }


            return RedirectToAction("History", "Account");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);
                if (result.Succeeded)
                {
                    TempData["success"] = "Đăng nhập thành công!";
                    
                    return Redirect(loginVM.ReturnUrl ?? "/");
                }
                ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu");
            }
            return View(loginVM);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                AppUserModel newUser = new AppUserModel
                {
                    UserName = user.UserName,
                    Email = user.Email
                };
                IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
                if (result.Succeeded)
                {
                    TempData["success"] = "Tạo tài khoản thành công!";
                    return RedirectToAction("Index","Account");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }
        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}