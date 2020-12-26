using FluentValidation;
using Restaurant.Api.Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Validators
{
    public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
    {
        public CreateRestaurantCommandValidator()
        {
            RuleFor(r => r.CloseTime).NotEmpty().WithMessage("Close time can't be emptied");
            RuleFor(r => r.OpenTime).NotEmpty().WithMessage("Open time can't be emptied");
            RuleFor(r => r.Images).NotEmpty().WithMessage("Image can't be null or emptied");
            RuleFor(r => r.ResName).NotEmpty().WithMessage("Res name can't be null or emptied");
            RuleFor(r => r.Street).NotEmpty().WithMessage("Street can't be emptied");
            RuleFor(r => r.Ward).NotEmpty().WithMessage("Ward can't be emptied");
            RuleFor(r => r.District).NotEmpty().WithMessage("District can't be emptied");
            RuleFor(r => r.Menus).NotEmpty().WithMessage("Menus can't be emptied");
            RuleFor(r => r.Seats).NotEmpty().GreaterThan(1).WithMessage("Seats can't be emptied");
            RuleFor(r => r.TypeNames).NotEmpty();
            RuleFor(r => r.Phone).NotEmpty().Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}").WithMessage("Invalid phone number");
        }
    }
}
