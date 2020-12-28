using Dapper;
using Microsoft.Data.SqlClient;
using Restaurant.Api.Application.ViewModels;
using Restaurant.Infrastructure.ExternalServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Queries
{
    public class RestaurantQueries : IRestaurantQueries
    {
        private string _connectionString = String.Empty;

        public RestaurantQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<FoodItemViewModel>> GetFoodItems()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"Select FoodId, FoodInfo_ImageUrl, FoodInfo_FoodName, FoodInfo_Description from dbo.FoodItem";
                var result = await connection.QueryAsync<dynamic>(query);
                return ToFoodItemView(result);
            }
        }
        public async Task<List<RestaurantInformationViewModel>> GetRestaurant(string typeName, string address)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"select r.ResId, r.Name, m.MenuId ,r.Phone, m.MenuInfo_Name, m.MenuInfo_Des, r.Address_District, r.Address_Street, r.Address_Ward, r.Seats, r.WorkTime_OpenTime, r.WorkTime_CloseTime, t.TypeName, ri.ImageUrl, f.FoodInfo_FoodName, f.FoodInfo_Description, f.FoodInfo_ImageUrl from Restaurant r
                              inner join ResAndMenu rm on r.ResId = rm.ResId
                              inner join Menu m on rm.MenuId = m.MenuId
                              inner join ResAndType rt on rt.ResId = r.ResId
                              inner join RestaurantType t on t.ResTypeId = rt.ResTypeId
                              inner join ResImages ri on ri.RestaurantsTenantId = r.ResId
                              inner join FoodAndMenu fm on fm.MenuId = m.MenuId
                              inner join FoodItem f on f.FoodId = fm.FoodId";
                var result = await connection.QueryAsync<dynamic>(query);
                var final = ToRestaurantInformationView(result);
                if (address != null)
                {
                    final = final.Where(x =>
                    {
                        string addr = $"{x.Address.Street}, {x.Address.Ward}, {x.Address.District}";
                        if (addr.ToLower().Contains(address.ToLower()))
                            return true;
                        return false;
                    }).ToList();
                }
                if (typeName != null)
                {
                    final = final.Where(x =>
                    {
                        var y = x.TypeName.Where(z =>
                        {
                            if (z.ToLower().Contains(typeName.ToLower()))
                                return true;
                            return false;
                        }).FirstOrDefault();
                        if (y is null)
                            return false;
                        return true;
                    }).ToList();
                }
                return final;
            }
        }
        public List<RestaurantInformationViewModel> ToRestaurantInformationView(dynamic result)
        {
            List<RestaurantInformationViewModel> resResult = new List<RestaurantInformationViewModel>();
            foreach (var item in result)
            {
                var existedRes = resResult.Where(x => x.RestaurantName == item.Name).AsParallel().FirstOrDefault();
                if (existedRes != null)
                {
                    var existedType = existedRes.TypeName.Where(x => x.Equals(item.TypeName)).AsParallel().FirstOrDefault();
                    if (existedType == null)
                        existedRes.TypeName.Add(item.TypeName);
                    var existedImage = existedRes.ImageUrls.Where(x => x.Equals(item.ImageUrl)).AsParallel().FirstOrDefault();
                    if(existedImage == null)
                        existedRes.ImageUrls.Add(item.ImageUrl);
                    var existedMenu = existedRes.Menus.Where(x => x.MenuName == item.MenuInfo_Name).AsParallel().FirstOrDefault();
                    if (existedMenu != null)
                    {
                        var existedFood = existedMenu.FoodItems.Where(x => x.FoodName == item.FoodInfo_FoodName).AsParallel().FirstOrDefault();
                        if(existedFood == null)
                        {
                            resResult.Where(x => x.RestaurantName == item.Name).AsParallel().FirstOrDefault().Menus.Where(x => x.MenuName == item.MenuInfo_Name).AsParallel().FirstOrDefault().FoodItems.Add(new FoodItemViewModel
                            {
                                Description = item.FoodInfo_Description,
                                FoodName = item.FoodInfo_FoodName,
                                ImageUrl = item.FoodInfo_ImageUrl
                            });
                        }
                    }
                    else
                    {
                        var foodItems = new List<FoodItemViewModel>();
                        foodItems.Add(new FoodItemViewModel
                        {
                            FoodName = item.FoodInfo_FoodName,
                            Description = item.FoodInfo_Description,
                            ImageUrl = item.FoodInfo_ImageUrl
                        });
                        resResult.Where(x => x.RestaurantName == item.Name).AsParallel().FirstOrDefault().Menus.Add(new MenuViewModel
                        {
                            MenuName = item.MenuInfo_Name,
                            Description = item.MenuInfo_Des,
                            FoodItems = foodItems,
                            MenuId = item.MenuId
                        });

                    }
                }
                else
                {
                    var types = new List<string>();
                    var menus = new List<MenuViewModel>();
                    var foodItems = new List<FoodItemViewModel>();
                    var images = new List<string>();
                    images.Add(item.ImageUrl);
                    foodItems.Add(new FoodItemViewModel
                    {
                        FoodName = item.FoodInfo_FoodName,
                        Description = item.FoodInfo_Description,
                        ImageUrl = item.FoodInfo_ImageUrl
                    });
                    menus.Add(new MenuViewModel
                    {
                        Description = item.MenuInfo_Des,
                        MenuName = item.MenuInfo_Name,
                        FoodItems = foodItems,
                        MenuId = item.MenuId
                    });
                    types.Add(item.TypeName);
                    var res = new RestaurantInformationViewModel
                    {
                        Address = new AddressViewModel
                        {
                            District = item.Address_District,
                            Street = item.Address_Street,
                            Ward = item.Address_Ward
                        },
                        ResId = item.ResId,
                        RestaurantName = item.Name,
                        Seats = item.Seats,
                        Phone = item.Phone,
                        TypeName = types,
                        WorkTime = new WorkTimeViewModel
                        {
                            CloseTime = item.WorkTime_CloseTime,
                            OpenTime = item.WorkTime_OpenTime
                        },
                        Menus = menus,
                        ImageUrls = images
                    };
                    resResult.Add(res);
                }
            }
            return resResult;
        }
        public List<FoodItemViewModel> ToFoodItemView(dynamic result)
        {
            List<FoodItemViewModel> foodItems = new List<FoodItemViewModel>();
            foreach(var item in result)
            {
                foodItems.Add(new FoodItemViewModel()
                {
                    FoodId = item.FoodId,
                    ImageUrl = item.FoodInfo_ImageUrl,
                    FoodName = item.FoodInfo_FoodName,
                    Description = item.FoodInfo_Description
                });
            }
            return foodItems;
        }
        public async Task<List<MenuViewModel>> GetMenus()
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"select  m.MenuId, m.MenuInfo_Des, m.MenuInfo_Name, fi.FoodInfo_FoodName, fi.FoodInfo_Description, fi.FoodInfo_ImageUrl from FoodItem fi
                              inner join FoodAndMenu fm on  fi.FoodId = fm.FoodId
                              inner join Menu m on m.MenuId = fm.MenuId";
                var result = await connection.QueryAsync<dynamic>(query);
                return ToMenuView(result);
            }
        }
        public List<MenuViewModel> ToMenuView(dynamic result)
        {
            List<MenuViewModel> menus = new List<MenuViewModel>();
            foreach(var item in result)
            {
                var existedMenu = menus.Where(x => x.MenuId.Equals(item.MenuId)).FirstOrDefault();
                if(existedMenu == null)
                {
                    List<FoodItemViewModel> foodItems = new List<FoodItemViewModel>();
                    foodItems.Add(new FoodItemViewModel
                    {
                        FoodId = item.FoodId,
                        ImageUrl = item.FoodInfo_ImageUrl,
                        FoodName = item.FoodInfo_FoodName,
                        Description = item.FoodInfo_Description
                    });
                    menus.Add(new MenuViewModel
                    {
                        Description = item.MenuInfo_Des,
                        MenuName = item.MenuInfo_Name,
                        MenuId = item.MenuId,
                        FoodItems = foodItems
                    });
                }
                else
                {
                    existedMenu.FoodItems.Add(new FoodItemViewModel
                    {
                        FoodId = item.FoodId,
                        ImageUrl = item.FoodInfo_ImageUrl,
                        FoodName = item.FoodInfo_FoodName,
                        Description = item.FoodInfo_Description
                    });
                }
            }
            return menus;
        }
        public async Task<RestaurantInfoViewModel> GetRestaurantName(string resId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"select r.Name, r.Address_District, r.Address_Street, r.Address_Ward from dbo.Restaurant r where r.ResId = @resId";
                var result = await connection.QueryAsync<dynamic>(query, new { resId });
                return ToRestaurantInfoViewModel(result.FirstOrDefault());
            }
        }
        public RestaurantInfoViewModel ToRestaurantInfoViewModel(dynamic obj)
        {
            return new RestaurantInfoViewModel
            {
                Address = $"{obj.Address_Street}, {obj.Address_Ward}, {obj.Address_District}",
                Name = obj.Name
            };
        }

    
    }
}
