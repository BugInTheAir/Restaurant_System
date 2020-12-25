using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.Application.Queries;
using Restaurant.Api.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Restaurant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantQueries _restaurantQueries;
        public RestaurantController(IRestaurantQueries restaurantQueries)
        {
            _restaurantQueries = restaurantQueries;
        }
        [Route("all")]
        [ProducesResponseType(typeof(List<RestaurantInformationViewModel>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<ActionResult> GetAllRestaurant()
        {
            try
            {
                return Ok(await _restaurantQueries.GetRestaurant(null,null));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [ProducesResponseType(typeof(List<RestaurantInformationViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet("{typeId}/{street}")]
        public async Task<ActionResult> GetAllRestaurant(string typeId, string street)
        {
            try
            {
                if (street == null)
                    return Ok(await _restaurantQueries.GetRestaurant(typeId, null));
                else 
                {
                    return Ok(await _restaurantQueries.GetRestaurant(typeId, street));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
