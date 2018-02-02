using System;
using System.Net;  
using System.Net.Http;  
using System.Web.Http.Filters;
using MyEvernote.Logger;


namespace MyEvernote.Api.Controllers
{
    public class MyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                Log.Instance.Info("Not Implemented Exception");
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            if (context.Exception is Exception)
            {
                Log.Instance.Info("хьюстон , у нас проблемы");
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}