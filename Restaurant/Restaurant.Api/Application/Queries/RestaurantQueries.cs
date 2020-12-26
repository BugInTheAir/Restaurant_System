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
                List<Object> parameters = new List<Object>();
                var query = @"select  r.ResId, r.Name, r.Phone, r.Address_Ward, ri.ImageUrl,r.RestaurantTypeTenantId, r.Address_Street, r.Address_District, r.Seats, r.WorkTime_CloseTime, r.WorkTime_OpenTime, temp.FoodInfo_Description, temp.FoodInfo_FoodName, temp.FoodInfo_ImageUrl, temp.MenuInfo_Des, temp.MenuInfo_Name from Restaurant as r inner join ResAndMenu as rm On rm.ResId = r.Id inner join (  select m.MenuId, m.MenuInfo_Des, m.MenuInfo_Name, fi.FoodInfo_ImageUrl, fi.FoodInfo_FoodName, fi.FoodInfo_Description from Menu as m  inner join FoodAndMenu as fm on fm.MenuId = m.Id inner join FoodItem as fi on fm.FoodId = fi.FoodId)as temp on temp.MenuId = rm.MenuId inner join dbo.ResImages as ri on r.ResId = ri.RestaurantsTenantId";
                var result = (await connection.QueryAsync<dynamic>(query, parameters));
                if (address != null || address != " ")
                {
                    result = result.Where(x => {
                        string addr = $"{x.Address_Street}, {x.Address_Ward}, {x.Address_District}";
                        if (addr.Contains(address))
                            return true;
                        return false;
                    });
                }
                if(typeId != null)
                {
                    result = result.Where(x => {
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
            foreach(var item in result)
            {
                var existedRes = resResult.Where(x => x.RestaurantName == result.Name).AsParallel().FirstOrDefault();
                if(existedRes != null)
                {
                    existedRes.ImageUrls.Add(result.ImageUrl);
                    var existedMenu = existedRes.Menus.Where(x => x.MenuName == result.MenuInfo_Name).AsParallel().FirstOrDefault();
                    if(existedMenu != null)
                    {
                        resResult.Where(x => x.RestaurantName == result.Name).AsParallel().FirstOrDefault().Menus.Where(x => x.MenuName == result.MenuInfo_Name).AsParallel().FirstOrDefault().FoodItems.Add(new FoodItemViewModel
                        {
                            Description = result.FoodInfo_Description,
                            FoodName = result.FoodInfo_FoodName,
                            ImageUrl = result.FoodInfo_ImageUrl
                        });
                        
                    }
                    else
                    {
                        var foodItems = new List<FoodItemViewModel>();
                        foodItems.Add(new FoodItemViewModel
                        {
                            FoodName = result._FoodInfo_FoodName,
                            Description = result._FoodInfo_Description,
                            ImageUrl = result._FoodInfo_ImageUrl
                        });
                        resResult.Where(x => x.RestaurantName == result.Name).AsParallel().FirstOrDefault().Menus.Add(new MenuViewModel
                        {
                            MenuName = result.MenuInfo_Name,
                            Description = result.MenuInfo_Des,
                            FoodItems = foodItems
                        });

                    }
                }
                else
                {
                    var menus = new List<MenuViewModel>();
                    var foodItems = new List<FoodItemViewModel>();
                    var images = new List<string>();
                    images.Add(result.ImageUrl);
                    foodItems.Add(new FoodItemViewModel
                    {
                        FoodName = result.FoodInfo_FoodName,
                        Description = result.FoodInfo_Description,
                        ImageUrl = result.FoodInfo_ImageUrl
                    });
                    menus.Add(new MenuViewModel
                    {
                        Description = result.MenuInfo.Des,
                        MenuName = result.MenuInfo.Name,
                        FoodItems = foodItems
                    });
                    var res = new RestaurantInformationViewModel
                    {
                        Address = new AddressViewModel
                        {
                            District = result.Address_District,
                            Street = result.Address_Street,
                            Ward = result.Address_Ward
                        },
                        ResId = result.ResId,
                        RestaurantName = result.Name,
                        Seats = result.Seats,
                        Phone = result.Phone,
                        WorkTime = new WorkTimeViewModel
                        {
                            CloseTime = result.WorkTime_CloseTime,
                            OpenTime = result.WorkTime_OpenTime
                        },
                        Menus = menus,
                        ImageUrls = images
                    };
                }
            }
            return resResult;
        }

    }
}
