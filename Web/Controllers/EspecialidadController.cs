using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class EspecialidadController : Controller
    {
        // GET: Especialidad
        public ActionResult Index()
        {
            return View();
        }


        

        public JsonResult ObtenerEspecialidad()
        {
            return Json(BL.EspecialidadBL.Listar(), JsonRequestBehavior.AllowGet);
        }


    }
}