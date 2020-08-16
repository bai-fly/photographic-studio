using Microsoft.EntityFrameworkCore;
using photographic_studio_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Context
{
    public class photographicDbcontext:DbContext
    {
        public photographicDbcontext(DbContextOptions<photographicDbcontext> options) : base(options)
        {

        }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<WapInfo> WapInfo { get; set; }
        public virtual DbSet<SysConfig> SysConfig { get; set; }
        /// <summary>
        /// 套餐
        /// </summary>
        public virtual DbSet<PricingPackage> PricingPackage { get; set; }
        public virtual DbSet<OrderList> OrderList { get; set; }
        public virtual DbSet<UserImage> UserImage { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }

    }
}
