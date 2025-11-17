using BAL.Dto;

namespace BAL.Services.Interfaces
{
    public interface ILibraryService
    {
        public BasicResponse<LibraryDto> Create(LibraryDto request);
        public LibraryDto Get(long id);
        public List<LibraryDto> GetAll();
        public LibraryDto Update(LibraryDto dto);
        public bool Delete(long id);
    }
}
