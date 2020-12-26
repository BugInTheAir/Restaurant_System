using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Command
{
    public class ChangeRestaurantAddressCommand : IRequest<bool>
    {
        public string ResId { get; private set; }
        public string Street { get; private set; }
        public string Ward { get; private set; }
        public string District { get; private set; }

        public ChangeRestaurantAddressCommand(string street, string ward, string district,string resId)
        {
            ResId = resId;
            Street = street;
            Ward = ward;
            District = district;
        }
    }
}
