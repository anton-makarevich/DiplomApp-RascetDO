using CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace DiplomApi.Controllers
{
    public class DiplomController : ApiController
    {
        [HttpPost]
        public PavementModel Calculate(PavementModel model)
        {
            var path = HttpContext.Current.Server.MapPath("~/ndt");
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru-RU");
            var calc = new CalcModule.CalcModule(path);
            return calc.Calculate(model);
        }
    }
}
