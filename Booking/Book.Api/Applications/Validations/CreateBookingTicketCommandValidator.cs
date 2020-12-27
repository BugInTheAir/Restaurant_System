using Book.Api.Applications.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Api.Applications.Validations
{
    public class CreateBookingTicketCommandValidator : AbstractValidator<CreateBookingTicketCommand>
    {
        public CreateBookingTicketCommandValidator()
        {
            RuleFor(x => x.AtHour).GreaterThan(0).LessThan(24).NotEmpty().WithMessage("Invalid hour");
            RuleFor(x => x.AtMinute).GreaterThan(0).LessThan(60).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty().Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
            RuleFor(x => x.Email).NotEmpty().Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            RuleFor(x => x.BookDate).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.ResId).NotEmpty();
        }
    }
}
