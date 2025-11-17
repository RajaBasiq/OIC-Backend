using AutoMapper;
using BAL.DataModel;
using BAL.Services.Interfaces;
using DAL.Repository.Implementation;
using DAL.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implementation
{
    public class GuardianService:IGuardianService
    {
        private readonly ILogger<GuardianService> _logger;
        private readonly IMapper _mapper;
        private readonly IGuardianRepo _guardianRepo;
        public GuardianService(ILogger<GuardianService> logger,
    IMapper mapper,
    IGuardianRepo guardianRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _guardianRepo = guardianRepo;
        }
        public BAL.Dto.GuardianDto Create(BAL.Dto.GuardianDto request)
        {
            try
            {
                if (request is not null)
                {
                    var guardianDataModel = _mapper.Map<GuardianDataModel>(request);
                    var guardianDto = _mapper.Map<DAL.Dto.GuardianDto>(guardianDataModel);
                    guardianDto = _guardianRepo.AddAsync(guardianDto).Result;
                    guardianDataModel = _mapper.Map<GuardianDataModel>(guardianDto);
                    var guardian = _mapper.Map<BAL.Dto.GuardianDto>(guardianDataModel);
                    return guardian;
                }
                throw new Exception("parameter can not be null");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public BAL.Dto.GuardianDto Get(long id)
        {
            try
            {
                if (id > 0)
                {
                    var guardianDto = _guardianRepo.GetByIdAsync(id).Result;
                    var guardianDataModel = _mapper.Map<GuardianDataModel>(guardianDto);
                    var guardian = _mapper.Map<BAL.Dto.GuardianDto>(guardianDataModel);
                    return guardian;
                }
                throw new Exception("Id must be greate then 0");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }
        public List<BAL.Dto.GuardianDto> GetAll()
        {
            try
            {
                var studentDtos = _guardianRepo.GetAllAsync().Result;
                var studentDataModels = _mapper.Map<List<StudentDataModel>>(studentDtos);
                var students = _mapper.Map<List<BAL.Dto.GuardianDto>>(studentDataModels);
                return students;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }
        public BAL.Dto.GuardianDto Update(BAL.Dto.GuardianDto dto)
        {
            try
            {
                if (dto == null || dto.Id == 0)
                    throw new ArgumentException("Invalid student data.");

                var studentModel = _mapper.Map<StudentDataModel>(dto);
                var studentDto = _mapper.Map<DAL.Dto.GuardianDto>(studentModel);
                studentDto = _guardianRepo.UpdateAsync(studentDto).Result;
                studentModel = _mapper.Map<StudentDataModel>(studentDto);
                var student = _mapper.Map<BAL.Dto.GuardianDto>(studentModel);
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
                    var result = _guardianRepo.DeleteAsync(id).Result;
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
    }
}
