using MediatR;
using Restaurant.Domain.Aggregates.MenuAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Command
{
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, bool>
    {
        private readonly IMenuRepository _menuRepository;

        public CreateMenuCommandHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<bool> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var existedMenu =await _menuRepository.FindByNameAsync(request.Name);
            if (existedMenu != null)
                throw new Exception("Menu with this name has been existed");
            var newMenu = new Menu(request.Name, request.Description, request.FoodItems);
            _menuRepository.Add(newMenu);
            return await _menuRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
