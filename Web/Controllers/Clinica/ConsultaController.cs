using BE;
using BL;
using Comun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers.Clinica
{
    [Autenticado]
    public class ConsultaController : Controller
    {
        // GET: Consulta
        public ActionResult Index()
        {
            //ViewBag.Medico = m.persona.NombreCompleto;
            ViewBag.cboEspecialidad = new SelectList(EspecialidadBL.Listar(), "EspecialidadId", "Denominacion");
            return View();
        }

        public ActionResult Detalle(int id)
        {
            ViewBag.cboExamen = new SelectList(TablaExamenBL.Listar(x => x.ItemId == 0 && x.Estado && !x.IndLab), "TablaId", "Denominacion");

            ViewBag.AtencionId = id;
            return View();
        }

        public JsonResult Obtener(int atencionId)
        {
            var atencion = AtencionBL.Obtener(x => x.AtencionId == atencionId, includeProperties: "persona,persona1,especialidad");
            var paciente = PacienteBL.Obtener(x => x.PersonaId == atencion.PerPacienteId);
            return Json(new
            {
                atencion.AtencionId,
                Paciente = atencion.persona.NombreCompleto,
                Dni = atencion.persona.DNI,
                Edad = atencion.persona.FechaNacimiento.HasValue ? (DateTime.Today.AddTicks(-atencion.persona.FechaNacimiento.Value.Ticks).Year - 1).ToString() : "--",
                Fecha = atencion.Fecha.ToShortDateString(),
                Especialidad = atencion.especialidad.Denominacion,
                Medico = atencion.persona1.NombreCompleto,
                atencion.IndPago,
                Pago = atencion.IndPago ? "PAGADO" : "NO PAGADO",
                Estado = Constante.ATENCION.Obtener(atencion.Estado),
                Diagnostico = atencion.Diagnostico,
                Tratamiento = atencion.Tratamiento,
                Motivo = atencion.Motivo,
                paciente
            }
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarExamenes(int atencionId)
        {
            var examenes = ExamenBL.Listar(x => x.AtencionId == atencionId);
            return Json(examenes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarTablaExamen(int tablaId)
        {
            return Json(TablaExamenBL
                .Listar(x => x.TablaId == tablaId)
                .Select(x => new examen { TablaId = x.TablaId, ItemId = x.ItemId, Denominacion = x.Denominacion, Valor = "" })
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarTablaLaboratorios()
        {
            return Json(TablaExamenBL.Listar(x => x.ItemId == 0 && x.IndLab && x.Estado)
                , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CrearExamen(List<examen> examen)
        {
            try
            {
                for (int i = 0; i < examen.Count; i++)
                {
                    if (examen[i].Valor == null)
                        examen[i].Valor = string.Empty;
                }
                ExamenBL.Guardar(examen);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult CrearOrdenLab(int pAtencionid, List<tablaexamen> examen)
        {
            try
            {
                var e = examen.Select(x => new examen
                {
                    AtencionId = pAtencionid,
                    Denominacion = x.Denominacion,
                    TablaId = x.TablaId,
                    ItemId = x.ItemId,
                    Valor = "P"
                }).ToList();

                ExamenBL.Guardar(e);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult EliminarExamen(int pAtencionid, int pExamenId)
        {
            try
            {
                return Json(ExamenBL.EliminarExamen(pAtencionid, pExamenId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult CrearAtencion(int PersonaId, int MedicoId, int cboEspecialidad)
        {
            var rm = new ResponseModel();
            try
            {
                if (PersonaId < 0)
                {
                    rm.SetResponse(false, "El paciente es requerido");
                    return Json(rm);
                }
                var at = AtencionBL.Guardar(new BE.atencion
                {
                    PerPacienteId = PersonaId,
                    EspecialidadId = cboEspecialidad,
                    Estado = Constante.ATENCION.Pendiente,
                    PerMedicoId = MedicoId,
                    Fecha = DateTime.Now,
                    FechaCreacion = DateTime.Now,
                    IndPago = false,
                    UsuarioCId = SessionHelper.GetUser()
                });
                rm.SetResponse(true);
                rm.function = "swal(  'Ok!',  'Se creo la atencion correctamente!',  'success');";
                rm.href = Url.Action("Detalle", "Consulta") + '/' + at.AtencionId.ToString();
            }
            catch (Exception ex)
            {
                rm.SetResponse(false);
                rm.function = "fn.mensaje('" + ex.Message + "')";
            }

            return Json(rm);
        }

        public JsonResult ListarAtenciones(string nombre = "")
        {
            var listaatencion = new List<atencion>();
            if (string.IsNullOrEmpty(nombre))
                listaatencion = AtencionBL.Listar(orderBy: x => x.OrderByDescending(y => y.Fecha),
                    includeProperties: "persona,persona1,especialidad").Take(10).ToList();
            else
                listaatencion = AtencionBL.Listar(x => x.persona.NombreCompleto.Contains(nombre),
                x => x.OrderByDescending(y => y.Fecha), "persona,persona1,especialidad");

            return Json(listaatencion
                .Select(x => new
                {
                    x.AtencionId,
                    Paciente = x.persona.NombreCompleto,
                    Fecha = x.Fecha.ToShortDateString(),
                    Especialidad = x.especialidad.Denominacion,
                    Medico = x.persona1.NombreCompleto,
                    x.IndPago,
                    Pago = x.IndPago ? "PAGADO" : "NO PAGADO"
                })
                , JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Guardar(int AtencionId, string txtDiagnostico, string txtTratamiento,string txtMotivo)
        {
            var rm = new ResponseModel();
            try
            {
                AtencionBL.ActualizarParcial(new atencion
                {
                    AtencionId = AtencionId,
                    FechaModificacion = DateTime.Now,
                    UsuarioMId = SessionHelper.GetUser(),
                    Diagnostico = txtDiagnostico,
                    Tratamiento = txtTratamiento,
                    Motivo = txtMotivo,
                    Estado = Constante.ATENCION.Terminado
                }, x => x.FechaModificacion, x => x.UsuarioMId, x => x.Tratamiento, x => x.Motivo, x => x.Diagnostico, x => x.Estado);
                
                rm.SetResponse(true, "Se Guardó Correctamente");
                //rm.function = "swal(  'Ok!',  'Se Guardó correctamente!',  'success');";
               // rm.href = Url.Action("Detalle", "Consulta") + '/' + at.AtencionId.ToString();
            }
            catch (Exception ex)
            {
                rm.SetResponse(false);
                rm.function = "fn.mensaje('" + ex.Message + "')";
            }

            return Json(rm);
        }
        [HttpPost]
        public JsonResult GuardarPaciente(paciente pac)
        {
            var rm = new ResponseModel();

            if (pac.PersonaId == 0)
            {
                rm.SetResponse(false, "Ingrese a la Persona");
                return Json(rm);
            }            
            try
            {
                PacienteBL.Guardar(pac);
                rm.SetResponse(true,"Se Guardó Correctamente");
                //rm.function = "fn.mensaje('Se guardo Correctamente')";
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