using System.ComponentModel.DataAnnotations;

namespace BookStore.Helpers
{
    public class CustomValidationAttribute : ValidationAttribute
    {
        public CustomValidationAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string bookName = value.ToString();
                if (bookName.Contains(Text))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(ErrorMessage ?? $"Book Name Doesn't Contain The Desired Value! -> {Text}");
        }
    }
}
