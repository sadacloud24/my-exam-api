namespace MyExamApi.Models
{
    public class DropdownQuestion : QuestionBase
    {
        public required List<string> Options { get; set; }
    }
}
