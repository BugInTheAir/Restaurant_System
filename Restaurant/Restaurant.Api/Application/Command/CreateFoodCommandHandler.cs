using Grpc.Net.Client;
using MediatR;
using Restaurant.Domain.Aggregates.FoodAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Command
{
    public class CreateFoodCommandHandler : IRequestHandler<CreateFoodCommand, bool>
    {
        private IFoodRepository _foodRepository;

        public CreateFoodCommandHandler(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<bool> Handle(CreateFoodCommand request, CancellationToken cancellationToken)
        {
            var existedFood = await _foodRepository.FindByNameAsync(request.FoodName);
            if (existedFood != null)
                throw new Exception("Food has been created before");
            else
            {
                var imageName = $"FI-{DateTime.Now.ToShortDateString().Replace("/", "-")}-{Guid.NewGuid().ToString().Split('-')[0]}";
                var imageUrl = await UploadFoodImage(imageName, request.ImgExt, request.RawImage);
                try
                {
                    _foodRepository.Add(new FoodItem(imageUrl, request.FoodName, request.Description));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return await _foodRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
        private async Task<string> UploadFoodImage(string imgName, string imgExt, byte[] rawImg)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5008");
            var client = new UploadService.UploadServiceClient(channel);
            try
            {
                var reply = await client.UploadImageAsync(new UploadImageRequest { FileExt = imgExt, FileName = imgName, ImageRaw = Google.Protobuf.ByteString.CopyFrom(rawImg) });
                return reply.ReturnUrl;
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
