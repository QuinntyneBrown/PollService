using MediatR;
using PollService.Data;
using PollService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace PollService.Features.Questions
{
    public class GetQuestionByIdQuery
    {
        public class GetQuestionByIdRequest : IRequest<GetQuestionByIdResponse> { 
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetQuestionByIdResponse
        {
            public QuestionApiModel Question { get; set; } 
        }

        public class GetQuestionByIdHandler : IAsyncRequestHandler<GetQuestionByIdRequest, GetQuestionByIdResponse>
        {
            public GetQuestionByIdHandler(PollServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetQuestionByIdResponse> Handle(GetQuestionByIdRequest request)
            {                
                return new GetQuestionByIdResponse()
                {
                    Question = QuestionApiModel.FromQuestion(await _context.Questions.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly PollServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
