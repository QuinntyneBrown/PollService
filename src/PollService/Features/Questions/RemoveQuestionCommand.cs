using MediatR;
using PollService.Data;
using PollService.Data.Model;
using PollService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace PollService.Features.Questions
{
    public class RemoveQuestionCommand
    {
        public class RemoveQuestionRequest : IRequest<RemoveQuestionResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveQuestionResponse { }

        public class RemoveQuestionHandler : IAsyncRequestHandler<RemoveQuestionRequest, RemoveQuestionResponse>
        {
            public RemoveQuestionHandler(PollServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveQuestionResponse> Handle(RemoveQuestionRequest request)
            {
                var question = await _context.Questions.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                question.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveQuestionResponse();
            }

            private readonly PollServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
