using AutoMapper;
using DAL.Common;
using DAL.Dto;
using DAL.Model;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repository.Implementation
{
    public class StudentOnboardingRepo:IStudentOnboardingRepo
    {
        private readonly ILogger<StudentOnboardingRepo> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentRepo _studentRepo;
        private readonly IGuardianRepo _guardianRepo;
        private readonly IEducationRepo _educationRepo;
        private readonly ILibraryRepo _libraryRepo;
        private readonly IHostelRepo _hostelRepo;
        private readonly ApplicationDbContext _dbContext;


        public StudentOnboardingRepo(ILogger<StudentOnboardingRepo> logger,
            IMapper mapper,
            IStudentRepo studentRepo,
            IGuardianRepo guardianRepo,
            IEducationRepo educationRepo,
            ILibraryRepo libraryRepo,
            IHostelRepo hostelRepo,
            ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _logger = logger;
            _studentRepo = studentRepo;
            _guardianRepo = guardianRepo;
            _educationRepo = educationRepo;
            _libraryRepo = libraryRepo;
            _hostelRepo = hostelRepo;
            _dbContext = dbContext;
        }

        public async Task<bool> OnBoardStudent(OnBoardStudentDto requestDto)
        {
            try
            {
                var guardianModel = _mapper.Map<Guardian>(requestDto.guardian);
                var studentModel = _mapper.Map<Student>(requestDto.student);
                var educationModel = _mapper.Map<List<Education>>(requestDto.educations);
                var libraryModel = _mapper.Map<List<Library>>(requestDto.libraries);
                var hostelModel = _mapper.Map<Hostel>(requestDto.hostel);

                studentModel.Guardian = guardianModel;
                studentModel.Educations = educationModel;
                studentModel.Hostel = hostelModel;
                _dbContext.Hostels.Attach(hostelModel);
                foreach (var library in libraryModel)
                {
                    _dbContext.Libraries.Attach(library);
                }
                studentModel.Libraries = libraryModel;
                await _dbContext.Set<Student>().AddAsync(studentModel);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error onboarding student");
                return false;
            }
        }
        public async Task<bool> UpdateStudentDetails(UpdateStudentDto dto)
        {
            try
            {
                // 1. Fetch the existing student entity
                var studentModel = await _dbContext.Students
                    .Include(s => s.Guardian)
                    .Include(s => s.Educations)
                    .Include(s=>s.Hostel)
                    .Include(s => s.Libraries) // For Many-to-Many
                    .FirstOrDefaultAsync(s => s.Id == dto.Id);

                if (studentModel == null)
                {
                    throw new Exception($"Student with ID {dto.Id} not found.");
                }                
                _mapper.Map(dto, studentModel);
                _mapper.Map(dto.guardian, studentModel.Guardian);
                studentModel.Guardian.Student = studentModel;
                studentModel.Educations.Clear();
                _mapper.Map(dto.educations, studentModel.Educations);
                foreach (var edu in studentModel.Educations) 
                {
                    edu.Student = studentModel;
                }
                studentModel.HostelId = dto.hostel?.Id;
                studentModel.Libraries.Clear();
                foreach (var libraryDto in dto.libraries)
                {
                    // Fetch the existing Library entity (assuming it must exist in the DB)
                    var library = await _dbContext.Libraries.FindAsync(libraryDto.Id);
                    if (library != null)
                    {
                        studentModel.Libraries.Add(library);
                    }
                }
                //// 3. Handle Navigation Properties (Complex Logic)

                //// 3a. Guardian (One-to-One with Student having the FK)
                //// Since Student owns the GuardianId, a simple update is often enough
                //if (dto.guardian != null)
                //{
                //    // Update Guardian's scalar properties using AutoMapper
                //    _mapper.Map(dto.guardian, existingStudent.Guardian);
                //}

                //// 3b. Hostel (Many-to-One)
                //// This typically updates the foreign key (HostelId) and potentially the entity reference.
                //if (dto.hostel != null)
                //{
                //    // Assuming HostelId is included in UpdateStudentDto or you're handling the FK
                //    // If the DTO only contains the HostelDto, you'll need to fetch the existing Hostel or update the FK.
                //    // For simplicity, we assume you're setting the FK based on the DTO's ID.
                //    existingStudent.HostelId = dto.hostel.Id;
                //}
                //else if (existingStudent.HostelId.HasValue)
                //{
                //    // If the DTO sets hostel to null, clear the FK
                //    existingStudent.HostelId = null;
                //}


                //// 3c. Educations (One-to-Many - typically replaces the entire collection or performs diff)
                //// This is complex: you must determine which Educations were added, modified, or removed.

                //// Example logic for replacing/updating the collection (simplistic approach):
                //// 1. Mark existing educations for deletion if not present in the new list
                //// --- Handling Educations ---

                //// 1. Identify and Remove/Delete existing Educations that are no longer in the DTO
                //var incomingEducationIds = dto.educations
                //    .Where(d => d.Id != 0) // Only look at DTOs that represent existing entities
                //    .Select(d => d.Id)
                //    .ToHashSet();

                //// Use a list to hold entities to delete to avoid modifying the collection while enumerating
                //var educationsToRemove = existingStudent.Educations
                //    .Where(e => !incomingEducationIds.Contains(e.Id))
                //    .ToList();

                //foreach (var education in educationsToRemove)
                //{
                //    // A clean way to mark a tracked entity for deletion.
                //    // Since you have Cascade Delete set, this is usually sufficient,
                //    // but explicitly telling the context to remove it is the safest bet.
                //    existingStudent.Educations.Remove(education);
                //    _dbContext.Educations.Remove(education);
                //}

                //// 2. Add or Update remaining educations
                //foreach (var eduDto in dto.educations)
                //{
                //    if (eduDto.Id != 0) // Existing Education
                //    {
                //        var existingEdu = existingStudent.Educations.FirstOrDefault(e => e.Id == eduDto.Id);
                //        if (existingEdu != null)
                //        {
                //            // Update properties
                //            _mapper.Map(eduDto, existingEdu);
                //        }
                //        // NOTE: If an ID exists in the DTO but is NOT in the fetched existingStudent.Educations
                //        // (meaning the relationship was established outside of this context or it was removed), 
                //        // you might need additional logic to handle re-establishing the relationship.
                //    }
                //    else // New Education (Id == 0)
                //    {
                //        var newEdu = _mapper.Map<Education>(eduDto);
                //        existingStudent.Educations.Add(newEdu); // EF Core tracks this as a new entity
                //    }
                //}

                //// 3d. Libraries (Many-to-Many)
                //// This involves updating the join table (StudentLibrary).
                //// 1. Clear the existing collection
                //existingStudent.Libraries.Clear();

                //// 2. Map and add the new collection from the DTO
                //if (dto.libraries != null)
                //{
                //    foreach (var libraryDto in dto.libraries)
                //    {
                //        // Fetch the existing Library entity (assuming it must exist in the DB)
                //        var library = await _dbContext.Libraries.FindAsync(libraryDto.Id);
                //        if (library != null)
                //        {
                //            existingStudent.Libraries.Add(library);
                //        }
                //    }
                //}
                //// 4. Save changes to the database
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error onboarding student");
                return false;
            }
        }
        public async Task<bool> UpdateStudentDetails1(UpdateStudentDto dto)
        {
            try { 
            // 1️⃣ Load the existing student (tracked by EF)
            var student = await _dbContext.Students
                        .Include(s => s.Guardian)
                        .Include(s => s.Educations)
                        .Include(s => s.Libraries)
                        .Include(s => s.Hostel)
                        .FirstOrDefaultAsync(s => s.Id == dto.Id);

            if (student == null)
                throw new Exception($"Student with ID {dto.Id} not found.");

            // 2️⃣ Map scalar fields (AutoMapper ignores navigations)
            _mapper.Map(dto, student);

                // 3️⃣ Update Guardian (1–1)
                if (dto.guardian != null)
                {
                    if (student.Guardian == null)
                        student.Guardian = _mapper.Map<Guardian>(dto.guardian);
                    else
                        _mapper.Map(dto.guardian, student.Guardian);
                }

                // 4️⃣ Update Hostel (optional 1–many)
                if (dto.hostel != null)
            {
                var hostel = await _dbContext.Hostels.FindAsync(dto.hostel.Id);
                if (hostel != null)
                    student.Hostel = hostel;
            }
            else
            {
                student.Hostel = null;
                student.HostelId = null;
            }

            // 5️⃣ Update Educations (1–many, cascade delete)
            var existingEducations = student.Educations.ToList();

            // Remove deleted educations
            foreach (var existing in existingEducations)
            {
                if (!dto.educations.Any(e => e.Id == existing.Id))
                    _dbContext.Educations.Remove(existing);
            }

            // Add or update educations
            foreach (var eduDto in dto.educations)
            {
                var existingEdu = existingEducations.FirstOrDefault(e => e.Id == eduDto.Id);
                if (existingEdu != null)
                    _mapper.Map(eduDto, existingEdu); // update existing
                else
                    student.Educations.Add(_mapper.Map<Education>(eduDto)); // add new
            }

            // 6️⃣ Update Libraries (many–many)
            student.Libraries.Clear();
            foreach (var libDto in dto.libraries)
            {
                var library = await _dbContext.Libraries.FindAsync(libDto.Id);
                if (library != null)
                    student.Libraries.Add(library);
            }
            // 7️⃣ Save
            await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error onboarding student");
                return false;
            }
        }
    }
}
