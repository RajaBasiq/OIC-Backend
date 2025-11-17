using AutoMapper;
using DAL.Dto;
using DAL.Model;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Implementation
{
    public class EducationRepo:BaseRepository<Education,EducationDto>, IEducationRepo
    {
        public EducationRepo(ApplicationDbContext context, IMapper mapper) : base(context, mapper) 
        {
        }
    }
}
