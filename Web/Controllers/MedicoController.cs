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
    public class MedicoController : Controller
    {
        // GET: Medico
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerMedico()
        {
            return Json(BL.MedicoBL.Listar(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Mantener(int id, string href)
        {
            ViewBag.cboEspecialidad = new SelectList(EspecialidadBL.Listar(), "EspecialidadId", "NombreEspecialidad");
            var med = new medico();
            if (id > 0)
                med = MedicoBL.Obtener(
                    );

            if (string.IsNullOrEmpty(href)) href = string.Empty;
            ViewBag.href = href;
            return View(med);
        }


        [HttpPost]
        public JsonResult GuardarMedico(medico med, persona per, string href)
        {

            var rm = new ResponseModel();
            per.NombreCompleto = per.Nombres + " " + per.Paterno + " " + per.Materno;
            try
            {
                MedicoBL.guardarMedico(med,per);
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

        public JsonResult getMedico()
        {
            return Json(BL.MedicoBL.listarMedico(), JsonRequestBehavior.AllowGet);
        }



    }
}