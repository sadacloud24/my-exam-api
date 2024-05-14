using MyExamApi.Models;

namespace MyExamApi.BA
{
    public interface IMiddleLayer
    {
        Task AddQuestionAsync(QuestionRequest questionRequest);
        Task UpdateQuestionAsync(QuestionRequest questionRequest);
        Task<IEnumerable<object>> GetQuestionsAsync(string type);
    }
}
