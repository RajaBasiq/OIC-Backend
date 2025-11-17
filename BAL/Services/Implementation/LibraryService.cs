using AutoMapper;
using BAL.DataModel;
using BAL.Dto;
using BAL.Services.Interfaces;
using DAL.Model;
using DAL.Repository.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implementation
{
    public  class LibraryService:ILibraryService
    {
        private readonly ILogger<LibraryService> _logger;
        private readonly IMapper _mapper;
        private readonly ILibraryRepo _libraryRepo;
        private readonly IValidator<LibraryDto> _libraryValidator;

        public LibraryService(ILogger<LibraryService> logger,
            IMapper mapper,
            ILibraryRepo libraryRepo,
            IValidator<LibraryDto> libraryValidator)
        {
            _mapper = mapper;
            _logger = logger;
            _libraryRepo = libraryRepo;
            _libraryValidator = libraryValidator;
        }
        public BasicResponse<BAL.Dto.LibraryDto> Create(BAL.Dto.LibraryDto request)
        {
            try
            {
                BasicResponse<BAL.Dto.LibraryDto> response = new BasicResponse<BAL.Dto.LibraryDto>();
                if (request is not null)
                {
                    var validationResult = _libraryValidator.Validate(request);
                    if (validationResult.IsValid)
                    {
                        var libraryDataModel = _mapper.Map<LibraryDataModel>(request);
                        var libraryDto = _mapper.Map<DAL.Dto.LibraryDto>(libraryDataModel);
                        libraryDto = _libraryRepo.AddAsync(libraryDto).Result;
                        libraryDataModel = _mapper.Map<LibraryDataModel>(libraryDto);
                        var library = _mapper.Map<BAL.Dto.LibraryDto>(libraryDataModel);
                        response.Status = true;
                        response.Data = library;
                        response.Message = "Library created successfully";
                    }
                    else
                    {
                        var errors = string.Join(',',validationResult.Errors.Select(e => e.ErrorMessage).ToList());
                        response.Status = false;
                        response.Data = null;
                        response.Message = errors;
                    }

                }
                else 
                {
                    response.Status = false;
                    response.Data = null;
                    response.Message = "request parameter can not be null";
                }
                return response;
                throw new Exception("parameter can not be null");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public BAL.Dto.LibraryDto Get(long id)
        {
            try
            {
                if (id > 0)
                {
                    var libraryDto = _libraryRepo.GetByIdAsync(id).Result;
                    var libraryDataModel = _mapper.Map<LibraryDataModel>(libraryDto);
                    var library = _mapper.Map<BAL.Dto.LibraryDto>(libraryDataModel);
                    return library;
                }
                throw new Exception("Id must be greate then 0");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }
        public List<BAL.Dto.LibraryDto> GetAll()
        {
            try
            {
                var libraryDtos = _libraryRepo.GetAllAsync().Result;
                var libraryDataModels = _mapper.Map<List<LibraryDataModel>>(libraryDtos);
                var librarys = _mapper.Map<List<BAL.Dto.LibraryDto>>(libraryDataModels);
                return librarys;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }

        }
        public BAL.Dto.LibraryDto Update(BAL.Dto.LibraryDto dto)
        {
            try
            {
                if (dto == null || dto.Id == 0)
                    throw new ArgumentException("Invalid library data.");

                var libraryModel = _mapper.Map<LibraryDataModel>(dto);
                var libraryDto = _mapper.Map<DAL.Dto.LibraryDto>(libraryModel);
                libraryDto = _libraryRepo.UpdateAsync(libraryDto).Result;
                libraryModel = _mapper.Map<LibraryDataModel>(libraryDto);
                var library = _mapper.Map<BAL.Dto.LibraryDto>(libraryModel);
                return library;
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
                    var result = _libraryRepo.DeleteAsync(id).Result;
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
