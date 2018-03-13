using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers.Clinica
{
    [Autenticado]
    public class AtencionController : Controller
    {
        // GET: Atencion
        public ActionResult Index()
        {
            return View();
        }
    }
}