namespace MyExamApi.Models
{
    public class MultipleChoiceQuestion : QuestionBase
    {
        public required List<string> Choices { get; set; }
    }
}
