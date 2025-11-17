using AutoMapper;
using BAL.Dto;
using OIC.RequestDto;
using OIC.ResponseDto;
using OIC.ResponseDto;

namespace OIC.Mapper
{
    public class UIMappingProfile : Profile
    {
        public UIMappingProfile()
        {
            CreateMap<CreateStudentRequestDto, StudentDto>().ReverseMap();
            CreateMap<StudentResponseDto, StudentDto>().ReverseMap();
            CreateMap<UpdateStudentRequestDto, StudentDto>().ReverseMap();

            CreateMap<CreateGuardianRequestDto, GuardianDto>().ReverseMap();
            CreateMap<UpdateGuardianRequestDto, GuardianDto>().ReverseMap();
            CreateMap<GuardianResponseDto, GuardianDto>().ReverseMap();

            CreateMap<CreateLibraryRequestDto, LibraryDto>().ReverseMap();
            CreateMap<UpdateLibraryRequestDto, LibraryDto>().ReverseMap();
            CreateMap<LibraryResponseDto, LibraryDto>().ReverseMap();

            CreateMap<CreateEducationRequestDto, EducationDto>().ReverseMap();
            CreateMap<UpdateEducationRequestDto, EducationDto>().ReverseMap();
            CreateMap<EducationResponseDto, EducationDto>().ReverseMap();

            CreateMap<CreateHostelRequestDto, HostelDto>().ReverseMap();
            CreateMap<UpdateHostelRequestDto, HostelDto>().ReverseMap();
            CreateMap<HostelResponseDto, HostelDto>().ReverseMap();

            CreateMap<OnBoardStudentRequestDto, OnBoardStudentDto>().ReverseMap();

            CreateMap<UpdateStudentDto, UpdateStudentRequestDto>().ReverseMap();

            CreateMap<StudentMiniResponse, StudentMiniDto>().ReverseMap();

        }
    }
}
