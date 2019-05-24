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
    public class MantenimientoController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.lstOficina = OficinaBL.Listar().Select(x => new BL.Modelo.Tabla { Id = x.OficinaId, Denominacion = x.Denominacion }).ToList();
            ViewBag.lstCargo = CargoBL.Listar().Select(x => new BL.Modelo.Tabla { Id = x.CargoId, Denominacion = x.Denominacion }).ToList();
            ViewBag.lstTipoArticulo = TipoArticuloBL.Listar().Select(x => new BL.Modelo.Tabla { Id = x.TipoArticuloId, Denominacion = x.Denominacion }).ToList();
            ViewBag.lstMarca = MarcaBL.Listar().Select(x => new BL.Modelo.Tabla { Id = x.MarcaId, Denominacion = x.Denominacion }).ToList();
            return View();
        }
        
        public JsonResult ComboMarca()
        {
            return Json(BL.MarcaBL.Listar()
                .Select(x => new { Id = x.MarcaId, Valor = x.Denominacion })
                , JsonRequestBehavior.AllowGet);
        }
        public JsonResult GuardarModelo(modelo r)
        {
            bool Esnuevo = r.ModeloId == 0 ? true : false;
            //r.Denominacion = r.Denominacion.ToUpper();
            var rm = new ResponseModel();

            try
            {
                ModeloBL.Guardar(r);
                rm.SetResponse(true);
                if (Esnuevo)
                {
                    rm.function = "AddRowOfR(" + r.ModeloId + ",'" + r.Denominacion + "'," + r.MarcaId + ");fn.notificar();";
                }
                else
                {
                    rm.function = "RefreshRowOfR(" + r.ModeloId + ",'" + r.Denominacion + "'," + r.MarcaId + ");fn.notificar();";
                }

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