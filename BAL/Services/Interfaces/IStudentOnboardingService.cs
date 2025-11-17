using BAL.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IStudentOnboardingService
    {
        Task<IBasicResponse<bool>> OnBoardStudent(OnBoardStudentDto requestDto);
        Task<IBasicResponse<bool>> UpdateStudentDetails(UpdateStudentDto requestDto);

    }
}
