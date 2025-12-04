using AutoMapper;
using DAL.Dto;
using DAL.Model;

namespace DAL.Mapper
{
    public class EntityMappingProfile: Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Student, StudentDto>()
                .ReverseMap();
            CreateMap<Student, UpdateStudentDto>()
                .ForMember(d => d.guardian, opt => opt.Ignore())
                .ForMember(d => d.hostel, opt => opt.Ignore())
                .ForMember(d => d.libraries, opt => opt.Ignore())
                .ForMember(d => d.educations, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(d => d.Guardian, opt => opt.Ignore())
                .ForMember(d => d.Hostel, opt => opt.Ignore())
                .ForMember(d => d.Libraries, opt => opt.Ignore())
                .ForMember(d => d.Educations, opt => opt.Ignore());
            CreateMap<Guardian, GuardianDto>()
                .ReverseMap();
            CreateMap<Library, LibraryDto>()
                .ReverseMap();
            CreateMap<Hostel, HostelDto>()
                .ReverseMap();
            CreateMap<Education, EducationDto>()
                .ReverseMap();
            CreateMap<RefreshToken, RefreshTokenDto>()
                .ReverseMap();
        }
    }
}
