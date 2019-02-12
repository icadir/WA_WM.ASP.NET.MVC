using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Web.UI.Controllers
{
    public class HataController : Controller
    {
        // GET: Hata
        public ActionResult Index()
        {
            Response.Status = "Aradıgınız Sayfa bulunamadı";
            Response.StatusCode = 404;
            return View();
        }

        public ActionResult H500()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}