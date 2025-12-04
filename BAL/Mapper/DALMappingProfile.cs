using AutoMapper;
using BAL.DataModel;

namespace BAL.Mapper
{
    public class DALMappingProfile:Profile
    {
        public DALMappingProfile()
        {
            CreateMap<StudentDataModel, DAL.Dto.StudentDto>().ReverseMap();
            CreateMap<GuardianDataModel, DAL.Dto.GuardianDto>().ReverseMap();
            CreateMap<EducationDataModel, DAL.Dto.EducationDto>().ReverseMap();
            CreateMap<LibraryDataModel, DAL.Dto.LibraryDto>().ReverseMap();
            CreateMap<HostelDataModel, DAL.Dto.HostelDto>().ReverseMap();
            CreateMap<UpdateStudentModel, DAL.Dto.UpdateStudentDto>().ReverseMap();
            CreateMap<RefreshTokenDataModel, DAL.Dto.RefreshTokenDto>().ReverseMap();
        }
    }
}
