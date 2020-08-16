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
    public class NewsController : BaseController
    {
        photographicDbcontext dbContext;
        public NewsController(photographicDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public async Task<News> Get([FromHeader] int? uid, [FromForm] int id, string ip)
        {
            var news = await dbContext.News.FirstOrDefaultAsync(m => m.Id == id);
            return news;
        }
        [HttpPost]
        public async Task<dynamic> List([FromHeader] int? uid, [FromForm] int index, [FromForm] int pageSize, [FromForm] int? category, [FromForm] string key)
        {
            var q = dbContext.News.Where(m => m.Status == (int)CommonStatus.启用);
            if (category != null)
            {
                q = q.Where(q => q.Category == category.Value);
            }
            if (!string.IsNullOrEmpty(key))
            {
                q = q.Where(m => m.Title.IndexOf(key) != -1);
            }
            return await q.OrderByDescending(m => m.CreateDate).Skip((index - 1) * pageSize).Take(pageSize).Select(m => new {
                Id = m.Id,
                Title = m.Title,
                Cover = m.Cover,
                CreateDate = m.CreateDate,
                PreviewCount = m.PreviewCount,
                LikeCount = m.LikeCount,
                Type = m.Type,
                Link = m.Link,
                IsLike = 0,
                ReplyCount = 0
            }).OrderByDescending(m => m.CreateDate).ToListAsync();
        }
        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public Task<List<News>> GetList([FromForm] int index, [FromForm] int pageSize, [FromForm] int? category)
        {
            var q = dbContext.News.AsQueryable();
            if (category != null)
            {
                q = q.Where(m => m.Category == category.Value);
            }
            return q.OrderByDescending(m => m.Id).Skip((index - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        //[UserAuth(UserRole.管理员)]
        [HttpPost]
        public Task<int> GetCount([FromForm] int? category)
        {
            var q = dbContext.News.AsQueryable();
            if (category != null)
            {
                q = q.Where(m => m.Category == category.Value);
            }
            return q.CountAsync();
        }

        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<bool> Edit([FromForm] int? id,
            [FromForm] int category,
            [FromForm] string title,
            [FromForm] string cover,
            [FromForm] uint isHot,
            [FromForm] int order,
            [FromForm] string link,
            [FromForm] string content,
            [FromForm] int type = 1
            )
        {
            if (content == null)
            {
                content = string.Empty;
            }
            if (id == null)
            {
                await dbContext.News.AddAsync(new News()
                {
                    Category = category,
                    Title = title,
                    Cover = cover,
                    Content = content,
                    IsHot = isHot,
                    PreviewCount = 0,
                    LikeCount = 0,
                    Order = order,
                    Status = (int)CommonStatus.启用,
                    CreateDate = DateTime.Now,
                    Link = link,
                    Type = type
                });
            }
            else
            {
                var model = await dbContext.News.FirstOrDefaultAsync(m => m.Id == id);
                if (model == null)
                {
                    throw new ApiException(HttpResultCode.数据不存在);
                }
                model.Category = category;
                model.Title = title;
                model.Cover = cover;
                model.Content = content;
                model.IsHot = isHot;
                model.Type = type;
                model.Link = link;
                model.Order = order;

            }
            return await dbContext.SaveChangesAsync() > 0;
        }
        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<bool> Delete([FromForm] int id)
        {
            var n = new News() { Id = id };
            dbContext.Attach(n);
            dbContext.Remove(n);
            return await dbContext.SaveChangesAsync() > 0;
        }
        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<bool> SetStatus([FromForm] int id, [FromForm] CommonStatus status)
        {
            var item = await dbContext.News.FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                throw new ApiException(HttpResultCode.数据不存在);
            }
            item.Status = (int)status;
            return await dbContext.SaveChangesAsync() > 0;
        }
    }
}
