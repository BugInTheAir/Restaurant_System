using MediatR;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Command
{
    public class ChangeRestaurantAddressCommandHandler : IRequestHandler<ChangeRestaurantAddressCommand, bool>
    {
        private readonly IRestaurantRepository _restaurantRepo;

        public ChangeRestaurantAddressCommandHandler(IRestaurantRepository restaurantRepo)
        {
            _restaurantRepo = restaurantRepo ?? throw new ArgumentNullException(nameof(_restaurantRepo));
        }

        public async Task<bool> Handle(ChangeRestaurantAddressCommand request, CancellationToken cancellationToken)
        {
            var existedRes =await _restaurantRepo.FindByIdAsync(request.ResId);
            if (existedRes == null)
                throw new KeyNotFoundException();
            existedRes.UpdateAddress(request.Street, request.District, request.Ward);
            _restaurantRepo.Update(existedRes);
            await _restaurantRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
