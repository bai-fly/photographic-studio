using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Utils
{
    public class NumberUtil
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min">可取</param>
        /// <param name="max">不可取</param>
        /// <returns></returns>
        public static int GetRandomInt(int min, int max)
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(min, max);
        }
        /*
        /// <summary>
        /// 数组顺序打乱
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        //public static string GetRandNumber(int[] arr)
        //{
        //    return string.Join(",", arr.OrderBy(x => Guid.NewGuid()).ToArray());
        //}

            */

        /// <summary>
        /// 将传入的数组顺序打乱
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static int[] GetRandNumber(int[] arr)
        {
            return arr.OrderBy(x => Guid.NewGuid()).ToArray();
        }
        /// <summary>
        /// 返回随机数，包含最小和最大值
        /// </summary>
        /// <param name="speed">种子</param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        /// <param name="Len">小数点保留位数</param>
        /// <returns></returns>
        public static decimal GetRandomDecimal(int speed, decimal minimum, decimal maximum, int Len)
        {
            Random random = new Random(speed);
            return Math.Round(Convert.ToDecimal(random.NextDouble()) * (maximum - minimum) + minimum, Len);
        }

    }
}

