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
    public class PricingPackageController : BaseController
    {
        photographicDbcontext dbContext;
        public PricingPackageController(photographicDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }

        //[UserAuth(UserRole.管理员)]
        [HttpPost]
        public Task<List<PricingPackage>> GetList([FromForm]int index, [FromForm]int pageSize)
        {
            var q = dbContext.PricingPackage.AsQueryable();
            return q.OrderByDescending(m=>m.Order).Skip((index - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        //[UserAuth(UserRole.管理员)]
        [HttpPost]
        public Task<int> GetCount()
        {
            var q = dbContext.PricingPackage.AsQueryable();
            return q.CountAsync();
        }

        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<bool> Edit([FromForm] int? id,
            [FromForm] string name,
            [FromForm] string cover,
            [FromForm] string desc,
            [FromForm] int price,
            [FromForm] decimal deposit,
            [FromForm] int order
            )
        {

            if (id == null)
            {
                var now = DateTime.Now;
                await dbContext.PricingPackage.AddAsync(new PricingPackage()
                {
                    Name = name,
                    Cover = cover,

                    Desc = desc,
                    Price = price,
                    Deposit = deposit,
                    Order = order,
                    CreateTime = now,
                    UpdateTime = now,
                });

            }
            else
            {
                var model = await dbContext.PricingPackage.FirstOrDefaultAsync(m => m.Id == id);
                if (model == null)
                {
                    throw new ApiException(HttpResultCode.数据不存在);
                }
                model.Name = name;
                model.Cover = cover;
                model.Desc = desc;
                model.Price = price;
                model.Deposit = deposit;
                model.Order = order;
                model.UpdateTime = DateTime.Now;

            }
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}