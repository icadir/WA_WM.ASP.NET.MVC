using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Web.UI.App_Code
{
    public class ExceptionHandlerFilterAttribute : ActionFilterAttribute, IExceptionFilter
    {
        // neden 2 sini kullanıyoruz. action filter ypama için bu classtan kalıtm almak gerekiyor. polymorphizim söz konusu. Iexception filter burada hataları yakalamak için kullanılıyor.,
        //gidiyorsun birde controller üstüne yazıyorsn. Verdigin ismi. Hoca kuzey uygulamasında yaptı oradan dene.
        //asp.net mvc5 de attribute routing bora makaleyi oku.

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                filterContext.Result = new JsonResult()
                {
                    ContentType = "application/json",
                    Data = new
                    {
                        name = filterContext.Exception.GetType().Name,
                        message = filterContext.Exception.Message,
                        trace = filterContext.Exception.StackTrace
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.ExceptionHandled = true;
                //Internal Error
            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var data = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(data)
                };
                filterContext.HttpContext.Response.StatusCode = 500;
                filterContext.ExceptionHandled = true;
            }
        }
    }
}