using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;
using BE;
using Comun;

namespace Web.Controllers.Clinica
{
    public class TopicoController : Controller
    {
        // GET: Topico
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mantener(int id)
        {
            topico top = new topico();
            top.AtencionId = id;
            ViewBag.listaRevision = BL.TablaConfiguracionBL.Listar(x => x.TablaId == 1);
            if (TopicoBL.Obtener(x => x.AtencionId == id) != null)
            {
                top.TopicoId = 1;
                ViewBag.listaTopico = TopicoBL.Listar(x => x.AtencionId == id);
            }
            else { top.TopicoId = 0; }
            return View(top);
        }

        [HttpPost]
        public JsonResult Guardar(List<topico> lista)
        {
            var rm = new ResponseModel();
            try
            {
                TopicoBL.Guardar(lista);
                rm.SetResponse(true);
                rm.href = Url.Action("Index", "Topico");
            }
            catch (Exception ex)
            {
                rm.SetResponse(false);
                rm.function = "fn.mensaje('" + ex.Message + "')";
            }

            return Json(rm);
        }
        public PartialViewResult Cita(string fecha)
        {
            DateTime fechaAtncion = DateTime.Parse(fecha);
            ViewBag.listaCitas = CitaBL.Listar(x => x.FechaAtencion == fechaAtncion, includeProperties: "Programacion,Paciente.Persona");
            return PartialView();
        }
        public PartialViewResult Atencion(int id)
        {
            if (AtencionBL.Contar(x => x.CitaId == id) > 0)
            {
                atencion a = AtencionBL.Obtener(x => x.CitaId == id, includeProperties: "Cita.Paciente.Persona");
                return PartialView(a);
            }
            else
            {
                cita c = CitaBL.Obtener(x => x.CitaId == id, includeProperties: "Paciente.Persona");
                atencion atencion = new atencion();
                atencion.AtencionId = 0;
                atencion.CitaId = id;
                atencion.Estado = "c";
                atencion.cita = c;
                return PartialView(atencion);
            }
        }

    }
}