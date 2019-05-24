using BE;
using BL;
using Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers.Clinica
{
    [Autenticado]
    public class PacienteController : Controller
    {
        // GET: Paciente
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult Listar(string clave = "")
        {
            return Json(BL.PacienteBL.Listar(
                x => x.persona.NombreCompleto.Contains(clave),
                includeProperties: "Persona").Select(x => new
                {
                    PersonaId = x.PersonaId,
                    PacienteId = x.PacienteId,
                    NumeroHistoria = x.NumeroHistoria,
                    Nombres = x.persona.NombreCompleto,
                    DNI = x.persona.DNI,
                    Celular = x.persona.Celular,
                    Correo = x.persona.Correo,
                    Alergia = x.Alergia
                }), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Mantener(int id)
        {
            var pac = new paciente();
            if (id > 0)
                pac = PacienteBL.Obtener(x => x.PacienteId == id, includeProperties: "Persona");
            return View(pac);
        }

        [HttpPost]
        public JsonResult Guardar(paciente pac)
        {
            var rm = new ResponseModel();

            if (pac.PersonaId == 0)
            {
                rm.SetResponse(false, "Ingrese a la Persona");
                return Json(rm);
            }
            var val = PacienteBL.Contar(x => x.PacienteId != pac.PacienteId && x.PersonaId == pac.PersonaId);
            if (val > 0)
            {
                rm.SetResponse(false, "Este paciente ya existe, ingrese otro");
                return Json(rm);
            }
            try
            {
                PacienteBL.Guardar(pac);
                rm.SetResponse(true);
                rm.href = Url.Action("Index", "Paciente");
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