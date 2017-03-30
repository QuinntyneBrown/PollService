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
    public class AddOrUpdateQuestionCommand
    {
        public class AddOrUpdateQuestionRequest : IRequest<AddOrUpdateQuestionResponse>
        {
            public QuestionApiModel Question { get; set; }
            public int? TenantId { get; set; }
        }

        public class AddOrUpdateQuestionResponse { }

        public class AddOrUpdateQuestionHandler : IAsyncRequestHandler<AddOrUpdateQuestionRequest, AddOrUpdateQuestionResponse>
        {
            public AddOrUpdateQuestionHandler(PollServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateQuestionResponse> Handle(AddOrUpdateQuestionRequest request)
            {
                var entity = await _context.Questions
                    .SingleOrDefaultAsync(x => x.Id == request.Question.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Questions.Add(entity = new Question());
                entity.Name = request.Question.Name;
                entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateQuestionResponse();
            }

            private readonly PollServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
