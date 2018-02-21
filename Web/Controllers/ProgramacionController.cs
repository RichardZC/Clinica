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
            ViewBag.cboEspecialidad = new SelectList(MedicoBL.Listar(), "MedicoId", "Especialidad");
            ViewBag.cboPersona = new SelectList(MedicoBL.listarMedico(), "PersonaId", "NombreCompleto");
            ViewBag.cboConsultorio = new SelectList(ConsultorioBL.Listar(), "ConsultorioId", "Denominacion");
            //ViewBag.cboModelo = ModeloBL.Listar();
            return View();
        }


        [HttpPost]
        public JsonResult GuardarProgramacion(programacion progr, string href)
        {
            var rm = new ResponseModel();
            
            try
            {
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