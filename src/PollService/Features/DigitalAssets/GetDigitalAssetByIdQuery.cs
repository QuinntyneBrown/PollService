using MediatR;
using PollService.Data;
using System.Threading.Tasks;
using PollService.Features.Core;

namespace PollService.Features.DigitalAssets
{
    public class GetDigitalAssetByIdQuery
    {
        public class GetDigitalAssetByIdRequest : IRequest<GetDigitalAssetByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetDigitalAssetByIdResponse
        {
            public DigitalAssetApiModel DigitalAsset { get; set; } 
		}

        public class GetDigitalAssetByIdHandler : IAsyncRequestHandler<GetDigitalAssetByIdRequest, GetDigitalAssetByIdResponse>
        {
            public GetDigitalAssetByIdHandler(IPollServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetDigitalAssetByIdResponse> Handle(GetDigitalAssetByIdRequest request)
            {                
                return new GetDigitalAssetByIdResponse()
                {
                    DigitalAsset = DigitalAssetApiModel.FromDigitalAsset(await _context.DigitalAssets.FindAsync(request.Id))
                };
            }

            private readonly IPollServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
