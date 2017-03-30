using PollService.Data.Model;

namespace PollService.Features.PollResults
{
    public class PollResultApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromPollResult<TModel>(PollResult pollResult) where
            TModel : PollResultApiModel, new()
        {
            var model = new TModel();
            model.Id = pollResult.Id;
            model.TenantId = pollResult.TenantId;
            model.Name = pollResult.Name;
            return model;
        }

        public static PollResultApiModel FromPollResult(PollResult pollResult)
            => FromPollResult<PollResultApiModel>(pollResult);

    }
}
