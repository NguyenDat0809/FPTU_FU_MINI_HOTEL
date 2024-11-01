using System.ComponentModel.DataAnnotations;

namespace MiniHotelManagement_Razor.Extensions
{
    public class AllowedExtensions : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensions( string? errorMessage, params string[] extensions) : base(errorMessage) 
        {
            _extensions = extensions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult("This photo extension is not allowed!");
                }
            }

            return ValidationResult.Success;
        }
    }
}
