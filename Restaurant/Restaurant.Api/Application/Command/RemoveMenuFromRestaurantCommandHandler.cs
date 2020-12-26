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
    public class RemoveMenuFromRestaurantCommandHandler : IRequestHandler<RemoveMenuFromRestaurantCommand, bool>
    {
        private readonly IMenuRepository _menuRepo;
        private readonly IRestaurantRepository _resRepo;

        public RemoveMenuFromRestaurantCommandHandler(IMenuRepository menuRepo, IRestaurantRepository resRepo)
        {
            _menuRepo = menuRepo;
            _resRepo = resRepo;
        }

        public async Task<bool> Handle(RemoveMenuFromRestaurantCommand request, CancellationToken cancellationToken)
        {
            var existedMenu = await _menuRepo.FindByIdAsync(request.MenuId);
            if (existedMenu == null)
                throw new KeyNotFoundException();
            var existedRes = await _resRepo.FindByIdAsync(request.ResId);
            if (existedRes == null)
                throw new KeyNotFoundException();
            existedRes.RemoveMenuFromRestaurant(request.MenuId);
            _resRepo.Update(existedRes);
            await _resRepo.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return true;
        }
    }
}
