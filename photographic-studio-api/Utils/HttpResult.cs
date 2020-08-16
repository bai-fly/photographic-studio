using photographic_studio_api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Utils
{

    public class HttpResult<T>
    {
        public int Code { get; set; }
        public T Data { get; set; }

        public string Remark { get; set; }

        public static HttpResult<T> Success(T data, string remark = "success")
        {
            return new HttpResult<T>()
            {
                Code = (int)HttpResultCode.SUCCESS,
                Data = data,
                Remark = remark
            };
        }

        public static HttpResult<T> Error(T data, string remark = "error")
        {
            return new HttpResult<T>()
            {
                Code = (int)HttpResultCode.ERROR,
                Data = data,
                Remark = remark
            };
        }
        public static HttpResult<T> Exception(T data, string remark = "exception")
        {
            return new HttpResult<T>()
            {
                Code = (int)HttpResultCode.EXCEPTION,
                Data = data,
                Remark = remark
            };
        }
        public static HttpResult<T> Result(HttpResultCode code, T data, string remark = "")
        {
            return new HttpResult<T>()
            {
                Code = (int)code,
                Data = data,
                Remark = remark
            };
        }
        public static HttpResult<T> Result(int code, T data, string remark = "")
        {
            return new HttpResult<T>()
            {
                Code = code,
                Data = data,
                Remark = remark
            };
        }

    }
}
