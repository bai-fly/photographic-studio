using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using photographic_studio_api.Context;
using photographic_studio_api.Enums;
using photographic_studio_api.Exceptions;
using photographic_studio_api.Filters.Auth;
using photographic_studio_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Controllers
{
    public class WapInfoController : BaseController
    {
        photographicDbcontext dbContext;
        public WapInfoController(photographicDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }
        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<List<WapInfo>> List([FromForm] int index, [FromForm] int pageSize)
        {
            return await dbContext.WapInfo.OrderByDescending(m => m.Id).Skip((index - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        [HttpPost]
        public async Task<string> GetWapInfo([FromForm] string key)
        {
            var t = await dbContext.WapInfo.FirstOrDefaultAsync(m => m.Key == key);
            return t?.Value;
        }

        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<bool> Edit([FromForm] string key, [FromForm] string value, [FromForm] string desc)
        {

            if (string.IsNullOrEmpty(key))
            {
                var item = new WapInfo()
                {
                    Key = key,
                    Value = value,
                    Desc = desc,
                };
                await dbContext.WapInfo.AddAsync(item);
                return await dbContext.SaveChangesAsync() > 0;
            }
            else
            {
                var model = await dbContext.WapInfo.FirstOrDefaultAsync(m => m.Key == key);
                if (model == null)
                {
                    throw new ApiException(HttpResultCode.数据不存在);
                }
                model.Value = value;
                if (!string.IsNullOrEmpty(desc))
                    model.Desc = desc;
                return await dbContext.SaveChangesAsync() > 0;

            }
        }
    }
}
