using BAL.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Validator
{
    public class EducationValidation: AbstractValidator<EducationDto>
    {
        public EducationValidation()
        {
            RuleFor(x => x.DegreeName).NotEmpty().WithMessage("Degree name is required.");

            RuleFor(x => x.StartingYear).NotEmpty().WithMessage("Starting year is required.");

            RuleFor(x => x.EndingYear).GreaterThan(x => x.StartingYear).WithMessage("Ending year must be greater than starting year.");

            RuleFor(x => x.Institute).NotEmpty().WithMessage("Institute name is required.");

            RuleFor(x => x.Ongoing).Must((dto, ongoing) =>
                    // Ongoing should be true if EndingYear is missing
                    !dto.EndingYear.HasValue ? ongoing == true : true).WithMessage("Ongoing must be true if EndingYear is missing.");
        }
    }
}
