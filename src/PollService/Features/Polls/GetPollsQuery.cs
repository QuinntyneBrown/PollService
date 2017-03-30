using MediatR;
using PollService.Data;
using PollService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace PollService.Features.Polls
{
    public class GetPollsQuery
    {
        public class GetPollsRequest : IRequest<GetPollsResponse> { 
            public int? TenantId { get; set; }        
        }

        public class GetPollsResponse
        {
            public ICollection<PollApiModel> Polls { get; set; } = new HashSet<PollApiModel>();
        }

        public class GetPollsHandler : IAsyncRequestHandler<GetPollsRequest, GetPollsResponse>
        {
            public GetPollsHandler(PollServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetPollsResponse> Handle(GetPollsRequest request)
            {
                var polls = await _context.Polls
                    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetPollsResponse()
                {
                    Polls = polls.Select(x => PollApiModel.FromPoll(x)).ToList()
                };
            }

            private readonly PollServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
