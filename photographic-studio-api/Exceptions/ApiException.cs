using photographic_studio_api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base(HttpResultCode.EXCEPTION.ToString())
        {
            this.HResult = (int)HttpResultCode.EXCEPTION;
        }
        public ApiException(HttpResultCode err) : base(err.ToString())
        {
            this.HResult = (int)err;
        }
    }
}
