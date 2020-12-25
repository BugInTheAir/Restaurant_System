using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Api.Application.Commands;
using User.Api.Application.Queries;
using User.Api.Models.DTOs;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserQueries _userQueries;

        public UserController(IMediator mediator, IUserQueries queries)
        {
            _mediator = mediator;
            _userQueries = queries;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string),(int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> RegisterUser([FromBody] UserRegisterDTO createUser)
        {
            try
            {
                var result = await _mediator.Send(new CreateUserCommand(createUser.Email, createUser.Password));
                return Ok("Created");
            }
            catch (Exception ex)
            {
                return BadRequest("Error" + ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Policy = "User")]
        [HttpGet]
        [ProducesResponseType(typeof(UserProfileDTO),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> GetProfile()
        {
            try
            {
                return Ok(await _userQueries.GetUserProfile(User.Claims.ToList().First(x => x.Type == "username").Value.ToString()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "User")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateUserBasicInfo([FromBody] UserBasicInfoDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await _mediator.Send(new UpdateUserBasicInfoCommand(dto, User.Claims.ToList().First(x => x.Type == "username").Value));
                return Ok();

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Policy = "User")]
        [Route("change-password")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> ChangePassword([FromBody] UserUpdatePasswordDTO dto)
        {
             
            try
            {
                if (await _mediator.Send(new UpdateUserPasswordCommand(dto, User.Claims.ToList().First(x => x.Type == "username").Value)))
                    return Ok("Updated");
                return BadRequest();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Policy = "User")]
        [Route("change-email")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> ChangeEmail([FromBody] UpdateEmailDTO dto )
        {
            try
            {
                if (await _mediator.Send(new ChangeEmailCommand(dto.EmailToUpdate, dto.OldEmail, User.Claims.ToList().First(x => x.Type == "username").Value)))
                    return Ok();
                return BadRequest();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("email/validate")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet]
        public async Task<ActionResult> ValidateEmail(string code,string email)
        {
            try
            {
                var result = await _mediator.Send(new ValidateEmailValidationCodeCommand(code,email));
                if (result)
                    return Ok();
                return BadRequest();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        
        [Route("password/recover")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult> RecoverPassword([FromBody] RecoveryPasswordDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new RecoverPasswordCommand(dto.Email));
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        
        [Route("password/newpassword")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<ActionResult> ChangeToNewPasswordWithRecoveryToken([FromBody] RecoveryPasswordDTO dto)
        {
            try
            {
                var result = await _mediator.Send(new ChangePasswordWithRecoveryTokenCommand(dto.Token,dto.Email,dto.NewPassword));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
    }
}
