using MediatR;
using PollService.Data;
using PollService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace PollService.Features.PollResults
{
    public class GetPollResultsQuery
    {
        public class GetPollResultsRequest : IRequest<GetPollResultsResponse> { 
            public int? TenantId { get; set; }        
        }

        public class GetPollResultsResponse
        {
            public ICollection<PollResultApiModel> PollResults { get; set; } = new HashSet<PollResultApiModel>();
        }

        public class GetPollResultsHandler : IAsyncRequestHandler<GetPollResultsRequest, GetPollResultsResponse>
        {
            public GetPollResultsHandler(PollServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetPollResultsResponse> Handle(GetPollResultsRequest request)
            {
                var pollResults = await _context.PollResults
                    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetPollResultsResponse()
                {
                    PollResults = pollResults.Select(x => PollResultApiModel.FromPollResult(x)).ToList()
                };
            }

            private readonly PollServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
