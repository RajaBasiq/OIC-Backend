using BAL.Dto;
using FluentValidation;

namespace BAL.Validator
{
    public class LibraryValidation:AbstractValidator<LibraryDto>
    {
        public LibraryValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Library name is required.");
            RuleFor(x => x.Address).NotEmpty().WithMessage("Library address is required.");
            RuleFor(x => x.LibrarianName).NotEmpty().WithMessage("Librarian name is required.");
            RuleFor(x => x.PhoneNumber).ValidPhoneNumber();
        }
    }
}
