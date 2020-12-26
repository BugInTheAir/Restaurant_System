using MediatR;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Command
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public CreateRestaurantCommandHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<bool> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            List<ResImage> resImagesUrl = null;
            var existedRestaurant =await _restaurantRepository.FindByNameAsync(request.ResName);
            if (existedRestaurant != null)
                throw new Exception("This restaurant with this name has been existed");
            var newRestaurant = new Restaurants(request.ResName, request.Street, request.District, request.Ward, request.OpenTime, request.CloseTime,request.Phone, request.Seats, request.Menus, resImagesUrl);
            _restaurantRepository.Add(newRestaurant);
            return await _restaurantRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
