using System.Web;
using System.Web.Mvc;
using ASP_Photo_Gallery.DAL.Context;
using ASP_Photo_Gallery.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace ASP_Photo_Gallery.Controllers
{
    public class AccountController : Controller
    {
        private IUserStore<IdentityUser> _userStore;

        private IUserStore<IdentityUser> UserStore
        {
            get
            {
                if (_userStore == null)
                {
                    var dbContext = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                    _userStore = new UserStore<IdentityUser>(dbContext);
                }

                return _userStore;
            }
        }

        private UserManager<IdentityUser> _userManager;

        private UserManager<IdentityUser> UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = new UserManager<IdentityUser>(UserStore);
                }

                return _userManager;
            }
        }

        private SignInManager<IdentityUser, string> _signInManager;

        private SignInManager<IdentityUser, string> SignInManager
        {
            get
            {
                if (_signInManager == null)
                {
                    var authentication = HttpContext.GetOwinContext().Authentication;
                    _signInManager = new SignInManager<IdentityUser, string>(UserManager, authentication);
                }

                return _signInManager;
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = SignInManager.PasswordSignIn(loginViewModel.UserName, loginViewModel.Password, true, false);
                if (result == SignInStatus.Success)
                {
                    return RedirectToAction("Index", "Image");
                }
            }
            ModelState.AddModelError("", "Login or password not match");
            return View(loginViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            SignInManager.AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Image");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            var user = new IdentityUser
            {
                UserName = registerViewModel.UserName
            };
            var result = UserManager.Create(user, registerViewModel.Password);
            if (registerViewModel.Password.Length < 6)
            {
                ModelState.AddModelError("", "Password must be over 6 chars");
            }
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Registration failed");
            return View(registerViewModel);
        }
        
    }
}