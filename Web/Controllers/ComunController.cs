using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ComunController : Controller
    {
        // GET: Comun
        public ActionResult ObtenerUsuarioSesion()
        {
            return Json(Comun.SessionHelper.GetUser(),JsonRequestBehavior.AllowGet);
        }
        

        public JsonResult ListarPersonas()
        {
            return Json(PersonaBL.Listar()
                          .Select(x => new { value = x.DNI + " - " + x.NombreCompleto, data = x.PersonaId })
                          .ToList()
            , JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarTabla(string tabla, BL.Modelo.Tabla data)
        {
            data.Denominacion = data.Denominacion.ToUpper();
            switch (tabla)
            {
                case "OFICINA":
                    var o = new BE.oficina { OficinaId = data.id, Denominacion = data.Denominacion };
                    OficinaBL.Guardar(o);
                    return Json(o.OficinaId);
                case "CARGO":
                    var c = new BE.cargo { CargoId = data.id, Denominacion = data.Denominacion };
                    CargoBL.Guardar(c);
                    return Json(c.CargoId);
                case "TIPOARTICULO":
                    var t = new BE.tipoarticulo { TipoArticuloId = data.id, Denominacion = data.Denominacion };
                    TipoArticuloBL.Guardar(t);
                    return Json(t.TipoArticuloId);
                case "MARCA":
                    var m = new BE.marca { MarcaId = data.id, Denominacion = data.Denominacion };
                    MarcaBL.Guardar(m);
                    return Json(m.MarcaId);
                case "CONSULTORIO":
                    var cc = new BE.consultorio { ConsultorioId = data.id, Denominacion = data.Denominacion };
                    ConsultorioBL.Guardar(cc);
                    return Json(cc.ConsultorioId);
                case "ESPECIALIDAD":
                    var e = new BE.especialidad { EspecialidadId = data.id, Denominacion = data.Denominacion };
                    EspecialidadBL.Guardar(e);
                    return Json(e.EspecialidadId);
            }
            return Json(0);
        }
    }
}