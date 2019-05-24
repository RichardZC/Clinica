using BE;
using BL;
using System.Linq;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers.Clinica
{
    [Autenticado]
    public class ConfigController : Controller
    {
        // GET: Mantenimiento
        public ActionResult Index()
        {
            ViewBag.lstConsultorio = ConsultorioBL.Listar().Select(x => new BL.Modelo.Tabla { Id = x.ConsultorioId, Denominacion = x.Denominacion }).ToList();
            ViewBag.lstEspecialidad = EspecialidadBL.Listar().Select(x => new BL.Modelo.Tabla { Id = x.EspecialidadId, Denominacion = x.Denominacion }).ToList();
            return View();
        }

        public JsonResult ListarExamen(int pTablaId)
        {
            return Json(BL.TablaExamenBL.Listar(x => x.TablaId == pTablaId && x.ItemId > 0)
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarTablaExamen()
        {
            return Json(BL.TablaExamenBL.Listar(x => x.ItemId == 0)
                , JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarTablaExamen(tablaexamen pExamen)
        {
            pExamen.Denominacion = pExamen.Denominacion.ToUpper();
            pExamen.ItemId = 0;
            if (pExamen.TablaId == 0)
                pExamen.TablaId = TablaExamenBL.ObtenerNuevoTablaId();

            var examen = TablaExamenBL.Guardar(pExamen);

            return Json(examen, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GuardarExamen(tablaexamen pExamen)
        {
            pExamen.Denominacion = pExamen.Denominacion.ToUpper();
            pExamen.Unidad = pExamen.Unidad.ToUpper();

            if (pExamen.ItemId == 0)
                pExamen.ItemId = TablaExamenBL.ObtenerNuevoId(pExamen.TablaId);

            var examen = TablaExamenBL.Guardar(pExamen);

            return Json(examen, JsonRequestBehavior.AllowGet);
        }
    }
}