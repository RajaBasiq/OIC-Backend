using AutoMapper;
using BAL.DataModel;
using BAL.Services.Interfaces;
using DAL.Repository.Implementation;
using DAL.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace BAL.Services.Implementation
{
    public class EducationService:IEducationService
    {
        private readonly ILogger<EducationService> _logger;
        private readonly IMapper _mapper;
        private readonly IEducationRepo _educationRepo;
        public EducationService(ILogger<EducationService> logger,
    IMapper mapper,
    IEducationRepo educationRepo)
        {
            _mapper = mapper;
            _logger = logger;
            _educationRepo = educationRepo;
        }
        public BAL.Dto.EducationDto Create(BAL.Dto.EducationDto request)
        {
            try
            {
                if (request is not null)
                {
                    var educationDataModel = _mapper.Map<EducationDataModel>(request);
                    var educationDto = _mapper.Map<DAL.Dto.EducationDto>(educationDataModel);
                    educationDto = _educationRepo.AddAsync(educationDto).Result;
                    educationDataModel = _mapper.Map<EducationDataModel>(educationDto);
                    var education = _mapper.Map<BAL.Dto.EducationDto>(educationDataModel);
                    return education;
                }
                throw new Exception("parameter can not be null");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public BAL.Dto.EducationDto Get(long id)
        {
            try
            {
                if (id > 0)
                {
                    var educationDto = _educationRepo.GetByIdAsync(id).Result;
                    var educationDataModel = _mapper.Map<EducationDataModel>(educationDto);
                    var education = _mapper.Map<BAL.Dto.EducationDto>(educationDataModel);
                    return education;
                }
                throw new Exception("Id must be greate then 0");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }
        public List<BAL.Dto.EducationDto> GetAll()
        {
            try
            {
                var educationDtos = _educationRepo.GetAllAsync().Result;
                var educationDataModels = _mapper.Map<List<EducationDataModel>>(educationDtos);
                var education = _mapper.Map<List<BAL.Dto.EducationDto>>(educationDataModels);
                return education;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }
        public BAL.Dto.EducationDto Update(BAL.Dto.EducationDto dto)
        {
            try
            {
                if (dto == null || dto.Id == 0)
                    throw new ArgumentException("Invalid education data.");

                var educationModel = _mapper.Map<EducationDataModel>(dto);
                var educationDto = _mapper.Map<DAL.Dto.EducationDto>(educationModel);
                educationDto = _educationRepo.UpdateAsync(educationDto).Result;
                educationModel = _mapper.Map<EducationDataModel>(educationDto);
                var education = _mapper.Map<BAL.Dto.EducationDto>(educationModel);
                return education;
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
                    var result = _educationRepo.DeleteAsync(id).Result;
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
