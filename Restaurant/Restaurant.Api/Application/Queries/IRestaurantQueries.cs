using Restaurant.Api.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Queries
{
    public interface IRestaurantQueries
    {
        Task<List<RestaurantInformationViewModel>> GetRestaurant(string typeId, string street);
        Task<List<FoodItemViewModel>> GetFoodItems();
        Task<List<MenuViewModel>> GetMenus();
        Task<RestaurantInfoViewModel> GetRestaurantName(string resId);
    }
}
