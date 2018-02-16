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
            
            return View(med);
        }


        [HttpPost]
        public JsonResult GuardarMedico(medico med, persona per, string href)
        {

            var rm = new ResponseModel();
            per.NombreCompleto = per.Nombres + " " + per.Paterno + " " + per.Materno;
            try
            {
                MedicoBL.guardarMedico(med, per);
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