using BE;
using BL;
using Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ProgramacionController : Controller
    {
        // GET: Programacion
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GuardarProgramacion(programacion progr, string href)
        {
            var rm = new ResponseModel();
            //per.NombreCompleto = per.Nombres + " " + per.Paterno + " " + per.Materno;
            try
            {
                //MedicoBL.guardarMedico(med, per);
                ProgramacionBL.Guardar(progr);

                rm.SetResponse(true);
                if (string.IsNullOrEmpty(href))
                    rm.href = Url.Action("Index", "Persona");
                else
                    rm.href = href;
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