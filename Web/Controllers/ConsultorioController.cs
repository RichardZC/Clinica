using BL;
using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comun;

namespace Web.Controllers
{
    public class ConsultorioController : Controller
    {
        // GET: Consultorio
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult ObtenerConsultorios()
        {
            return Json(BL.ConsultorioBL.Listar(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Mantener(int id, string href)
        {
            var med = new medico();
            if (id > 0)
                med = MedicoBL.Obtener(
                    );

            if (string.IsNullOrEmpty(href)) href = string.Empty;
            ViewBag.href = href;
            return View(med);
        }



        [HttpPost]
        public JsonResult GuardarConsultorio(consultorio consul, string href)
        {

            var rm = new ResponseModel();
            //per.NombreCompleto = per.Nombres + " " + per.Paterno + " " + per.Materno;
            try
            {
                //MedicoBL.guardarMedico(med, per);
                ConsultorioBL.Guardar(consul);

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