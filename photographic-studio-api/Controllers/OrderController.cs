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
using photographic_studio_api.Utils;

namespace photographic_studio_api.Controllers
{
    public class OrderController : BaseController
    {
        photographicDbcontext dbContext;
        public OrderController(photographicDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }
        [UserAuth]
        [HttpPost]
        public async Task<bool> CreateOrder(
            [FromHeader]int uid,
            [FromForm]int pricingPackageId,
            [FromForm]string phone,
            [FromForm]string address,
            [FromForm]DateTime time,
            [FromForm]string province,
            [FromForm]string city,
            [FromForm]string area )
        {
            PricingPackage pricingPackage = await dbContext.PricingPackage.FirstOrDefaultAsync(m => m.Id == pricingPackageId);
            if (pricingPackage==null)
            {
                throw new ApiException(HttpResultCode.数据不存在);
            }
            var deposit = await dbContext.SysConfig.FirstOrDefaultAsync(m => m.Key == ReadonlyUtils.SysConfig_deposit);
            var depositValue = deposit==null?0M:decimal.Parse(deposit.Value);
            var user = await dbContext.UserInfo.FirstOrDefaultAsync(m => m.Id == uid);
            if (user.Point < depositValue)
            {
                throw new ApiException(HttpResultCode.余额不足);
            }
            user.Point -= depositValue;
            OrderList orderList = new OrderList() { 
                PricingPackageId = pricingPackageId,
                OrderNumber = StringUtil.GetOrderNumber(),
                Price = pricingPackage.Price,
                Deposit = depositValue,
                UserId = uid,
                Province = province,
                City = city,
                Area = area,
                Address = address,
                Time = time,
                Status = (int)OrderStatus.预约,
                IsTel = 0,
                CreateTime = DateTime.Now,
                Phone = phone
            };
            dbContext.OrderList.Add(orderList);
            await dbContext.SaveChangesAsync();
            return true;
        }
        [UserAuth]
        [HttpPost]
        public async Task<dynamic> MyList([FromHeader]int uid,[FromForm]int index, [FromForm]int pageSize) {
            return await dbContext.OrderList.Where(m => m.UserId == uid).Skip((index - 1) * pageSize).Take(pageSize).Join(dbContext.PricingPackage, o => o.PricingPackageId, p => p.Id, (o, p) => new {
                o.Id,
                o.OrderNumber,
                o.Phone,
                o.Price,
                o.Address,
                o.Area,
                o.Cameraman,
                o.City,
                o.CreateTime,
                o.Deposit,
                o.PricingPackageId,
                o.Province,
                o.Status,
                o.Time,
                p.Cover,
                p.Desc,
                p.Name
            }).ToListAsync();
        }
        [UserAuth]
        [HttpPost]
        public Task<int> MyCount([FromHeader]int uid, [FromForm]int index, [FromForm]int pageSize)
        {
            return dbContext.OrderList.Where(m => m.UserId == uid).CountAsync();
        }
        [UserAuth]
        [HttpPost]
        public async Task<bool> Pay([FromHeader]int uid, [FromForm]string orderNumber)
        {
            var order = await dbContext.OrderList.FirstOrDefaultAsync(m => m.OrderNumber==orderNumber);
            if (order == null)
            {
                throw new ApiException(HttpResultCode.数据不存在);
            }
            var user = await dbContext.UserInfo.FirstOrDefaultAsync(m => m.Id == uid);
            if (user.Point < order.Price)
            {
                throw new ApiException(HttpResultCode.余额不足);
            }
            user.Point -= order.Price;
            order.Status = (int)OrderStatus.付款;
            await dbContext.SaveChangesAsync();
            return true;
        }
        [UserAuth]
        [HttpPost]
        public async Task<bool> setCameraman([FromHeader]int uid, [FromForm]string orderNumber,[FromForm]int cameramanId)
        {
            var order = await dbContext.OrderList.FirstOrDefaultAsync(m =>m.UserId==uid && m.OrderNumber == orderNumber);

            if (order == null||await dbContext.UserInfo.CountAsync(m=>m.Id==cameramanId&&m.Role==(int)UserRole.摄影师)==0)
            {
                throw new ApiException(HttpResultCode.数据不存在);
            }
            order.Cameraman = cameramanId;
            await dbContext.SaveChangesAsync();
            return true;
        }

        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public Task<List<OrderList>> GetList([FromForm]int index, [FromForm]int pageSize)
        {
            var q = dbContext.OrderList.AsQueryable();
            return q.OrderByDescending(m => m.CreateTime).Skip((index - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<bool> SetIsTel([FromForm]string orderNumber, [FromForm]int isTel)
        {
            var order = await dbContext.OrderList.FirstOrDefaultAsync(m => m.OrderNumber == orderNumber);

            if (order == null)
            {
                throw new ApiException(HttpResultCode.数据不存在);
            }
            order.IsTel = isTel;
            await dbContext.SaveChangesAsync();
            return true;
        }
        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public Task<int> GetCount()
        {
            var q = dbContext.OrderList.AsQueryable();
            return q.CountAsync();
        }

        [UserAuth]
        [HttpPost]
        public async Task<dynamic> GetOrderDetail([FromHeader]int uid,[FromForm]string orderNumber)
        {
            var order = await dbContext.OrderList.FirstOrDefaultAsync(m =>m.UserId==uid&& m.OrderNumber == orderNumber);
            var cuid = order.Cameraman;
            var cameraman = order.Cameraman == null ? null : await dbContext.UserInfo.FirstOrDefaultAsync(m=>m.Id== cuid);
            var images = await dbContext.UserImage.Where(m => m.OrderNumber == orderNumber).ToListAsync();
            var comment = await dbContext.Comment.Where(m => m.OrderNumber == orderNumber).FirstOrDefaultAsync();
            return new { order, cameraman, images, comment };
        }

        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public Task<OrderList> GetByOrderName([FromForm]string orderNumber)
        {
            return dbContext.OrderList.FirstOrDefaultAsync(m => m.OrderNumber == orderNumber);
        }
        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public Task<List<string>> GetImageList([FromForm]string orderNumber) {
            return dbContext.UserImage.Where(m => m.OrderNumber == orderNumber).Select(m => m.Path).ToListAsync();
        }
        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<bool> SaveImageList([FromForm]string orderNumber, [FromForm]string imageList) {
            var order = await dbContext.OrderList.FirstOrDefaultAsync(m => m.OrderNumber == orderNumber);

            if (order == null)
            {
                throw new ApiException(HttpResultCode.数据不存在);
            }
            var list = await dbContext.UserImage.Where(m => m.OrderNumber == orderNumber).ToListAsync();
            dbContext.RemoveRange(list);
            string[] str = imageList.Split(',');
            var add = new List<UserImage>(str.Length);
            DateTime now = DateTime.Now;
            foreach (var item in str)
            {
                add.Add(new UserImage() {
                    UserId = order.UserId,
                    IsPublish = 0,
                    Path = item,
                    OrderNumber = orderNumber,
                    CreateTime = now,
                });
            }
            dbContext.UserImage.AddRange(add);
            await dbContext.SaveChangesAsync();
            return true;
        }
        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<bool> IsPublish([FromForm]string orderNumber, [FromForm]int isPublish)
        {
            var order = await dbContext.OrderList.FirstOrDefaultAsync(m => m.OrderNumber == orderNumber);

            if (order == null)
            {
                throw new ApiException(HttpResultCode.数据不存在);
            }
            order.IsPublish = isPublish;
            await dbContext.SaveChangesAsync();
            return true;
        }
        [UserAuth]
        [HttpPost]
        public async Task<bool> Comment([FromHeader]int uid, [FromForm]string orderNumber, [FromForm]string content)
        {
            var order = await dbContext.OrderList.FirstOrDefaultAsync(m => m.OrderNumber == orderNumber);

            if (await dbContext.Comment.CountAsync(m => m.OrderNumber == orderNumber) > 0)
            {
                throw new ApiException(HttpResultCode.参数不正确);
            }
            if (await dbContext.OrderList.CountAsync(m => m.UserId == uid && m.OrderNumber == orderNumber) == 0)
            {
                throw new ApiException(HttpResultCode.数据不存在);
            }
            dbContext.Comment.Add(new Comment()
            {
                OrderNumber = orderNumber,
                Content = content,
                Uid = uid,
                CreateDate = DateTime.Now
            });
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}