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
            //ViewBag.Pacientes = BL.PacienteBL.Listar(includeProperties:"Persona");
            return View();
        }

        public ActionResult Mantener(int id)
        {
            atencion atenc = new atencion();
            if (id > 0)
            {
                atenc = AtencionBL.Obtener(id);
            }
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


        public PartialViewResult HistoriaPaciente(int id)
        {
            int user_ID = Comun.SessionHelper.GetUser();
            ViewBag.usuario = BL.UsuarioBL.Obtener(x => x.UsuarioId == user_ID, includeProperties: "Persona");
            ViewBag.Pacientes = BL.AtencionBL.Listar( x=> x.PerPacienteId==id ,includeProperties: "Persona,Persona.Paciente");
            return PartialView();
        }


        public PartialViewResult VistaResumen(int id)
        {

            return PartialView();

        }
        public PartialViewResult VistaExamen(int id)
        {
            return PartialView();

        }
        public PartialViewResult VistaProcedimiento(int id)
        {
            return PartialView();

        }
        public PartialViewResult VistaAtencion(int id)
        {
            atencionespecialidad atencionEspecial = new atencionespecialidad();
            atencionEspecial.Item = 2;
            ViewBag.cboExamenesAtencion = TablaExamenBL.Listar();
            return PartialView(atencionEspecial);
        }
        public PartialViewResult VistaRecetas(int id)
        {
            return PartialView();

        }

        public JsonResult ComboAtencion(string fechaCita)
        {
           
            return Json(TablaExamenBL.Listar(x => x.TablaId == 2)
                         .Select(x => 
                         new { Id = x.ItemId, Valor = x.Denominacion }).ToList(), JsonRequestBehavior.AllowGet);

            //return Json(ProgramacionBL.Listar(x => x.FechaInicio == fecha, includeProperties: "Medico")
            //              .Select(x => new { Id = EspecialidadBL.Obtener(x.medico.EspecialidadId).EspecialidadId, Valor = EspecialidadBL.Obtener(x.medico.EspecialidadId).Denominacion + "-" + x.HoraInicio + " " + x.HoraFin }).ToList(), JsonRequestBehavior.AllowGet);

        }
    }
}