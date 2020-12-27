using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Book.Api.Applications.Commands;
using Book.Api.Applications.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookingController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
        }
        [HttpPost]
        [Route("anonymous")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> BookRestaurant([FromBody] UserBookingViewModel model)
        {
            try
            {
                var date = DateTime.Parse(model.AtDate);
                if (DateTime.Parse(model.AtDate).Date.Subtract(DateTime.Now.Date).Ticks < 0)
                    return BadRequest("Invalid booking time");
                await _mediator.Send(new CreateBookingTicketCommand(model.ResId, model.UserName, model.Email, model.Phone,
                    model.Note, date.ToShortDateString(), model.AtHour, model.AtMinute, true));
                return Ok("Created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("user")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "User")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> BookUserRes([FromBody] UserBookingViewModel model)
        {
            try
            {
                var date = DateTime.Parse(model.AtDate);
                if (DateTime.Parse(model.AtDate).Date.Subtract(DateTime.Now.Date).Ticks < 0)
                    return BadRequest("Invalid booking time");
                await _mediator.Send(new CreateBookingTicketCommand(model.ResId, model.UserName, model.Email, model.Phone,
                    model.Note, date.ToShortDateString(), model.AtHour, model.AtMinute, false));
                return Ok("Created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
