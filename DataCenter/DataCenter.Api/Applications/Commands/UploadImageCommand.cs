using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCenter.Api.Applications.Commands
{
    public class UploadImageCommand : IRequest<string>
    {
        public byte[] File { get; private set; }
        public string ImageName { get; private set; }
        public string FileExt { get; private set; }

        public UploadImageCommand(byte[] file, string imageName, string fileExt)
        {
            File = file;
            ImageName = imageName;
            FileExt = fileExt;
        }
    }
}
