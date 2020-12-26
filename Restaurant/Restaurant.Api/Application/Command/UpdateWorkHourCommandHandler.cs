using MediatR;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Command
{
    public class UpdateWorkHourCommandHandler : IRequestHandler<UpdateWorkHourCommand, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public UpdateWorkHourCommandHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(_restaurantRepository));
        }

        public async Task<bool> Handle(UpdateWorkHourCommand request, CancellationToken cancellationToken)
        {
            var existedRes = await _restaurantRepository.FindByIdAsync(request.ResId);
            if (existedRes == null)
                throw new KeyNotFoundException();
            existedRes.UpdateWorkTime(request.OpenTime, request.CloseTime);
            _restaurantRepository.Update(existedRes);
            await _restaurantRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
