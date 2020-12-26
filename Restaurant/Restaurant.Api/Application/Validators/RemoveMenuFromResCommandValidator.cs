using FluentValidation;
using Restaurant.Api.Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Validators
{
    public class RemoveMenuFromResCommandValidator : AbstractValidator<RemoveMenuFromRestaurantCommand>
    {
        public RemoveMenuFromResCommandValidator()
        {
            RuleFor(x => x.MenuId).NotEmpty();
            RuleFor(x => x.ResId).NotEmpty();
        }
    }
}
