using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Command
{
    public class UpdateWorkHourCommand : IRequest<bool>
    {
        public string ResId { get; private set; }
        public string OpenTime { get; private set; }
        public string CloseTime { get; private set; }

        public UpdateWorkHourCommand(string resId, string openTime, string closeTime)
        {
            ResId = resId;
            OpenTime = openTime;
            CloseTime = closeTime;
        }
    }
}
