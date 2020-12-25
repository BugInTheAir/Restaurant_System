using DataCenter.Api.Applications.Configurations;
using DataCenter.Api.Applications.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataCenter.Api.Applications.Commands
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, string>
    {
        private readonly IUploadImageService _uploadImageService;

        public UploadImageCommandHandler(IUploadImageService uploadImageService)
        {
            _uploadImageService = uploadImageService;
        }

        public async Task<string> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            var result = await _uploadImageService.UploadImageToServer(request.File, request.ImageName + "." + request.FileExt);
            if (result)
                return Config.IMG_URL + request.ImageName + "." + request.FileExt;
            throw new Exception("Error when upload image to server");
        }
    }
}
