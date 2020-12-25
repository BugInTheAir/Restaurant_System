using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCenter.Api.Applications.Services
{
    public interface IUploadImageService
    {
        Task<bool> UploadImageToServer(byte[] img, string fileName);
    }
}
