using AutoMapper;
using DAL.Dto;
using DAL.Model;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Implementation
{
    public class LibraryRepo:BaseRepository<Library,LibraryDto>,ILibraryRepo
    {
        public LibraryRepo(ApplicationDbContext context, IMapper mapper) : base(context, mapper) { }
    }
}
