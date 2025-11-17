using BAL.Dto;
using DAL.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Validator
{
    public class StudentValidation: AbstractValidator<StudentDto>
    {
        public StudentValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Student first name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Student last name is required.");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date of birth is required.").Must(g => g.Year > 1980).WithMessage("Student date of birth must be greater then 1980");
            RuleFor(s => s.Email).ValidEmail();
            RuleFor(s => s.CellNumber).ValidPhoneNumber();
            RuleFor(s => s.Gender).ValidGender();
            RuleFor(s => s.BloodGroup).ValidBloodGroup();
        }
    }
}
