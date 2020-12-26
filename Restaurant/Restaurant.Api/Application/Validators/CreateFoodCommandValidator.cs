using FluentValidation;
using Restaurant.Api.Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Validators
{
    public class CreateFoodCommandValidator : AbstractValidator<CreateFoodCommand>
    {
        public CreateFoodCommandValidator()
        {
            RuleFor(u => u.FoodName).NotEmpty();
            RuleFor(u => u.RawImage).NotEmpty();
            RuleFor(u => u.Description).NotEmpty();
            RuleFor(u => u.ImgExt).NotEmpty();
        }
    }
}
