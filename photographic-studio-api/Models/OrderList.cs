using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Models
{
    public class OrderList
    {
        public int Id { get; set; }
        public int PricingPackageId { get; set; }
        public string OrderNumber { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 定金
        /// </summary>
        public decimal Deposit { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 摄影师
        /// </summary>
        public int? Cameraman { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public DateTime? Time { get; set; }
        public int Status { get; set; }
        public string Phone { get; set; }
        public int IsTel { get; set; }
        public int IsPublish { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
