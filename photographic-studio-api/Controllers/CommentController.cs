using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using photographic_studio_api.Context;
using photographic_studio_api.Filters.Auth;
using photographic_studio_api.Models;

namespace photographic_studio_api.Controllers
{
    public class CommentController : BaseController
    {
        photographicDbcontext dbContext;
        public CommentController(photographicDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public Task<List<Comment>> GetList([FromForm] int index, [FromForm] int pageSize)
        {
            var q = dbContext.Comment.AsQueryable();
            return q.OrderByDescending(m => m.Id).Skip((index - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        [HttpPost]
        public Task<int> GetCount()
        {
            var q = dbContext.Comment.AsQueryable();
            return q.CountAsync();
        }
    }
}