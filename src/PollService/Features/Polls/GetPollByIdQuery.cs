using MediatR;
using PollService.Data;
using PollService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace PollService.Features.Polls
{
    public class GetPollByIdQuery
    {
        public class GetPollByIdRequest : IRequest<GetPollByIdResponse> { 
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetPollByIdResponse
        {
            public PollApiModel Poll { get; set; } 
        }

        public class GetPollByIdHandler : IAsyncRequestHandler<GetPollByIdRequest, GetPollByIdResponse>
        {
            public GetPollByIdHandler(PollServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetPollByIdResponse> Handle(GetPollByIdRequest request)
            {                
                return new GetPollByIdResponse()
                {
                    Poll = PollApiModel.FromPoll(await _context.Polls.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly PollServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
