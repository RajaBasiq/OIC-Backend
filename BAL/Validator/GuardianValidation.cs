using BAL.Dto;
using FluentValidation;


namespace BAL.Validator
{
    public class GuardianValidation : AbstractValidator<GuardianDto>
    {
        public GuardianValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Guardian first name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Guardian last name is required.");
            RuleFor(x => x.Relationship).NotEmpty().WithMessage("Guardian relationship is required.");
            RuleFor(s => s.Email).ValidEmail();
            RuleFor(s => s.CellNumber).ValidPhoneNumber();

        }
    }
}
