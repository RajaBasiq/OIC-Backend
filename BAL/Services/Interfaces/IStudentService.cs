using BAL.Dto;

namespace BAL.Services.Interfaces
{
    public interface IStudentService
    {
        public StudentDto Create(StudentDto request);
        public StudentDto Get(long id);
        public List<StudentMiniDto> GetAll ();
        public StudentDto Update(StudentDto dto);
        public bool Delete(long id);
        public StudentDto GetStudent(long id);
    }
}
