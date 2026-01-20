using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Ruv.WebAPI.App_Start
{
    public class ElmahExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            ErrorLog.GetDefault(HttpContext.Current).Log(new Error(context.Exception, HttpContext.Current));
        }
    }
}