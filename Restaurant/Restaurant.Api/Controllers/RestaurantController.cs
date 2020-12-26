using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Restaurant.Api.Application.Command;
using Restaurant.Api.Application.DTOs;
using Restaurant.Api.Application.Queries;
using Restaurant.Api.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Restaurant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantQueries _restaurantQueries;
        private readonly IMediator _mediator;
        public RestaurantController(IRestaurantQueries restaurantQueries, IMediator mediator)
        {
            _restaurantQueries = restaurantQueries;
            _mediator = mediator;
        }
        [Route("food")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult> CreateFood()
        {
            StringValues foodName, description;
            try
            {
                Request.Form.TryGetValue("FoodName", out foodName);
                Request.Form.TryGetValue("Description", out description);
                var file = Request.Form.Files[0];
                var fileExt = file.FileName.Split('.').LastOrDefault().ToLower();
                if (fileExt != "jpg" && fileExt != "jpeg")
                    return BadRequest("Invalid image type (only jpg and jpeg)");
                Byte[] fileBytes;
                if (file.Length > 0)
                {
                    var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                    var result = await _mediator.Send(new CreateFoodCommand(fileExt, foodName, fileBytes, description));
                    if (result)
                        return Ok();
                    else
                        return BadRequest();
                }
                else
                {
                    return BadRequest("Invalid file length");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("all")]
        [ProducesResponseType(typeof(List<RestaurantInformationViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<ActionResult> GetAllRestaurant()
        {
            try
            {
                return Ok(await _restaurantQueries.GetRestaurant(null, null));
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
