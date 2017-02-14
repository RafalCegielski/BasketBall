using BasketBallMVC.Services;
using BasketBallMVC.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BasketBallMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private AdminService _adminService = new AdminService();
        private ComplementViewModelsService _complementVMService = new ComplementViewModelsService();
        // GET: Admin
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Users(int? Page, string searchString)
        {
            int page = Page ?? 1;
            var userList = _adminService.GetUserList();

            if (!string.IsNullOrEmpty(searchString))
            {
                userList.pagedList = userList.pagedList.Where(s => s.email.Contains(searchString) || s.name.Contains(searchString) || s.id.Contains(searchString)).ToList();
            }

            userList.pagedPagedList = userList.pagedList.ToPagedList(page, 25);


            return View(userList);
        }
        public ActionResult Characters()
        {
            return View();
        }
        public ActionResult Statistics()
        {
            return View();
        }
        public ActionResult DeleteUser(string userId)
        {
            _adminService.DeleteUserWithDependencies(userId);

            return RedirectToAction("Users");
        }
        public void BanUser(string value, string userId)
        {
            _adminService.BanUser(value, userId);
        }
        public void UnbanUser(string userId)
        {
            _adminService.UnbanUser(userId);
        }
        [HttpGet]
        public ActionResult CharacterProfile(string userId)
        {
            AdminProfileViewModel adminProfileVM = _complementVMService.ComplementAdminProfileVM(userId);

            return View(adminProfileVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CharacterProfile(AdminProfileViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            _adminService.ChangeCharacterData(vm);
            return Redirect("Users");
        }
    }
}