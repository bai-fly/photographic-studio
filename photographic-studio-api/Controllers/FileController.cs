using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using photographic_studio_api.Context;
using photographic_studio_api.Enums;
using photographic_studio_api.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace photographic_studio_api.Controllers
{

    public class FileController : BaseController
    {

        static string[] imglast = { "jpg", "jpeg", "gif", "bmp", "png" };
        static string[] videolast = { "avi", "rmvb", "rm", "mpg", "mpeg", "wmv", "mp4", "mkv" };
        photographicDbcontext dbContext;
        public FileController(photographicDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<string> Upload([FromForm] List<IFormFile> file)
        {
            try
            {
                var fileitem = file.FirstOrDefault(m => m.Name == "file");
                if (fileitem == null)
                {
                    throw new ApiException(HttpResultCode.文件不能为空);
                }
                string[] tname = fileitem.FileName.Split('.');
                if (!imglast.Contains(tname[tname.Length - 1].ToLower()))
                {
                    throw new ApiException(HttpResultCode.文件类型不正确);
                }
                string newfilename = $"{Guid.NewGuid()}.{tname[tname.Length - 1]}";
                string serverPath = AppContext.BaseDirectory;
                string dirPath = $"/upload/{DateTime.Now.ToString("yyyyMMdd")}";
                string uploadPath = $"{dirPath}/{newfilename}";
                string fullPath = $"{serverPath}{uploadPath}";
                if (!Directory.Exists($"{serverPath}{dirPath}"))
                {
                    Directory.CreateDirectory($"{serverPath}{dirPath}");
                }
                using (var stream = new FileStream(fullPath, FileMode.Create))

                {
                    await fileitem.CopyToAsync(stream);

                }
                var domain = await dbContext.SysConfig.FirstOrDefaultAsync(m => m.Key == "doman");
                return (domain?.Value + uploadPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
