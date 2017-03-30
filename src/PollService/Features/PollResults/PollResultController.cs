using PollService.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static PollService.Features.PollResults.AddOrUpdatePollResultCommand;
using static PollService.Features.PollResults.GetPollResultsQuery;
using static PollService.Features.PollResults.GetPollResultByIdQuery;
using static PollService.Features.PollResults.RemovePollResultCommand;

namespace PollService.Features.PollResults
{
    [Authorize]
    [RoutePrefix("api/pollResult")]
    public class PollResultController : ApiController
    {
        public PollResultController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdatePollResultResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdatePollResultRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdatePollResultResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdatePollResultRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetPollResultsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetPollResultsRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetPollResultByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetPollResultByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemovePollResultResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemovePollResultRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
