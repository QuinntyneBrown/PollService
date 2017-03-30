using PollService.Data.Model;

namespace PollService.Features.Questions
{
    public class QuestionApiModel
    {        
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public int? OrderIndex { get; set; }

        public string Description { get; set; }

        public QuestionType QuestionType { get; set; }

        public static TModel FromQuestion<TModel>(Question question) where
            TModel : QuestionApiModel, new()
        {
            var model = new TModel();
            model.Id = question.Id;
            model.TenantId = question.TenantId;
            model.Name = question.Name;
            model.Body = question.Body;
            model.OrderIndex = question.OrderIndex;
            model.Description = question.Description;
            model.QuestionType = question.QuestionType;
            return model;
        }

        public static QuestionApiModel FromQuestion(Question question)
            => FromQuestion<QuestionApiModel>(question);
    }
}
