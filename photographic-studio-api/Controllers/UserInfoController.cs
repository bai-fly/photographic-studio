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
    public class UserInfoController : BaseController
    {
        photographicDbcontext dbContext;
        public UserInfoController(photographicDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }
        [UserAuth]
        [HttpPost]
        public async Task<UserInfo> Get([FromHeader] int uid)
        {
            var user = await dbContext.UserInfo.FirstOrDefaultAsync(m => m.Id == uid);
            user.Password = "";
            return user;
        }


        [UserAuth]
        [HttpPost]
        public async Task<dynamic> GetInfo([FromHeader] int uid, [FromForm] int userId)
        {
            var Focus = 0;
            var Fans = 0;
            var user = await dbContext.UserInfo.Where(m => m.Id == userId).Select(m => new
            {
                Id = m.Id,
                Avatar = m.Avatar,
                Account = m.Account,
                Sex = m.Sex,
                Dynamic = 0,
                Focus = Focus,
                Fans = Fans,
                Like = 0,
            }).FirstOrDefaultAsync();

            return user;
        }

        [UserAuth]
        [HttpPost]
        public async Task<UserInfo> Update([FromHeader] int uid, [FromForm] string avatar, [FromForm] int sex, [FromForm] DateTime birthday)
        {
            var user = await dbContext.UserInfo.FirstOrDefaultAsync(m => m.Id == uid);
            user.Avatar = avatar;
            user.Sex = sex;
            await dbContext.SaveChangesAsync();
            return user;
        }



        [HttpPost]
        public async Task<bool> Regist([FromForm] string account, [FromForm] string password)
        {

            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                throw new ApiException(HttpResultCode.参数不正确);
            }
            if (await dbContext.UserInfo.CountAsync(m => m.Account == account) > 0)
            {
                throw new ApiException(HttpResultCode.账号已存在);
            }
            UserInfo userInfo = new UserInfo()
            {
                Account = account,
                Password = CommonUtil.GetMd5(password),
                Point = 0,
                Score = 0,
                Role = (int)UserRole.普通用户,
                Status = (int)CommonStatus.启用,
                CreateDate = DateTime.Now,
                LoginDate = DateTime.Now,
            };
            await dbContext.UserInfo.AddAsync(userInfo);
            return await dbContext.SaveChangesAsync() > 0;

        }
        [HttpPost]
        public async Task<dynamic> Login([FromForm] string account, [FromForm] string password)
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                throw new ApiException(HttpResultCode.参数不正确);
            }
            string pwd = CommonUtil.GetMd5(password);
            UserInfo userInfo = await dbContext.UserInfo.FirstOrDefaultAsync(m => m.Account == account && m.Password == pwd);
            if (userInfo == null)
            {
                throw new ApiException(HttpResultCode.用户名或密码错误);
            }
            userInfo.LoginDate = DateTime.Now;

            await dbContext.SaveChangesAsync();
            string token = CommonUtil.CreateToken(userInfo.Id, userInfo.LoginDate, userInfo.Role);
            Cache.GetInstance().Set<int>(token, userInfo.Id);
            return (new
            {
                user = userInfo,
                token = token,
            });
        }
       
        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public Task<UserInfo> GetUserInfo([FromForm] int uid)
        {
            return dbContext.UserInfo.FirstOrDefaultAsync(m => m.Id == uid);
        }
        [HttpPost]
        public Task<int> GetRole([FromForm] int uid)
        {
            return dbContext.UserInfo.Where(m => m.Id == uid).Select(m => m.Role).FirstAsync();
        }
        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public Task<List<UserInfo>> GetList([FromForm]int index, [FromForm]int pageSize, [FromForm]int? role)
        {
            var q = dbContext.UserInfo.AsQueryable();
            if (role != null)
            {
                q = q.Where(m => m.Role == role);
            }
            return q.OrderByDescending(m=>m.Id).Skip((index - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public Task<int> GetCount([FromForm] int? role)
        {
            var q = dbContext.UserInfo.AsQueryable();
            if (role != null)
            {
                q = q.Where(m => m.Role == role);
            }
            return q.CountAsync();
        }

        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<bool> Edit([FromForm] int? id,
            [FromForm] string account,
            [FromForm] string name,
            [FromForm] string password,
            [FromForm] string avatar,
            [FromForm] int sex,
            [FromForm] UserRole role,
            [FromForm] string desc
            )
        {
            if (id == null)
            {
                string pwd = CommonUtil.GetMd5(password);
                var now = DateTime.Now;
                await dbContext.UserInfo.AddAsync(new UserInfo()
                {
                    Account = account,
                    Name = name,
                    Password = pwd,
                    Avatar = avatar,
                    Sex = sex,
                    Point = 0,
                    Score = 0,
                    Role = (int)role,
                    Status = (int)CommonStatus.启用,
                    CreateDate = now,
                    LoginDate = now,
                    Desc = desc,
                });
                
            }
            else
            {
                var model = await dbContext.UserInfo.FirstOrDefaultAsync(m => m.Id == id);
                if (model == null)
                {
                    throw new ApiException(HttpResultCode.数据不存在);
                }
                model.Account = account;
                model.Name = name;
                if(!string.IsNullOrEmpty(password))
                    model.Password = password;
                model.Sex = sex;
                model.Role = (int)role;
                model.Desc = desc;
                model.Avatar = avatar;

            }
            return await dbContext.SaveChangesAsync() > 0;
        }

        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<bool> Point([FromForm]int id, [FromForm]decimal point)
        {
            var model = await dbContext.UserInfo.FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                throw new ApiException(HttpResultCode.数据不存在);
            }
            model.Point += point;
            return await dbContext.SaveChangesAsync() > 0;
        }
        [UserAuth(UserRole.管理员)]
        [HttpPost]
        public async Task<UserInfo> GetById([FromForm] int userId)
        {
            var user = await dbContext.UserInfo.FirstOrDefaultAsync(m => m.Id == userId);
            user.Password = "";
            return user;
        }
        /// <summary>
        /// 获取摄影师
        /// </summary>
        /// <returns></returns>
        [UserAuth]
        [HttpPost]
        public Task<List<UserInfo>> GetCameraman() {
            return dbContext.UserInfo.Where(m => m.Role == (int)UserRole.摄影师).ToListAsync();
        }
        [HttpPost]
        public async Task<dynamic> GetCameramanList([FromForm]int index, [FromForm]int pageSize)
        {
            return await dbContext.UserInfo.Where(m => m.Role == (int)UserRole.摄影师).Skip((index-1)*pageSize).Take(pageSize).Select(m=>new { 
                m.Id,m.Account,m.Name,m.Desc,m.Sex,m.Avatar
            }).ToListAsync();
        }
        [HttpPost]
        public Task<int> GetCameramanCount()
        {
            return dbContext.UserInfo.Where(m => m.Role == (int)UserRole.摄影师).CountAsync();
        }

    }
}