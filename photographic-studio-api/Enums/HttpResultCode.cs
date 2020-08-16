using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Enums
{
    public enum HttpResultCode
    {
        ERROR = 0,
        SUCCESS = 1,
        EXCEPTION = -1,
        账号已存在 = -100,
        参数不正确 = -101,
        用户名或密码错误 = -102,
        数据不存在 = -103,
        权限错误 = -104,
        文件不能为空 = -105,
        文件类型不正确 = -106,
        分类商品不为0 = -107,
        用户不存在 = -108,
        余额不足 = -109,
        NOAUTH = -10000,
    }
}
