using System.ComponentModel.DataAnnotations;

namespace Re_ABP_Backend.Data.Helpers.Validation
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            int maxSizeInMB = _maxFileSize / (1024 * 1024);
            return $"Maximum allowed file size is {maxSizeInMB} MB.";
        }

    }
}
