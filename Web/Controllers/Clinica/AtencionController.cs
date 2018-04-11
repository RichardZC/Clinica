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
    public class AtencionController : Controller
    {
        // GET: Atencion
        public ActionResult Index()
        {
            int user_ID = Comun.SessionHelper.GetUser();
            ViewBag.usuario = BL.UsuarioBL.Obtener(x => x.UsuarioId == user_ID, includeProperties: "Persona");
            return View();
        }

        public ActionResult Mantener(int id)
        {
            int user_ID = Comun.SessionHelper.GetUser();
            usuario user = BL.UsuarioBL.Obtener(x => x.UsuarioId == user_ID, includeProperties: "Persona");
            medico med = MedicoBL.Obtener(x => x.PersonaId == user.PersonaId, includeProperties: "Persona,Especialidad");

            atencionespecialidad atenc = new atencionespecialidad();
            atenc.AtencionId = id;

            if (med.EspecialidadId == 4)
            {
                ViewBag.listaRevision = BL.TablaConfiguracionBL.Listar(x => x.TablaId == 2);
            }
            if (med.EspecialidadId == 5)
            {
                ViewBag.listaRevision = BL.TablaConfiguracionBL.Listar(x => x.TablaId == 3);
            }
            
            if (AtencionEspecialidadBL.Obtener(x => x.AtencionId == id) != null)
            {
                atenc.AtencionespecialidadId = 1;
                ViewBag.listaTopico = AtencionEspecialidadBL.Listar(x => x.AtencionId == id);
            }
            else { atenc.AtencionespecialidadId = 0; }

            
            return View(atenc);
        }

        [HttpPost]
        public JsonResult Guardar(atencion at)
        {
            var rm = new ResponseModel();
            try
            {
                AtencionBL.Guardar(at);
                rm.SetResponse(true);
                rm.href = Url.Action("Mantener", "Topico", new { id = at.AtencionId});
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