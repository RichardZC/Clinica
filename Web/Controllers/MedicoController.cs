using BE;
using BL;
using Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    [Autenticado]
    public class MedicoController : Controller
    {
        // GET: Medico
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Listar()
        {
            return Json(BL.MedicoBL.Listar(includeProperties: "Persona").Select(x => new
            {
                PersonaId = x.PersonaId,
                MedicoId = x.MedicoId,
                Titulo = x.TituloProfesional,
                Nombres = x.persona.NombreCompleto,
                DNI = x.persona.DNI,
                Celular = x.persona.Celular,
                Correo = x.persona.Correo
            }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Mantener(int id)
        {
            var med = new medico();
            if (id > 0)
                med = MedicoBL.Obtener(includeProperties:"Persona");

            ViewBag.cboEspecialidad = new SelectList(EspecialidadBL.Listar(), "EspecialidadId", "Denominacion");
            return View(med);
        }


        [HttpPost]
        public JsonResult Guardar(medico med)
        {
            var rm = new ResponseModel();
            if (med.PersonaId==0)
            {
                rm.SetResponse(false, "Ingrese a la Persona");
                return Json(rm);
            }

            
            try
            {
                MedicoBL.Guardar(med);
                rm.SetResponse(true);
                
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