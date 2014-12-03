using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AddRoutes.Controllers
{
    class HomeController : Controller
    {
        public ActionResult About()
        {
            return Content("This is the About page");
        }
    }
}
