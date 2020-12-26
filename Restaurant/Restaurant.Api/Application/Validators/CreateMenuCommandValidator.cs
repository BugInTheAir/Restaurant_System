using FluentValidation;
using Restaurant.Api.Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Validators
{
    public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuCommandValidator()
        {
            RuleFor(m => m.FoodItems).NotEmpty().WithMessage("Invalid food item");
            RuleFor(m => m.Name).NotEmpty().WithMessage("Invalid food name");
            RuleFor(m => m.Description).NotEmpty().WithMessage("Invalid food description");
        }
    }
}
