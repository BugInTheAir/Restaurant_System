using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Command
{
    public class CreateMenuCommand : IRequest<bool>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<string> FoodItems { get; private set; }

        public CreateMenuCommand(string name, string description, List<string> foodItems)
        {
            Name = name;
            Description = description;
            FoodItems = foodItems;
        }
    }
}
