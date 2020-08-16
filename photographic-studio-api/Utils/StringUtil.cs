using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace photographic_studio_api.Utils
{
    public class StringUtil
    {
        public static string GetOrderNumber() {
            return $"No{RandNumber()}";
        }
        /// <summary>
        /// 获取随机由数字组成的字符串
        /// </summary>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string GetNumberVerify(int maxLength)
        {
            if (maxLength > 10)
            {
                maxLength = 10;
            }
            StringBuilder v = new StringBuilder();
            var arr = NumberUtil.GetRandNumber(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            for (int i = 0; i < maxLength; i++)
            {
                v.Append(arr[i]);
            }
            return v.ToString();
        }
        /// <summary>
        /// 获取由日期组成的随机数字
        /// </summary>
        /// <returns></returns>
        public static string RandNumber()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssfff" + GetNumberVerify(6));
        }
    }
}
