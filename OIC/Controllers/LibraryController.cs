using AutoMapper;
using BAL.Dto;
using BAL.Services.Interfaces;
using DAL.Repository.Implementation;
using DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OIC.RequestDto;
using OIC.ResponseDto;
using OIC.ResponseDto;

namespace OIC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILogger<LibraryController> _logger;
        private readonly ILibraryService _libraryService;
        private readonly IMapper _mapper;

        public LibraryController(ILogger<LibraryController> logger,
            ILibraryService libraryService,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _libraryService = libraryService;
        }
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var librarys = _libraryService.GetAll();
                List<LibraryResponseDto> response = _mapper.Map<List<LibraryResponseDto>>(librarys);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}"), Route("{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                var library = _libraryService.Get(id);
                var responce = _mapper.Map<LibraryResponseDto>(library);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpOptions]
        [HttpPost, Route("Create")]
        public IActionResult Create([FromBodyAttribute] CreateLibraryRequestDto requestDto)
        {
            try
            {
                OIC.ResponseDto.BasicResponse<LibraryResponseDto> response=new OIC.ResponseDto.BasicResponse<LibraryResponseDto>();
                if (requestDto is not null)
                {
                    var libraryDto = _mapper.Map<LibraryDto>(requestDto);
                    var result = _libraryService.Create(libraryDto);
                    var libraryResponseDto = _mapper.Map<LibraryResponseDto>(libraryDto);
                    response.Status = result.Status;
                    response.Message = result.Message;
                    response.Data = libraryResponseDto;
                }
                else
                {
                    response.Status = false;
                    response.Message = "Request parameter can not be null";
                    response.Data = null;
                }
                return response.Status? Ok(response):BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut, Route("Update"),HttpOptions]
        public IActionResult Update([FromBodyAttribute] UpdateLibraryRequestDto dto)
        {
            try
            {
                var libraryDto = _mapper.Map<LibraryDto>(dto);
                libraryDto = _libraryService.Update(libraryDto);
                var responce = _mapper.Map<LibraryResponseDto>(libraryDto);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}"), Route("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var result = _libraryService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
