using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utils.Azure
{
    public interface IAzureBlobStorage
    {
        Task<string> UploadImageAsync(IFormFile formFile);
    }
}
