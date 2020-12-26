using FluentValidation;
using Restaurant.Api.Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Validators
{
    public class UpdateWorkHourCommandValidator : AbstractValidator<UpdateWorkHourCommand>
    {
        public UpdateWorkHourCommandValidator()
        {
            RuleFor(x => x.OpenTime).NotEmpty();
            RuleFor(x => x.CloseTime).NotEmpty();
            RuleFor(x => x.ResId).NotEmpty();
        }
    }
}
