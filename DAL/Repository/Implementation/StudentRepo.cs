using AutoMapper;
using DAL.Dto;
using DAL.Model;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository.Implementation
{
    public class StudentRepo:BaseRepository<Student,StudentDto>,IStudentRepo
    {
        protected readonly DbSet<Student> _studentDbSet;
        public StudentRepo(ApplicationDbContext context,IMapper mapper):base(context,mapper) 
        {
            _studentDbSet = _context.Set<Student>();
        }
        public async Task<List<StudentDto>> GetStudentDetail() 
        {
            var students = await _studentDbSet
                                .Include(s=>s.Guardian)
                                .Include(s=>s.Educations)
                                .Include(s=>s.Libraries)
                                .Include(s=>s.Hostel)
                                .ToListAsync();
            return _mapper.Map<List<StudentDto>>(students);
        }
        public async Task<StudentDto> GetStudent(long id) 
        {
            var students = await _studentDbSet
                                .Include(s=>s.Guardian)
                                .Include(s=>s.Educations)
                                .Include(s=>s.Libraries)
                                .Include(s=>s.Hostel)
                                .Where(x=>x.Id==id)
                                .FirstOrDefaultAsync();
            return _mapper.Map<StudentDto>(students);
        }
    }
}
