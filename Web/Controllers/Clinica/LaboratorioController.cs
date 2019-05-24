using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers.Clinica
{
    [Autenticado]
    public class LaboratorioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarDetalleExamen(int pAtencionId, int pTablaId)
        {
            var pen = ExamenBL.Contar(x => x.AtencionId == pAtencionId && x.TablaId == pTablaId && x.ItemId == 0 && x.Valor == "P");


            if (pen == 0)
            {
                var a = ExamenBL.Listar(x => x.AtencionId == pAtencionId && x.TablaId == pTablaId && x.ItemId > 0);
                return Json(a, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(BL.TablaExamenBL.Listar(x => x.TablaId == pTablaId && x.ItemId > 0)
                    .Select(x => new examen
                    {
                        AtencionId = pAtencionId,
                        Denominacion = x.Denominacion,
                        TablaId = x.TablaId,
                        ItemId = x.ItemId,
                        Valor = ""
                    })
                    , JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarExamen(List<examen> detalleExamen)
        {
            try
            {
                for (int i = 0; i < detalleExamen.Count; i++)
                {
                    if (detalleExamen[i].Valor == null)
                        detalleExamen[i].Valor = string.Empty;
                }
                ExamenBL.Guardar(detalleExamen);

                if (detalleExamen.Count > 0)
                {
                    ExamenBL.ActualizarParcial(new examen
                    {
                        AtencionId = detalleExamen[0].AtencionId,
                        TablaId = detalleExamen[0].TablaId,
                        ItemId = 0,
                        Valor = string.Empty
                    }, x => x.Valor);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult ListarExamenes(string nombre = "")
        {
            return Json(ExamenBL.ListarExamenes(nombre)
                , JsonRequestBehavior.AllowGet);
        }
    }
}