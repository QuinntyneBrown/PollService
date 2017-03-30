using PollService.Data.Model;

namespace PollService.Features.Polls
{
    public class PollApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromPoll<TModel>(Poll poll) where
            TModel : PollApiModel, new()
        {
            var model = new TModel();
            model.Id = poll.Id;
            model.TenantId = poll.TenantId;
            model.Name = poll.Name;
            return model;
        }

        public static PollApiModel FromPoll(Poll poll)
            => FromPoll<PollApiModel>(poll);

    }
}
