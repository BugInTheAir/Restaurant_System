using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Command
{
    public class CreateRestaurantCommand : IRequest<bool>
    {
        public string ResName { get; private set; }
        public string TypeId { get; private set; }
        public string Street { get; private set; }
        public string Ward { get; private set; }
        public string District { get; private set; }
        public string OpenTime { get; private set; }
        public string CloseTime { get; private set; }
        public int Seats { get; private set; }
        public List<string> Menus { get; private set; }
        

        public CreateRestaurantCommand(string resName, string typeId, string street, string ward, string district, string openTime, string closeTime, int seats, List<string> menus)
        {
            ResName = resName;
            TypeId = typeId;
            Street = street;
            Ward = ward;
            District = district;
            OpenTime = openTime;
            CloseTime = closeTime;
            Menus = menus;
            Seats = seats;
        }
    }
}
