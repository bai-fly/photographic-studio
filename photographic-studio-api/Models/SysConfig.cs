﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Models
{
    public class SysConfig
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Desc { get; set; }
    }
}
