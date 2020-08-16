using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Content { get; set; }
        public int Category { get; set; }
        public uint IsHot { get; set; }
        public int PreviewCount { get; set; }
        public uint LikeCount { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int Order { get; set; }
        public int Type { get; set; }
        public string Link { get; set; }
    }
}
