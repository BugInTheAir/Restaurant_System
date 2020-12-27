using Grpc.Net.Client;
using MediatR;
using Restaurant.Domain.Aggregates.Common;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Command
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IResTypeRepository _resTypeRepository;

        public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository, IResTypeRepository resTypeRepository)
        {
            _restaurantRepository = restaurantRepository;
            _resTypeRepository = resTypeRepository;
        }

        public async Task<bool> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var existedRestaurant = await _restaurantRepository.FindByNameAsync(request.ResName);
            if (existedRestaurant != null)
                throw new Exception("This restaurant with this name has been existed");
            List<ResImage> resImagesUrl = new List<ResImage>();
            
            List<string> typeId = new List<string>();
            for(int i = 0; i < request.TypeNames.Count; i++)
            {
                var resType =await _resTypeRepository.FindByNameAsync(request.TypeNames[i]);
                if(resType == null)
                {
                    var newType = new RestaurantType(request.TypeNames[i]);
                    _resTypeRepository.Add(newType);
                    typeId.Add(newType.TenantId);
                }
                else
                {
                    typeId.Add(resType.TenantId);
                }
            }
            for(int i = 0; i < request.Images.Count; i++)
            {
                var imageName = $"RI-{DateTime.Now.ToShortDateString().Replace("/", "-")}-{Guid.NewGuid().ToString().Split('-')[0]}";
                var url = await UploadResImage(imageName, request.Images[i].FileExt, request.Images[i].RawImage);
                resImagesUrl.Add(new ResImage(url, request.Images[i].FileExt));
            }
           
            var newRestaurant = new Restaurants(request.ResName, request.Street, request.District, request.Ward, request.OpenTime, request.CloseTime,request.Phone, request.Seats, typeId, request.Menus, resImagesUrl);
            _restaurantRepository.Add(newRestaurant);
            return await _restaurantRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
        private async Task<string> UploadResImage(string imgName, string imgExt, byte[] rawImg)
        {
            var httpHandler = new HttpClientHandler();
            // Return `true` to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress("https://localhost:5008", new GrpcChannelOptions { HttpHandler = httpHandler });
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
