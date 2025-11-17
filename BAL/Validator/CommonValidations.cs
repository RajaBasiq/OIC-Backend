using FluentValidation;

namespace BAL.Validator
{
    public static class ValidationExtensions
    {
        private static readonly string[] AllowedGenders = { "Male", "Female" };
        private static readonly string[] AllowedBloodGroups = { "A+", "A-", "B+", "B-", "O+", "O-", "AB+", "AB-" };

        public static IRuleBuilderOptions<T, string> ValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");
        }

        public static IRuleBuilderOptions<T, string> ValidPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d{10}$").WithMessage("Phone number must be 10 digits."); // Modify regex as needed
        }
        public static IRuleBuilderOptions<T, string> ValidGender<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Gender is required.")
                .Must(g => AllowedGenders.Any(allowed => string.Equals(allowed, g, StringComparison.OrdinalIgnoreCase)))
                .WithMessage("Gender must be one of 'Male' or 'Female'.");
        }
        public static IRuleBuilderOptions<T, string> ValidBloodGroup<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Blood group is required.")
                .Must(bg => AllowedBloodGroups.Any(allowed => string.Equals(allowed, bg, StringComparison.OrdinalIgnoreCase)))
                .WithMessage("Invalid blood group.");
        }
    }
}
