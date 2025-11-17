using AutoMapper;
using DAL.Dto;
using DAL.Model;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Implementation
{
    public class HostelRepo:BaseRepository<Hostel,HostelDto>,IHostelRepo
    {
        public HostelRepo(ApplicationDbContext context,IMapper mapper):base(context,mapper) { }    
    }
}
