using Dapper;
using Microsoft.Data.SqlClient;
using Restaurant.Api.Application.ViewModels;
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
      
        public async Task<List<RestaurantInformationViewModel>> GetRestaurant(string typeName, string address)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = @"select r.ResId, r.Name, r.Phone, m.MenuInfo_Name, m.MenuInfo_Des, r.Address_District, r.Address_Street, r.Address_Ward, r.Seats, r.WorkTime_OpenTime, r.WorkTime_CloseTime, t.TypeName, ri.ImageUrl, f.FoodInfo_FoodName, f.FoodInfo_Description, f.FoodInfo_ImageUrl from Restaurant r
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
                            FoodItems = foodItems
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
                        FoodItems = foodItems
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

    }
}
