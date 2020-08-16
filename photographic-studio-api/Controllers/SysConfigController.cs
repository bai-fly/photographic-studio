using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using photographic_studio_api.Context;
using photographic_studio_api.Enums;
using photographic_studio_api.Exceptions;
using photographic_studio_api.Filters.Auth;
using photographic_studio_api.Models;

namespace photographic_studio_api.Controllers
{
    public class SysConfigController : BaseController
    {
        photographicDbcontext dbContext;
        public SysConfigController(photographicDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> Get([FromHeader] int? uid, [FromHeader] string ip, [FromForm]string key, [FromForm] string referrer)
        {
            var value = await dbContext.SysConfig.Where(m => m.Key == key).Select(m => m.Value).FirstOrDefaultAsync();
          
            return value;
        }

        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<List<SysConfig>> List([FromForm] int index, [FromForm] int pageSize)
        {
            return await dbContext.SysConfig.OrderByDescending(m => m.Id).Skip((index - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        [HttpPost]
        public async Task<SysConfig> GetInfo([FromForm] string key)
        {
            return await dbContext.SysConfig.FirstOrDefaultAsync(m => m.Key == key);
        }

        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<bool> Edit([FromForm] string key, [FromForm] string value, [FromForm] string desc)
        {

            if (string.IsNullOrEmpty(key))
            {
                var item = new SysConfig()
                {
                    Key = key,
                    Value = value,
                    Desc = desc,
                };
                await dbContext.SysConfig.AddAsync(item);
                return await dbContext.SaveChangesAsync() > 0;
            }
            else
            {
                var model = await dbContext.SysConfig.FirstOrDefaultAsync(m => m.Key == key);
                if (model == null)
                {
                    throw new ApiException(HttpResultCode.数据不存在);
                }
                model.Value = value;
                model.Desc = desc;
                return await dbContext.SaveChangesAsync() > 0;

            }
        }
    }
}