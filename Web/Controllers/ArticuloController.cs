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
            ViewBag.cboTipoArticulo = TipoArticuloBL.Listar();
            ViewBag.cboModelo = ModeloBL.Listar();

            return View(per);
        }

        [HttpPost]
        public JsonResult Guardar(articulo art, string href, string Estado, string IndServicio)
        {
            var rm = new ResponseModel();
            try
            {
                if (!string.IsNullOrEmpty(Estado)) art.Estado = true;
                if (!string.IsNullOrEmpty(IndServicio)) art.IndServicio = true;
                ArticuloBL.Guardar(art);
                rm.SetResponse(true);
                if (string.IsNullOrEmpty(href))
                    rm.href = Url.Action("Index", "Articulo");
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