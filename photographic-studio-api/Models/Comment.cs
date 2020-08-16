using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public string OrderNumber { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
