using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apache.Models
{
    public class ErrorHandler : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var ex = filterContext.Exception;
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            var errorModel = new HandleErrorInfo(ex, controllerName, actionName);

            var vd = new ViewDataDictionary();
            vd.Model = errorModel;

            //vd.Add("ErrMsg", ex.Message);

            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = vd
            };
        }
    }
}