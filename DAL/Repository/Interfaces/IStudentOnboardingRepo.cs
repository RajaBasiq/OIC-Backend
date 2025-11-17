using DAL.Dto;

namespace DAL.Repository.Interfaces
{
    public interface IStudentOnboardingRepo
    {
        Task<bool> OnBoardStudent(OnBoardStudentDto requestDto);
        Task<bool> UpdateStudentDetails(UpdateStudentDto requestDto);
    }
}
