using BAL.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Validator
{
    public class StudentOnboardingValidation: AbstractValidator<OnBoardStudentDto>
    {
        public StudentOnboardingValidation()
        {
            RuleFor(x => x.student).NotNull().WithMessage("Student is required.").SetValidator(new StudentValidation());
            RuleFor(x => x.guardian).NotNull().WithMessage("Guardian is required.").SetValidator(new GuardianValidation());
            RuleForEach(x => x.educations).SetValidator(new EducationValidation()).When(x=>x.educations!=null);
            RuleForEach(x => x.libraries).SetValidator(new LibraryValidation()).When(x=>x.libraries!=null);
            RuleFor(x => x.hostel).SetValidator(new HostelValidation()).When(x=>x.hostel!=null);
        }

    }
}
