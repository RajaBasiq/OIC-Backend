using DAL.Dto;

namespace DAL.Repository.Interfaces
{
    public interface IStudentRepo:IBaseRepository<StudentDto>
    {
        Task<List<StudentDto>> GetStudentDetail();
        Task<StudentDto> GetStudent(long id);
    }
}
