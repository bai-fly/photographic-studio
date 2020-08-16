using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public int Sex { get; set; }
        public decimal Point { get; set; }
        public decimal Score { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LoginDate { get; set; }
        public string Desc { get; set; }
    }
}
