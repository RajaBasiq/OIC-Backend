using AutoMapper;
using Azure.Core;
using BAL.DataModel;
using BAL.Dto;
using BAL.Services.Interfaces;
using DAL.Dto;
using DAL.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BAL.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly ILogger<StudentService> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentRepo _studentRepo;

        public StudentService(ILogger<StudentService> logger,
            IMapper mapper,
            IStudentRepo studentRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _studentRepo = studentRepo;
        }
        public BAL.Dto.StudentDto Create(BAL.Dto.StudentDto request)
        {
            try
            {
                if (request is not null)
                {
                    var studentDataModel = _mapper.Map<StudentDataModel>(request);
                    var studentDto = _mapper.Map<DAL.Dto.StudentDto>(studentDataModel);
                    studentDto = _studentRepo.AddAsync(studentDto).Result;
                    studentDataModel = _mapper.Map<StudentDataModel>(studentDto);
                    var student = _mapper.Map<BAL.Dto.StudentDto>(studentDataModel);
                    return student;
                }
                throw new Exception("parameter can not be null");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public BAL.Dto.StudentDto Get(long id)
        {
            try
            {
                if (id > 0)
                {
                    var studentDto = _studentRepo.GetByIdAsync(id).Result;
                    var studentDataModel = _mapper.Map<StudentDataModel>(studentDto);
                    var student = _mapper.Map<BAL.Dto.StudentDto>(studentDataModel);
                    return student;
                }
                throw new Exception("Id must be greate then 0");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }
        public List<BAL.Dto.StudentMiniDto> GetAll()
        {
            try
            {
                var studentDtos = _studentRepo.GetStudentDetail().Result;
                var studentDataModels = _mapper.Map<List<StudentDataModel>>(studentDtos);
                var students = _mapper.Map<List<BAL.Dto.StudentMiniDto>>(studentDataModels);
                return students;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }
        public BAL.Dto.StudentDto Update(BAL.Dto.StudentDto dto)
        {
            try
            {
                if (dto == null || dto.Id == 0)
                    throw new ArgumentException("Invalid student data.");

                var studentModel = _mapper.Map<StudentDataModel>(dto);
                var studentDto = _mapper.Map<DAL.Dto.StudentDto>(studentModel);
                studentDto = _studentRepo.UpdateAsync(studentDto).Result;
                studentModel = _mapper.Map<StudentDataModel>(studentDto);
                var student = _mapper.Map<BAL.Dto.StudentDto>(studentModel);
                return student;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }
        public bool Delete(long id)
        {
            try
            {
                if (id > 0)
                {
                    var result = _studentRepo.DeleteAsync(id).Result;
                    return result;
                }
                throw new Exception("Id must be greate then 0");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

        }
        public BAL.Dto.StudentDto GetStudent(long id)
        {
            try
            {
                if (id > 0)
                {
                    var studentDto = _studentRepo.GetStudent(id).Result;
                    var studentDataModel = _mapper.Map<StudentDataModel>(studentDto);
                    var student = _mapper.Map<BAL.Dto.StudentDto>(studentDataModel);
                    return student;
                }
                throw new Exception("Id must be greate then 0");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
