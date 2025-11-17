using BAL.Dto;

namespace BAL.Services.Interfaces
{
    public interface IGuardianService
    {
        public GuardianDto Create(GuardianDto request);
        public GuardianDto Get(long id);
        public List<GuardianDto> GetAll();
        public GuardianDto Update(GuardianDto dto);
        public bool Delete(long id);
    }
}
