using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using photographic_studio_api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Filters
{
    public class HttpGlobalAttribule : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                string ip = filterContext.HttpContext.GetClientUserIp();
                filterContext.ActionArguments.Add("ip", ip);
                string token = filterContext.HttpContext.Request.Headers["x-token"];
                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        var tokenInfo = CommonUtil.GetTokenInfo(token);
                        int uid = tokenInfo.Item1;
                        int role = tokenInfo.Item2;
                        filterContext.ActionArguments.Add("uid", uid);
                        filterContext.ActionArguments.Add("currentRole", role);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {

                base.OnActionExecuting(filterContext);
            }

        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var objectResult = context.Result as ObjectResult;
            context.Result = new OkObjectResult(HttpResult<object>.Result(Enums.HttpResultCode.SUCCESS, objectResult.Value));
            base.OnResultExecuting(context);
        }

    }
}
