using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        ///Menu section
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Admin")]
        [Route("menu")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult> CreateMenu([FromBody] MenuRegistrationDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new CreateMenuCommand(dto.Name, dto.Description, dto.FoodItems));
                if (result)
                    return Ok();
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        ///End of menu section

        ///Food section
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Admin")]
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
        ///End food section

        ///Restaurant section
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

        [Authorize(AuthenticationSchemes = "Bearer", Policy = "Admin")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateNewRestaurant()
        {
            StringValues menus, name, street, ward, district, phone, typeNames, seats, openHour, openMinute, closeHour, closeMinute;
            Request.Form.TryGetValue("Menus", out menus);
            Request.Form.TryGetValue("Name", out name);
            Request.Form.TryGetValue("Street", out street);
            Request.Form.TryGetValue("Ward", out ward);
            Request.Form.TryGetValue("District", out district);
            Request.Form.TryGetValue("Phone", out phone);
            Request.Form.TryGetValue("TypeNames", out typeNames);
            Request.Form.TryGetValue("Seats", out seats);
            Request.Form.TryGetValue("OpenHour", out openHour);
            Request.Form.TryGetValue("OpenMinute", out openMinute);
            Request.Form.TryGetValue("CloseHour", out closeHour);
            Request.Form.TryGetValue("CloseMinute", out closeMinute);
            try
            {
                List<RestaurantImage> resImages = new List<RestaurantImage>();
                for (int i = 0; i < Request.Form.Files.Count(); i++)
                {
                    var file = Request.Form.Files[i];
                    var fileExt = file.FileName.Split('.').LastOrDefault().ToLower();
                    if (fileExt != "jpg" && fileExt != "jpeg")
                        return BadRequest("Invalid image type (only jpg and jpeg)");
                    Byte[] fileBytes;
                    if (file.Length < 0)
                    {
                        return BadRequest("Invalid image length");
                    }
                    var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                    resImages.Add(new RestaurantImage
                    {
                        FileExt = fileExt,
                        RawImage = fileBytes
                    });
                }
                if (resImages.Count == 0)
                    return BadRequest("Can't create restaurant with empty image");
                var result = await _mediator.Send(new CreateRestaurantCommand(name, phone,typeNames.ToList(), street, ward, district, GetOpenHours(int.Parse(openHour), int.Parse(openMinute), int.Parse(closeHour), int.Parse(closeMinute)
                    ), GetCloseHour(int.Parse(openHour), int.Parse(openMinute), int.Parse(closeHour), int.Parse(closeMinute)
                    ), int.Parse(seats), menus.ToList(), resImages));
                if (result)
                    return Ok();
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private string GetOpenHours(int openHour, int openMinute, int closeHour, int closeMinute)
        {
            if (openHour > 24 || openHour < 0 || closeHour > 24 || closeHour < 0 || openMinute >= 60 || closeMinute >= 60
                || openMinute < 0 || closeMinute < 0)
                throw new Exception("Invalid hour");
            return openHour.ToString() + ":" + openMinute.ToString();
        }
        private string GetCloseHour(int openHour, int openMinute, int closeHour, int closeMinute)
        {
            if (openHour > 24 || openHour < 0 || closeHour > 24 || closeHour < 0 || openMinute >= 60 || closeMinute >= 60
                || openMinute < 0 || closeMinute < 0)
                throw new Exception("Invalid hour");
            return closeHour.ToString() + ":" + closeMinute.ToString();
        }
        //End of restaurant section
    }
}
