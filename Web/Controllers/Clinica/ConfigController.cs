using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            ViewBag.lstConsultorio = ConsultorioBL.Listar().Select(x => new BL.Modelo.Tabla { id = x.ConsultorioId, Denominacion = x.Denominacion }).ToList();
            ViewBag.lstEspecialidad = EspecialidadBL.Listar().Select(x => new BL.Modelo.Tabla { id = x.EspecialidadId, Denominacion = x.Denominacion }).ToList();
            return View();
        }

       

    }
}