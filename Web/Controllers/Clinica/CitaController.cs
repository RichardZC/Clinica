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
    public class CitaController : Controller
    {
        [Autenticado]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Mantener(int id)
        {
            var cita = new cita();
            cita.IndPago = false;
            if (id > 0)
            {
                cita = CitaBL.Obtener(x => x.CitaId == id, includeProperties: "Paciente");
                ViewBag.NombreCompleto = PersonaBL.Obtener(x => x.PersonaId == cita.paciente.PersonaId).NombreCompleto;
            }
            ViewBag.cboConceptoPago = new SelectList(ConceptopagoBL.Listar(), "ConceptoPagoId", "Denominacion");
            return View(cita);
        }

        public JsonResult ComboEspecialidadCita(string fechaCita)
        {
            DateTime fecha = DateTime.Parse(fechaCita);

            return Json(ProgramacionBL.Listar(x => x.FechaInicio == fecha, includeProperties: "Medico")
                          .Select(x => new { Id = EspecialidadBL.Obtener(x.medico.EspecialidadId).EspecialidadId, Valor = EspecialidadBL.Obtener(x.medico.EspecialidadId).Denominacion + "-" + x.HoraInicio + " " + x.HoraFin }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerNumeroCita(string fechaCita,int EspecialidadId)
        {

            DateTime fecha = DateTime.Parse(fechaCita);
            TimeSpan tiempoPorAtencion = new TimeSpan(0, 20, 0);
            int numeroCita = 1;
            bool estado = true;
            TimeSpan HoraProbable = new TimeSpan();
            if (fecha.DayOfWeek != DayOfWeek.Sunday)
            {
                programacion p = ProgramacionBL.Obtener(x => x.FechaInicio == fecha && x.medico.EspecialidadId == EspecialidadId, includeProperties: "Medico");
                cita c = CitaBL.Obtener(x => x.FechaAtencion == fecha && x.ProgramacionId == p.ProgramacionId);
                int numeroCitaHabiles = (int)(p.HoraFin - p.HoraInicio).TotalMinutes / (int)tiempoPorAtencion.TotalMinutes;
                HoraProbable = p.HoraInicio;
                if (c != null)
                {
                    if (CitaBL.Contar(x=> x.ProgramacionId == p.ProgramacionId) < numeroCitaHabiles)
                    {
                        numeroCita = CitaBL.Contar(x => x.ProgramacionId == p.ProgramacionId) + 1;
                        HoraProbable = p.HoraInicio + TimeSpan.FromTicks(tiempoPorAtencion.Ticks * (numeroCita-1 ));
                    }
                    else { estado = false; }
                }
            }
            var clientList = new List<object>();
            clientList.Add(new
            {
                estado = estado,
                numeroCita = numeroCita,
                horaProbable = HoraProbable.ToString()
            });
            return Json(clientList.ToArray(), JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult Guardar(cita cita, int pEspecialidad)
        {
            var rm = new ResponseModel();

            try
            {
                cita.IndPago = false;
                cita.Estado = Comun.Constante.CITA.Pendiente;
                cita.ProgramacionId = ProgramacionBL.Obtener(x => x.medico.especialidad.EspecialidadId== pEspecialidad && x.FechaInicio == cita.FechaAtencion).ProgramacionId;
                CitaBL.Guardar(cita);
                rm.SetResponse(true);
                rm.href = Url.Action("Index", "Cita");
            }
            catch (Exception ex)
            {
                rm.SetResponse(false);
                rm.function = "fn.mensaje('" + ex.Message + "')";
            }
            return Json(rm);
        }

        public PartialViewResult Cita(int id)
        {
            ViewBag.listaCitas = CitaBL.Listar(x => x.ProgramacionId == id, includeProperties: "Programacion,Paciente.Persona");
            int meid = ProgramacionBL.Obtener(id).MedicoId;
            medico med = MedicoBL.Obtener(x => x.MedicoId == meid, includeProperties: "Especialidad,Persona");
            return PartialView(med);
        }
    }
}