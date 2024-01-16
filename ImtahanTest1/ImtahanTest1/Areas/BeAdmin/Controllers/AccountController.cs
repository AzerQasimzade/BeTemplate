using ImtahanTest1.Areas.ViewModels;
using ImtahanTest1.Models;
using ImtahanTest1.Utilities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ImtahanTest1.Areas.BeAdmin.Controllers
{
    [Area("BeAdmin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = new AppUser
            {
                Email = registerVM.Email,
                UserName= registerVM.UserName,
              
            };
            IdentityResult identityResult=await _userManager.CreateAsync(user,registerVM.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                    return View();
                }
            }
            await _userManager.AddToRoleAsync(user, UserRole.Member.ToString());
            await _signInManager.SignInAsync(user,false);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
                if (user is null)
                {
                    ModelState.AddModelError(String.Empty, "Username,password or email is incorrect");
                    return View();
                }
            }
            var passwordCheck = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.IsRemembered, false);
            if (!passwordCheck.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Username,password or email is incorrect");
                return View();
            }
            if (passwordCheck.IsLockedOut)
            {
                ModelState.AddModelError(String.Empty, "You Have Blocked hahaha");
                return View();
            }
            return RedirectToAction("Index", "Home",new {area=""});
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home",new {area=""});
        }

        public async Task<IActionResult> CreateRoles()
        {
            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = role.ToString(),
                });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
