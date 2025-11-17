using BAL.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Validator
{
    public class HostelValidation:AbstractValidator<HostelDto>
    {
        public HostelValidation() 
        {
            RuleFor(x=>x.Address).NotEmpty().WithMessage("Hostel address is required.");
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Hostel name is required.");
            RuleFor(x=>x.WardenName).NotEmpty().WithMessage("Hostel warden name is required.");
            RuleFor(x=>x.PhoneNumber).ValidPhoneNumber();
        }
    }
}
