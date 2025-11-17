using BAL.Dto;

namespace BAL.Services.Interfaces
{
    public interface IHostelService
    {
        public BasicResponse<HostelDto> Create(HostelDto request);
        public HostelDto Get(long id);
        public List<HostelDto> GetAll();
        public HostelDto Update(HostelDto dto);
        public bool Delete(long id);
    }
}
