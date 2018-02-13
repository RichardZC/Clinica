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
    public class ArticuloController : Controller
    {
        // GET: Articulo
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Buscar(string clave = "")
        {
            return PartialView(ArticuloBL.ListarArticulos(clave.Trim()));
        }
        public ActionResult Mantener(int id, string href)
        {
            var per = new articulo() { Estado = true, IndServicio = false };
            if (id > 0)
                per = ArticuloBL.Obtener(id);

            if (string.IsNullOrEmpty(href)) href = string.Empty;
            ViewBag.href = href;
            ViewBag.cboTipoArticulo = new SelectList(TipoArticuloBL.Listar(), "TipoArticuloId", "Denominacion") ;
            ViewBag.cboModelo = ModeloBL.Listar();

            return View(per);
        }

        [HttpPost]
        public JsonResult Guardar(persona per, string href)
        {
            var rm = new ResponseModel();
            per.NombreCompleto = per.Nombres + " " + per.Paterno + " " + per.Materno;
            try
            {
                PersonaBL.Guardar(per);
                rm.SetResponse(true);
                if (string.IsNullOrEmpty(href))
                    rm.href = Url.Action("Index", "Persona");
                else
                    rm.href = href;
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