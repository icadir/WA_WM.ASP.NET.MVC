using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyWebSite.BLL;
using MyWebSite.DAL.MyEntities;
using MyWebSite.Models;

namespace MyWebSite.Controllers
{
    public class UserController : Controller
    {
        UserService _userService;
        public UserController()
        {
            _userService = new UserService();

        }
        // GET: User
        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(UserVM item)
        {
            _userService.AddUser(new User()
            {
                EMail = item.EMail,
                Password = item.Password,
                LastName = item.LastName,
                FirstName = item.FirstName,
                Gender = item.Gender
            });
            TempData["Message"] = "Kaydınız oluşturuldu. ";
            return RedirectToAction("RegisterUser");
        }

    }
}