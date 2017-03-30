using MediatR;
using PollService.Data;
using PollService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace PollService.Features.PollResults
{
    public class GetPollResultByIdQuery
    {
        public class GetPollResultByIdRequest : IRequest<GetPollResultByIdResponse> { 
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetPollResultByIdResponse
        {
            public PollResultApiModel PollResult { get; set; } 
        }

        public class GetPollResultByIdHandler : IAsyncRequestHandler<GetPollResultByIdRequest, GetPollResultByIdResponse>
        {
            public GetPollResultByIdHandler(PollServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetPollResultByIdResponse> Handle(GetPollResultByIdRequest request)
            {                
                return new GetPollResultByIdResponse()
                {
                    PollResult = PollResultApiModel.FromPollResult(await _context.PollResults.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly PollServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
