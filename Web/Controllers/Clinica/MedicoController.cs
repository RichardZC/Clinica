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
                Correo = x.persona.Correo,
                Estado = x.Estado
            }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Mantener(int id)
        {
            var med = new medico();
            med.Estado = true;
            if (id > 0)
                med = MedicoBL.Obtener(x=>x.MedicoId==id, includeProperties:"Persona");

            ViewBag.cboEspecialidad = new SelectList(EspecialidadBL.Listar(), "EspecialidadId", "Denominacion");
            return View(med);
        }


        [HttpPost]
        public JsonResult Guardar(medico med, string pEstado)
        {
            var rm = new ResponseModel();
            
            if (med.PersonaId==0)
            {
                rm.SetResponse(false, "Ingrese a la Persona");
                return Json(rm);
            }
            var val = MedicoBL.Contar(x => x.MedicoId!=med.MedicoId && x.PersonaId == med.PersonaId);
            if (val > 0)
            {
                rm.SetResponse(false, "Ya existe el médico para esta persona, ingrese otro");
                return Json(rm);
            }
            try
            {
                if (!string.IsNullOrEmpty(pEstado)) med.Estado = true;
                MedicoBL.Guardar(med);
                rm.SetResponse(true);
                rm.href = Url.Action("Index", "Medico");
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