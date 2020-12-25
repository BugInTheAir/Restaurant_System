using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Email.Api.Models.DTOs;
using Email.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Email.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private IEmailService _service;
        public EmailController(IEmailService service)
        {
            _service = service;
        }
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult SendVerificationEmail([FromBody] EmailPayloadDTO dto)
        {
            _service.SendEmail(dto);
            return Ok();
        }
    }
}
