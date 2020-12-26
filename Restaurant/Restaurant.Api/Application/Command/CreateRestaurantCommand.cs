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
        public List<string> TypeNames { get; private set; }
        public string Street { get; private set; }
        public string Ward { get; private set; }
        public string District { get; private set; }
        public string OpenTime { get; private set; }
        public string CloseTime { get; private set; }
        public int Seats { get; private set; }
        public List<string> Menus { get; private set; }
        public List<RestaurantImage> Images { get; private set; }
        public string Phone { get; private set; }
        public CreateRestaurantCommand(string resName, string phone ,List<string> typeNames, string street, string ward, string district, string openTime, string closeTime, int seats, List<string> menus, List<RestaurantImage> images)
        {
            ResName = resName;
            TypeNames = typeNames;
            Street = street;
            Ward = ward;
            District = district;
            OpenTime = openTime;
            CloseTime = closeTime;
            Menus = menus;
            Images = images;
            Phone = phone;
            Seats = seats;
        }
    }
    public class RestaurantImage
    {
        public byte[] RawImage { get; set; }
        public string FileExt { get; set; }
    }
}
