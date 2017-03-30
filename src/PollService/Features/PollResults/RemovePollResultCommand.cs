using MediatR;
using PollService.Data;
using PollService.Data.Model;
using PollService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace PollService.Features.PollResults
{
    public class RemovePollResultCommand
    {
        public class RemovePollResultRequest : IRequest<RemovePollResultResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemovePollResultResponse { }

        public class RemovePollResultHandler : IAsyncRequestHandler<RemovePollResultRequest, RemovePollResultResponse>
        {
            public RemovePollResultHandler(PollServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemovePollResultResponse> Handle(RemovePollResultRequest request)
            {
                var pollResult = await _context.PollResults.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                pollResult.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemovePollResultResponse();
            }

            private readonly PollServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
