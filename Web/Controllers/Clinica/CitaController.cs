using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Filters;

namespace Web.Controllers
{
    public class CitaController : Controller
    {
        [Autenticado]
        public ActionResult Index()
        {
            return View();
        }
    }
}