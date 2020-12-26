using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.ViewModels
{
    public class RestaurantInformationViewModel
    {
        public string ResId { get; set; }
        public string RestaurantName { get; set; }
        public AddressViewModel Address { get; set; }
        public string Phone { get; set; }
        public string TypeName { get; set; }
        public int Seats { get; set; }
        public List<string> ImageUrls { get; set; }
        public WorkTimeViewModel WorkTime { get; set; }
        public List<MenuViewModel> Menus { get; set; }
    }
    public class WorkTimeViewModel
    {
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
    }
    public class AddressViewModel
    {
        public string Ward { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Country { get { return "Viet Nam"; } }
    }
    public class MenuViewModel
    {
        public string MenuName { get; set; }
        public string Description { get; set; }
        public List<FoodItemViewModel> FoodItems { get; set; }
    }
    public class FoodItemViewModel
    {
        public string FoodName { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
}
