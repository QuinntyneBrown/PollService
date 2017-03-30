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
    public class AddOrUpdatePollCommand
    {
        public class AddOrUpdatePollRequest : IRequest<AddOrUpdatePollResponse>
        {
            public PollApiModel Poll { get; set; }
            public int? TenantId { get; set; }
        }

        public class AddOrUpdatePollResponse { }

        public class AddOrUpdatePollHandler : IAsyncRequestHandler<AddOrUpdatePollRequest, AddOrUpdatePollResponse>
        {
            public AddOrUpdatePollHandler(PollServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdatePollResponse> Handle(AddOrUpdatePollRequest request)
            {
                var entity = await _context.Polls
                    .SingleOrDefaultAsync(x => x.Id == request.Poll.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Polls.Add(entity = new Poll());
                entity.Name = request.Poll.Name;
                entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdatePollResponse();
            }

            private readonly PollServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
