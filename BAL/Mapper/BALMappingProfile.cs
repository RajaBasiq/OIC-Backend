using AutoMapper;
using BAL.DataModel;
using BAL.Dto;
using BAL.Mapper.CustomMappingExtensionMethod;

namespace BAL.Mapper
{
    public class BALMappingProfile: Profile
    {
        public BALMappingProfile()
        {
            CreateMap<StudentDataModel, StudentDto>().ReverseMap();
            CreateMap<GuardianDataModel, GuardianDto>().ReverseMap();
            CreateMap<LibraryDataModel, LibraryDto>().ReverseMap();
            CreateMap<HostelDataModel, HostelDto>().ReverseMap();
            CreateMap<EducationDataModel, EducationDto>().ReverseMap();
            CreateMap<UpdateStudentModel, UpdateStudentDto>().ReverseMap();
            this.StudentDataModelMappingStudentMiniDto();
        }
    }
}
