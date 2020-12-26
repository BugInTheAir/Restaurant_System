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
        //Database query
        //select r.Name, r.Address_Ward, r.Address_Street,r.RestaurantTypeTenantId,  r.Address_District, r.Seats, r.WorkTime_OpenTime, r.WorkTime_OpenTime, temp.FoodInfo_Description, temp.FoodInfo_FoodName, temp.FoodInfo_ImageUrl, temp.MenuInfo_Des, temp.MenuInfo_Name from Restaurant as r inner join ResAndMenu as rm On rm.ResId = r.Id inner join (  select m.MenuId, m.MenuInfo_Des, m.MenuInfo_Name, fi.FoodInfo_ImageUrl, fi.FoodInfo_FoodName, fi.FoodInfo_Description from Menu as m  inner join FoodAndMenu as fm on fm.MenuId = m.Id inner join FoodItem as fi on fm.FoodId = fi.FoodId)as temp on temp.MenuId = rm.MenuId 
        //where Address_Street like '%test%' and Address_Street like '%test2%' and Address_District like '%test%' 
        public async Task<List<RestaurantInformationViewModel>> GetRestaurant(string typeId, string address)
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
                if (address != null)
                {
                    result = result.Where(x =>
                    {
                        string addr = $"{x.Address_Street}, {x.Address_Ward}, {x.Address_District}";
                        if (addr.Contains(address))
                            return true;
                        return false;
                    });
                }
                if (typeId != null)
                {
                    result = result.Where(x =>
                    {
                        string id = x.RestaurantTypeTenantId;
                        if (id == typeId)
                            return true;
                        return false;
                    });
                }
                return ToRestaurantInformationView(result);
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
                            FoodName = item._FoodInfo_FoodName,
                            Description = item._FoodInfo_Description,
                            ImageUrl = item._FoodInfo_ImageUrl
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
                        TypeName = item.TypeName,
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
