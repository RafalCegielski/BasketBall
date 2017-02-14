using BasketBallMVC.App_Start;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using BasketBallMVC.ViewModel;
using BasketBallMVC.Model;
using System.Threading.Tasks;
using BasketBallMVC.Services;
using BasketBallMVC.DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;

namespace BasketBallMVC.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private CharacterService _characterService = new CharacterService();


        public ActionResult CreateRole()
        {
            string Output = "";
            using (var db = new BasketBallContext())
            {
                RoleManager<IdentityRole> RoleMenager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                if (!RoleMenager.RoleExists("Admin"))
                {
                    IdentityResult Result = RoleMenager.Create(new IdentityRole("Admin"));
                    if (Result.Succeeded)
                    {
                        Output = "Role is created!";
                    }
                    else
                    {
                        Output = "Error: " + Result.Errors.ToList()[0];
                    }
                }
                else
                {
                    Output = "The role already exists";
                }
            }
            return Content(Output);
        }

        public ActionResult AddUserToRole() // TODO
        {
            string Output = "";
            using (var db = new BasketBallContext())
            {
                RoleManager<IdentityRole> RoleMenager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                UserManager<ApplicationUser> UserMenager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                ApplicationUser user = UserMenager.FindByEmail("admin2@wp.pl");
                IdentityRole role = RoleMenager.FindByName("Admin");

                if (user == null)
                {
                    Output = "The user does not exist";
                }
                if (role == null)
                {
                    Output = "The role does not exist";
                }
                else
                {
                    if (!UserManager.IsInRole(user.Id, "Admin"))
                    {
                        UserMenager.AddToRole(user.Id, "Admin");
                        Output = "The user added to the role";
                    }
                    else
                    {
                        Output = "The user already exists in the role";
                    }

                }

            }
            return Content(Output);
        }

        public ActionResult RemoveUserToRole() // TODO
        {
            string Output = "";
            using (var db = new BasketBallContext())
            {
                RoleManager<IdentityRole> RoleMenager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                UserManager<ApplicationUser> UserMenager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                ApplicationUser user = UserMenager.FindByEmail("admin2@wp.pl");
                IdentityRole role = RoleMenager.FindByName("Admin");

                if (user == null)
                {
                    Output = "The user does not exist";
                }
                if (role == null)
                {
                    Output = "The role does not exist";
                }
                else
                {
                    if (UserManager.IsInRole(user.Id, "Admin"))
                    {
                        UserMenager.RemoveFromRole(user.Id, "Admin");
                        Output = "The user deleted from the role";
                    }
                    else
                    {
                        Output = "The user does not exists in the role";
                    }

                }

            }
            return Content(Output);
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }



        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BasketBallContext())
                {
                    var allUsers = db.Users.ToList();
                    if (allUsers.Any(x => x.UserData.Nick == model.Nick))
                    {
                        AddErrors(new IdentityResult("Nick jest zajęty"));
                        return View(model);
                    }
                }

                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, UserData = new UserData() { FirstName = model.FirstName, LastName = model.LastName, Nick = model.Nick } };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    _characterService.CreateCharacter(user.Id);

                    
                    return RedirectToAction("Home", "Home");
                }
                AddErrors(result);
            }
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("registerError", error);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }   
                // This doen't count login failures towards lockout only two factor authentication
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            model.Name = "test";
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Home", "Home");
                case SignInStatus.LockedOut:
                    var user = await UserManager.FindByNameAsync(model.Email);
                    var message = string.Format("Twoje konto jest zablokowane do: {0} UTC", user.LockoutEndDateUtc.ToString());
                    ModelState.AddModelError("loginerror", message);
                    return View(model);
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("loginerror", "Nieudana próba logowania.");
                    return View(model);
            }

        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}