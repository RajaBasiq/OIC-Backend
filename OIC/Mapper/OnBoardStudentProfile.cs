using AutoMapper;
using BAL.Dto;
using OIC.RequestDto;

namespace OIC.Mapper
{
    public class OnBoardStudentProfile : Profile
    {
        public OnBoardStudentProfile()
        {
            // Student
            CreateMap<CreateStudentRequestDto, StudentDto>()
                .ForMember(dest => dest.Guardian, opt => opt.Ignore())
                .ForMember(dest => dest.Hostel, opt => opt.Ignore())
                .ForMember(dest => dest.Libraries, opt => opt.Ignore())
                .ForMember(dest => dest.Educations, opt => opt.Ignore());

            // Guardian
            CreateMap<CreateGuardianRequestDto, GuardianDto>()
                .ForMember(dest => dest.Student, opt => opt.Ignore());

            // Education
            CreateMap<CreateEducationRequestDto, EducationDto>()
                .ForMember(dest => dest.Student, opt => opt.Ignore());

            // Library
            CreateMap<CreateLibraryRequestDto, LibraryDto>()
                .ForMember(dest => dest.Students, opt => opt.Ignore());

            // Hostel
            CreateMap<CreateHostelRequestDto, HostelDto>()
                .ForMember(dest => dest.Students, opt => opt.Ignore());

            // Main OnBoard mapping
            CreateMap<OnBoardStudentRequestDto, OnBoardStudentDto>()
                .ForMember(dest => dest.student, opt => opt.MapFrom(src => src.student))
                .ForMember(dest => dest.guardian, opt => opt.MapFrom(src => src.guardian))
                .ForMember(dest => dest.educations, opt => opt.MapFrom(src => src.educations))
                .ForMember(dest => dest.libraries, opt => opt.MapFrom(src => src.libraries))
                .ForMember(dest => dest.hostel, opt => opt.MapFrom(src => src.hostel));
        }
    }
}
