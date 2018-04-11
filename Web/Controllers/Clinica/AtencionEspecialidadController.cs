using BE;
using BL;
using Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers.Clinica
{
    public class AtencionEspecialidadController : Controller
    {
        // GET: AtencionEspecialidad
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Guardar(List<atencionespecialidad> lista)
        {
            var rm = new ResponseModel();
            try
            {
                AtencionEspecialidadBL.Guardar(lista);
                rm.SetResponse(true);
                rm.href = Url.Action("Index", "Topico");
            }
            catch (Exception ex)
            {
                rm.SetResponse(false);
                rm.function = "fn.mensaje('" + ex.Message + "')";
            }

            return Json(rm);
        }
    }
}