namespace MyExamApi.Models
{
    public class YesNoQuestion : QuestionBase
    {
        public bool Answer { get; set; }
    }

    public class DateQuestion : QuestionBase
    {
        public DateTime DateValue { get; set; }
    }

    public class NumberQuestion : QuestionBase
    {
        public double NumericValue { get; set; }
    }
}
