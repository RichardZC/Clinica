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
    public class PersonaController : Controller
    {
        // GET: Persona
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Buscar(string clave = "")
        {
            return PartialView(PersonaBL.ListarPersonas(clave.Trim()));
        }

        public ActionResult Mantener(int id ,string href)
        {
            var per = new persona() { Sexo="M"};
            if (id > 0)
                per = PersonaBL.Obtener(id);

            if (string.IsNullOrEmpty(href)) href = string.Empty;
            ViewBag.href = href ;

            return View(per);
        }

        public JsonResult ListarPersona()
        {
            return Json(BL.PersonaBL.Listar(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(persona per,string href)
        {
            var rm = new ResponseModel();
            per.NombreCompleto = per.Nombres + " " + per.Paterno + " " + per.Materno;
            try
            {
                var dnival = PersonaBL.Contar(x => x.DNI == per.DNI && x.PersonaId != per.PersonaId);
                if (dnival>0)
                {
                    rm.SetResponse(false, "El DNI ya existe, ingrese otro");
                    return Json(rm);
                }
                
                PersonaBL.Guardar(per);                
                rm.SetResponse(true);
                rm.href = string.IsNullOrEmpty(href) ? Url.Action("Index", "Persona") : href;
            }
            catch (Exception ex)
            {
                rm.SetResponse(false);
                rm.function = "fn.mensaje('" + ex.Message + "')";
            }

            return Json(rm);
        }

        public JsonResult ObtenerPersona(string dni)
        {
            return Json(BL.PersonaBL.Obtener(x=> x.DNI==dni), JsonRequestBehavior.AllowGet);
        }

    }
}