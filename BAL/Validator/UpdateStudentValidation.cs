using BAL.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Validator
{
    public class UpdateStudentValidation : AbstractValidator<UpdateStudentDto>
    {
        public UpdateStudentValidation() 
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Can not update without Id.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Student first name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Student last name is required.");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth is required.").Must(g => g.Year > 1980).WithMessage("Student date of birth must be greater then 1980");
            RuleFor(s => s.Email).ValidEmail();
            RuleFor(s => s.CellNumber).ValidPhoneNumber();
            RuleFor(s => s.Gender).ValidGender();
            RuleFor(s => s.BloodGroup).ValidBloodGroup();
            RuleFor(x => x.guardian).NotNull().WithMessage("Guardian is required.").SetValidator(new GuardianValidation());
            RuleForEach(x => x.educations).SetValidator(new EducationValidation()).When(x => x.educations != null);
            RuleForEach(x => x.libraries).SetValidator(new LibraryValidation()).When(x => x.libraries != null);
            RuleFor(x => x.hostel).SetValidator(new HostelValidation()).When(x => x.hostel != null);
        }
    }
}
