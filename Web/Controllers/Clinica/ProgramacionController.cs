using BE;
using BL;
using Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers.Clinica
{
    public class ProgramacionController : Controller
    {
        // GET: Progr
        public ActionResult Index()
        {
            programacion progr = new programacion();
            progr.ProgramacionId = 0;
            ViewBag.cboHoras = new SelectList(ProgramacionBL.Horas(), "Id", "Valor");
            return View(progr);
        }

        public JsonResult GetEvents()
        {
            var clientList = new List<object>();
            foreach (var e in ProgramacionBL.Listar(includeProperties: "Medico"))
            {
                clientList.Add(new
                {
                    id = e.ProgramacionId,
                    title = BL.PersonaBL.Obtener(e.medico.PersonaId).NombreCompleto + "\n" + BL.EspecialidadBL.Obtener(e.medico.EspecialidadId).Denominacion
                    + "\n" + e.HoraInicio + " - " + e.HoraFin,
                    start = e.FechaInicio.ToString("yyyy-MM-dd") + "T" + e.HoraInicio,
                    end = e.FechaLimite.Value.ToString("yyyy-MM-dd") + "T" + e.HoraFin
                });
            }
            return Json(clientList.ToArray(), JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public JsonResult Guardar(programacion progr, string pEstado, string pRepite, string pRepiteSemana)
        {
            var rm = new ResponseModel();

            try
            {

                if (!string.IsNullOrEmpty(pEstado)) { progr.Estado = true; } else { progr.Estado = false; }
                if (!string.IsNullOrEmpty(pRepite)) { progr.Repite = true; } else { progr.Repite = false; }
                if (!string.IsNullOrEmpty(pRepiteSemana)) { progr.Semanal = true; } else { progr.Semanal = false; }

                if (progr.Repite.Value)
                {
                    DateTime Fecini = progr.FechaInicio;
                    DateTime FecFinal = progr.FechaLimite.Value;
                    System.TimeSpan dif = FecFinal - Fecini;
                    DateTime FecSec;
                    if (progr.Repite.Value && progr.Semanal.Value)
                    {
                        for (int n = 0; n <= dif.Days; n++)
                        {
                            FecSec = Fecini.AddDays(n);
                            if (FecSec.DayOfWeek != DayOfWeek.Sunday)
                            {
                                progr.FechaInicio = FecSec;
                                progr.FechaLimite = FecSec;
                                ProgramacionBL.Crear(progr);
                            }
                        }
                    }
                    else
                    {
                        for (int n = 0; n <= dif.Days; n++)
                        {
                            FecSec = Fecini.AddDays(n);
                            if (progr.FechaInicio.DayOfWeek == FecSec.DayOfWeek)
                            {
                                progr.FechaInicio = FecSec;
                                progr.FechaLimite = FecSec;
                                ProgramacionBL.Crear(progr);
                            }
                        }
                    }
                }
                else
                {
                    progr.FechaLimite = progr.FechaInicio;
                    ProgramacionBL.Guardar(progr);
                }

                rm.SetResponse(true);
                rm.href = Url.Action("Index", "Programacion");
                //rm.function = "cargar();";
            }
            catch (Exception ex)
            {
                rm.SetResponse(false);
                rm.function = "fn.mensaje('" + ex.Message + "')";
            }
            return Json(rm);
        }

        public ActionResult Mantener(int id)
        {
            ViewBag.cboHoras = new SelectList(ProgramacionBL.Horas(), "Id", "Valor");
            programacion prog = ProgramacionBL.Obtener(x => x.ProgramacionId == id, includeProperties: "Medico.Persona");
            return View(prog);
        }
    }
}