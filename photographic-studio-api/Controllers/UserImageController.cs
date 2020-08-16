using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using photographic_studio_api.Context;

namespace photographic_studio_api.Controllers
{
    public class UserImageController : BaseController
    {

        photographicDbcontext dbContext;
        public UserImageController(photographicDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<List<string>> GetImages([FromForm]int index,[FromForm]int pageSize)
        {
            return dbContext.UserImage.Join(dbContext.OrderList, i => i.OrderNumber, o => o.OrderNumber, (i, o) => new
            {
                i.Path,
                o.IsPublish,
                i.CreateTime
            }).Where(m => m.IsPublish == 1).Skip((index - 1) * pageSize).Take(pageSize).Select(m=>m.Path).ToListAsync();
        }
    }
}