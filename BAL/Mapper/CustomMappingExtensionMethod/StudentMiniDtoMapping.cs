using AutoMapper;
using BAL.DataModel;
using BAL.Dto;

namespace BAL.Mapper.CustomMappingExtensionMethod
{
    public static class StudentMiniDtoMapping
    {
        public static void StudentDataModelMappingStudentMiniDto(this Profile profile)
        {
            profile.CreateMap<StudentDataModel, StudentMiniDto>()
                .ForMember(d => d.GuardianName, s => s.MapFrom(src => src.Guardian==null? null: $"{src.Guardian.FirstName} {src.Guardian.LastName}"))
                .ForMember(d => d.GuardianEmail, s => s.MapFrom(src => src.Guardian==null?null:src.Guardian.Email))
                .ForMember(d => d.GuardianCellNumber, s => s.MapFrom(src => src.Guardian==null?null:src.Guardian.CellNumber))
                .ForMember(d => d.RelationWithGuardian, s => s.MapFrom(src => src.Guardian==null?null:src.Guardian.Relationship))
                .ForMember(d => d.HotelName, s => s.MapFrom(src => src.Hostel==null? null:src.Hostel.Name))
                .ForMember(d=>d.WardenName, s=>s.MapFrom(src => src.Hostel==null?null:src.Hostel.WardenName))
                .ForMember(d=>d.Libraries, s=>s.MapFrom(src => src.Libraries.Any()? string.Join(", ", src.Libraries.Where(l => l != null && !string.IsNullOrEmpty(l.Name)).Select(l => l.Name)) : null))
                .ForMember(d=>d.LatestDegree, s=>s.MapFrom(src => src.Educations.Any()? src.Educations.Where(eh => eh != null).OrderByDescending(eh => eh.Ongoing == true).ThenByDescending(eh => eh.EndingYear ?? int.MinValue).First().DegreeName:null));
        }
    }
}
