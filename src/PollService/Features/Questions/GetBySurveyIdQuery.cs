using MediatR;
using PollService.Data;
using PollService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace PollService.Features.Questions
{
    public class GetBySurveyIdQuery
    {
        public class GetBySurveyIdRequest : IRequest<GetBySurveyIdResponse>
        {
            public GetBySurveyIdRequest()
            {

            }
        }

        public class GetBySurveyIdResponse
        {
            public GetBySurveyIdResponse()
            {

            }
        }

        public class GetBySurveyIdHandler : IAsyncRequestHandler<GetBySurveyIdRequest, GetBySurveyIdResponse>
        {
            public GetBySurveyIdHandler(PollServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetBySurveyIdResponse> Handle(GetBySurveyIdRequest request)
            {
                throw new System.NotImplementedException();
            }

            private readonly PollServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
