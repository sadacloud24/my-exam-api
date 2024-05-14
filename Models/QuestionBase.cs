namespace MyExamApi.Models
{
    public class QuestionBase
    {
        public required string Id { get; set; }
        public required string Type { get; set; } // e.g., "Paragraph", "YesNo", etc.
    }
}
