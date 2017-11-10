using BL;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    [Autenticado]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var NroNac = 100;
            var NroDef = 100;
            var NroMat = 100;

            ViewBag.NroNac = NroNac;
            ViewBag.NroDef = NroDef;
            ViewBag.NroMat = NroMat;
            ViewBag.Total = NroNac + NroDef + NroMat;
            return View();
        }
        public JsonResult EnvioMenus(int[] cbomenu)
        {
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SinPermiso()
        {
            return View();
        }
               
    }
}