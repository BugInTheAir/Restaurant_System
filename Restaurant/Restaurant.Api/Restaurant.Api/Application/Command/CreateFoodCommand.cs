using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Command
{
    public class CreateFoodCommand : IRequest<bool>
    {
        public string ImgExt { get; private set; }
        public string FoodName { get; private set; }
        public byte[] RawImage { get; private set; }
        public string Description { get; private set; }

        public CreateFoodCommand(string imgExt, string foodName, byte[] rawImage, string description)
        {
            ImgExt = imgExt;
            FoodName = foodName;
            RawImage = rawImage;
            Description = description;
        }
    }
}
