namespace MyExamApi.Models
{
    public class QuestionRequest
    {
        public required string Id { get; set; }
        public required string Type { get; set; }

        public YesNoQuestion? YesNoQuestion { get; set; }
        public DateQuestion? DateQuestion { get; set; }
        public NumberQuestion? NumberQuestion { get; set; }
        public MultipleChoiceQuestion? MultipleChoiceQuestion { get; set; }
        public DropdownQuestion? DropdownQuestion { get; set; }

        public ParagraphQuestion? ParagraphQuestion { get; set; }
    }
}
