using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Utils
{
    public static class CommonUtil
    {
        public static long GetTimeStamp(DateTime dateTime)
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }


        public static string GetMd5(string str)
        {
            return EncryptUtils.Md532(str);
        }
        public static string CreateToken(int uid, DateTime dateTime, int role)
        {
            string str = $"{uid}-{role}-{dateTime.ToString("yyyy-MM-dd HH:mm:ss")}";
            return $"{role}{GetMd5(str)}{uid}";
        }
        public static (int, int) GetTokenInfo(string token)
        {
            int role = int.Parse(token[0].ToString());
            int uid = int.Parse(token.Substring(33));
            return (uid, role);
        }
        public static string GetClientUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }
    }
}
