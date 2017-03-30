using PollService.Security;
using MediatR;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using static PollService.Features.Polls.AddOrUpdatePollCommand;
using static PollService.Features.Polls.GetPollsQuery;
using static PollService.Features.Polls.GetPollByIdQuery;
using static PollService.Features.Polls.RemovePollCommand;

namespace PollService.Features.Polls
{
    [Authorize]
    [RoutePrefix("api/poll")]
    public class PollController : ApiController
    {
        public PollController(IMediator mediator, IUserManager userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Route("add")]
        [HttpPost]
        [ResponseType(typeof(AddOrUpdatePollResponse))]
        public async Task<IHttpActionResult> Add(AddOrUpdatePollRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("update")]
        [HttpPut]
        [ResponseType(typeof(AddOrUpdatePollResponse))]
        public async Task<IHttpActionResult> Update(AddOrUpdatePollRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }
        
        [Route("get")]
        [AllowAnonymous]
        [HttpGet]
        [ResponseType(typeof(GetPollsResponse))]
        public async Task<IHttpActionResult> Get()
        {
            var request = new GetPollsRequest();
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("getById")]
        [HttpGet]
        [ResponseType(typeof(GetPollByIdResponse))]
        public async Task<IHttpActionResult> GetById([FromUri]GetPollByIdRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        [Route("remove")]
        [HttpDelete]
        [ResponseType(typeof(RemovePollResponse))]
        public async Task<IHttpActionResult> Remove([FromUri]RemovePollRequest request)
        {
            request.TenantId = (await _userManager.GetUserAsync(User)).TenantId;
            return Ok(await _mediator.Send(request));
        }

        protected readonly IMediator _mediator;
        protected readonly IUserManager _userManager;
    }
}
