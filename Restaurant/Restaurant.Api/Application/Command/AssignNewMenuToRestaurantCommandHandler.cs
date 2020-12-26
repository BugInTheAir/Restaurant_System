using MediatR;
using Restaurant.Domain.Aggregates.MenuAggregate;
using Restaurant.Domain.Aggregates.RestaurantAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Command
{
    public class AssignNewMenuToRestaurantCommandHandler : IRequestHandler<AssignNewMenuToRestaurantCommand,bool>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public AssignNewMenuToRestaurantCommandHandler(IMenuRepository menuRepository, IRestaurantRepository restaurantRepository)
        {
            _menuRepository = menuRepository ?? throw new ArgumentNullException(nameof(_menuRepository));
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(_restaurantRepository));
        }

        public async Task<bool> Handle(AssignNewMenuToRestaurantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existedRestaurant = await _restaurantRepository.FindByIdAsync(request.ResId);
                if (existedRestaurant == null)
                    throw new KeyNotFoundException();
                var existedMenu = await _menuRepository.FindByIdAsync(request.MenuId);
                if (existedMenu == null)
                    throw new KeyNotFoundException();
                existedRestaurant.AssignMenuToRestaurant(request.MenuId);
                _restaurantRepository.Update(existedRestaurant);
                await _restaurantRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
