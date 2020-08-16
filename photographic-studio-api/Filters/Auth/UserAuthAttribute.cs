using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using photographic_studio_api.Context;
using photographic_studio_api.Enums;
using photographic_studio_api.Exceptions;
using photographic_studio_api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Filters.Auth
{
    public class UserAuthAttribute : ActionFilterAttribute
    {
        private UserRole[] permissions;
        public UserAuthAttribute()
        {
            permissions = new UserRole[0];
        }
        public UserAuthAttribute(params UserRole[] permissions)
        {
            this.permissions = permissions;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                int noFilter = filterContext.Filters.Count(m => m.GetType() == typeof(NoFilter));
                if (noFilter == 0)
                {
                    string token = filterContext.HttpContext.Request.Headers["x-token"];
                    if (string.IsNullOrEmpty(token))
                    {
                        throw new ApiException(HttpResultCode.NOAUTH);
                    }
                    if (filterContext.ActionArguments.Keys.Contains("uid") && filterContext.ActionArguments.Keys.Contains("currentRole"))
                    {
                        int uid = int.Parse(filterContext.ActionArguments["uid"].ToString());
                        int role = int.Parse(filterContext.ActionArguments["currentRole"].ToString());
                        (bool, int, int) t = CheckToken(token, uid, role);
                        if (t.Item1)
                        {
                            Cache.GetInstance().Set<int>(token, t.Item2);

                            if (this.permissions.Length > 0)
                            {
                                if (CheckRole(t.Item2, permissions, t.Item3))
                                {
                                    base.OnActionExecuting(filterContext);
                                }
                                else
                                {
                                    throw new ApiException(HttpResultCode.权限错误);
                                }
                            }
                            else
                            {
                                base.OnActionExecuting(filterContext);
                            }
                        }
                        else
                        {
                            throw new ApiException(HttpResultCode.NOAUTH);
                        }
                    }
                    else
                    {
                        throw new ApiException(HttpResultCode.NOAUTH);
                    }



                }
            }
            catch (Exception)
            {

                throw new ApiException(HttpResultCode.NOAUTH);
            }

        }
        private bool CheckRole(int uid, UserRole[] permissions, int role)
        {
            var uRole = (UserRole)role;
            return permissions.Count(m => m == uRole) > 0;
        }
        private (bool, int, int) CheckToken(string token, int uid, int role)
        {

            int? cacheUid = Cache.GetInstance().Get<int?>(token);
            if (cacheUid == uid)
            {
                return (true, uid, role);
            }
            using (var serviceScope = ServiceLocator.Instance.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<photographicDbcontext>();
                var u = dbContext.UserInfo.Where(m => m.Id == uid).Select(m => new {
                    LoginDate = m.LoginDate,
                    Role = m.Role
                }).FirstOrDefault();
                role = u.Role;
                return (u != null && token == Utils.CommonUtil.CreateToken(uid, u.LoginDate, role), uid, role);
            }
            //var repository = (Repository)ServiceLocator.Instance.GetService(typeof(Repository));//IOC.GetService<Repository>();
        }

    }
}
