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
    public class AddOrUpdatePollResultCommand
    {
        public class AddOrUpdatePollResultRequest : IRequest<AddOrUpdatePollResultResponse>
        {
            public PollResultApiModel PollResult { get; set; }
            public int? TenantId { get; set; }
        }

        public class AddOrUpdatePollResultResponse { }

        public class AddOrUpdatePollResultHandler : IAsyncRequestHandler<AddOrUpdatePollResultRequest, AddOrUpdatePollResultResponse>
        {
            public AddOrUpdatePollResultHandler(PollServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdatePollResultResponse> Handle(AddOrUpdatePollResultRequest request)
            {
                var entity = await _context.PollResults
                    .SingleOrDefaultAsync(x => x.Id == request.PollResult.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.PollResults.Add(entity = new PollResult());
                entity.Name = request.PollResult.Name;
                entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdatePollResultResponse();
            }

            private readonly PollServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
