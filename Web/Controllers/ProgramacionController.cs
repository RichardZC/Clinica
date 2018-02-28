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
            programacion progr = new programacion();
            progr.ProgramacionId = 0;
            ViewBag.cboPersona = new SelectList(MedicoBL.listarMedico(), "PersonaId", "NombreCompleto");
            ViewBag.cboConsultorio = new SelectList(ConsultorioBL.Listar(), "ConsultorioId", "Denominacion");
            ViewBag.listaHorario = ProgramacionBL.Listar() ;
            return View(progr);
        }


        [HttpPost]
        public JsonResult Guardar(programacion progr, string pEstado, string pRepite)
        {
            var rm = new ResponseModel();

            try
            {
                if (!string.IsNullOrEmpty(pEstado)) progr.Estado = true;
                if (!string.IsNullOrEmpty(pRepite)) progr.Repite = true;
                ProgramacionBL.Guardar(progr);
                rm.SetResponse(true);
                rm.href = Url.Action("Index", "Programacion");
            }
            catch (Exception ex)
            {
                rm.SetResponse(false);
                rm.function = "fn.mensaje('" + ex.Message + "')";
            }

            return Json(rm);

            //return Json(progr, JsonRequestBehavior.AllowGet);
        }
    }
}