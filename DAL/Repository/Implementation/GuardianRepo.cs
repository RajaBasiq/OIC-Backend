using AutoMapper;
using DAL.Dto;
using DAL.Model;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Implementation
{
    public class GuardianRepo: BaseRepository<Guardian, GuardianDto>,IGuardianRepo
    {
        public GuardianRepo(ApplicationDbContext context,IMapper mapper):base(context,mapper) { }   


    }
}
