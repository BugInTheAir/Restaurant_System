using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCenter.Api.Applications.Commands;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DataCenter.Api
{
    public class FileUploadService : UploadService.UploadServiceBase
    {
        private readonly ILogger<FileUploadService> _logger;
        private readonly IMediator _mediator;
        public FileUploadService(ILogger<FileUploadService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public override async Task<UploadImageReply> UploadImage(UploadImageRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _mediator.Send(new UploadImageCommand(request.ImageRaw.ToByteArray(), request.FileName, request.FileExt));
                return new UploadImageReply { ReturnUrl = result};
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
