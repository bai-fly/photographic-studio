using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Models
{
    public class UserImage
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Path { get; set; }
        public int IsPublish { get; set; }
        public string OrderNumber { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
