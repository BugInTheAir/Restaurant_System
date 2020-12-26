using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Command
{
    public class AssignNewMenuToRestaurantCommand : IRequest<bool>
    {
        public string MenuId { get; private set; }
        public string ResId { get; private set; }

        public AssignNewMenuToRestaurantCommand(string menuId, string resId)
        {
            MenuId = menuId;
            ResId = resId;
        }
    }
}
