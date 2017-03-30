using MediatR;
using PollService.Data;
using PollService.Data.Model;
using PollService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace PollService.Features.Polls
{
    public class RemovePollCommand
    {
        public class RemovePollRequest : IRequest<RemovePollResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemovePollResponse { }

        public class RemovePollHandler : IAsyncRequestHandler<RemovePollRequest, RemovePollResponse>
        {
            public RemovePollHandler(PollServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemovePollResponse> Handle(RemovePollRequest request)
            {
                var poll = await _context.Polls.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                poll.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemovePollResponse();
            }

            private readonly PollServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
