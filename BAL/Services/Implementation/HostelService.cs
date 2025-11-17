using AutoMapper;
using BAL.DataModel;
using BAL.Dto;
using BAL.Services.Interfaces;
using DAL.Model;
using DAL.Repository.Implementation;
using DAL.Repository.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BAL.Services.Implementation
{
    public class HostelService:IHostelService
    {
        private readonly ILogger<HostelService> _logger;
        private readonly IMapper _mapper;
        private readonly IHostelRepo _hostelRepo;
        private readonly IValidator<HostelDto> _hostelValidator;

        public HostelService(ILogger<HostelService> logger,
            IMapper mapper,
            IHostelRepo hostelRepo,
            IValidator<HostelDto> hostelValidator)
        {
            _mapper = mapper;
            _logger = logger;
            _hostelRepo = hostelRepo;
            _hostelValidator = hostelValidator;
        }
        public BasicResponse<BAL.Dto.HostelDto> Create(BAL.Dto.HostelDto request)
        {
            try
            {
                BasicResponse<BAL.Dto.HostelDto> response = new BasicResponse<BAL.Dto.HostelDto>();
                if (request is not null)
                {
                    ValidationResult validation = _hostelValidator.Validate(request);
                    if (validation.IsValid)
                    {
                        var hostelDataModel = _mapper.Map<HostelDataModel>(request);
                        var hostelDto = _mapper.Map<DAL.Dto.HostelDto>(hostelDataModel);
                        hostelDto = _hostelRepo.AddAsync(hostelDto).Result;
                        hostelDataModel = _mapper.Map<HostelDataModel>(hostelDto);
                        var hostel = _mapper.Map<BAL.Dto.HostelDto>(hostelDataModel);
                        response.Status = true;
                        response.Message = "Hostel created successfully.";
                        response.Data = hostel;
                    }
                    else
                    {
                        var errors = string.Join(", ", validation.Errors.Select(e => e.ErrorMessage));
                        response.Status = false;
                        response.Message = errors;
                        response.Data = null;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = "Request data can not be null";
                    response.Data = null;
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public BAL.Dto.HostelDto Get(long id)
        {
            try
            {
                if (id > 0)
                {
                    var hostelDto = _hostelRepo.GetByIdAsync(id).Result;
                    var hostelDataModel = _mapper.Map<HostelDataModel>(hostelDto);
                    var hostel = _mapper.Map<BAL.Dto.HostelDto>(hostelDataModel);
                    return hostel;
                }
                throw new Exception("Id must be greate then 0");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }
        public List<BAL.Dto.HostelDto> GetAll()
        {
            try
            {
                var hostelDtos = _hostelRepo.GetAllAsync().Result;
                var hostelDataModels = _mapper.Map<List<HostelDataModel>>(hostelDtos);
                var hostels = _mapper.Map<List<BAL.Dto.HostelDto>>(hostelDataModels);
                return hostels;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }
        public BAL.Dto.HostelDto Update(BAL.Dto.HostelDto dto)
        {
            try
            {
                if (dto == null || dto.Id == 0)
                    throw new ArgumentException("Invalid hostel data.");

                var hostelModel = _mapper.Map<HostelDataModel>(dto);
                var hostelDto = _mapper.Map<DAL.Dto.HostelDto>(hostelModel);
                hostelDto = _hostelRepo.UpdateAsync(hostelDto).Result;
                hostelModel = _mapper.Map<HostelDataModel>(hostelDto);
                var hostel = _mapper.Map<BAL.Dto.HostelDto>(hostelModel);
                return hostel;
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
                    var result = _hostelRepo.DeleteAsync(id).Result;
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
