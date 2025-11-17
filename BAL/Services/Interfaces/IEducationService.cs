using BAL.Dto;

namespace BAL.Services.Interfaces
{
    public interface IEducationService
    {
        public EducationDto Create(EducationDto request);
        public EducationDto Get(long id);
        public List<EducationDto> GetAll();
        public EducationDto Update(EducationDto dto);
        public bool Delete(long id);

    }
}
