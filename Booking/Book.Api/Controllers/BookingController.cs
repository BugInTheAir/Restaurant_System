using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Book.Api.Applications.Commands;
using Book.Api.Applications.Models;
using Book.Api.Applications.Queries;
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
        private readonly IBookTicketQueries _bookTicketQueries;
        public BookingController(IMediator mediator, IBookTicketQueries queries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(_mediator));
            _bookTicketQueries = queries;
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
                    model.Note, date.ToShortDateString(), int.Parse(model.AtHour), int.Parse(model.AtMinute), true));
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
                var userName = User.Claims.ToList().First(x => x.Type == "username").Value;
                if (DateTime.Parse(model.AtDate).Date.Subtract(DateTime.Now.Date).Ticks < 0)
                    return BadRequest("Invalid booking time");
                await _mediator.Send(new CreateBookingTicketCommand(model.ResId, userName, model.Email, model.Phone,
                    model.Note, date.ToShortDateString(), int.Parse(model.AtHour),int.Parse(model.AtMinute), false));
                return Ok("Created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
        [HttpGet]
        [Route("all")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "User")]
        [ProducesResponseType(typeof(List<UserBookTicketViewModel>),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetAllBooking()
        {
            try
            {
                var userName = User.Claims.ToList().First(x => x.Type == "username").Value;
                return Ok(await _bookTicketQueries.GetUserBookTickets(userName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
        }
    }
}
