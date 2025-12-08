using AutoMapper;
using BAL.Dto;
using BAL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OIC.RequestDto;
using OIC.ResponseDto;

namespace OIC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly ILogger<EducationController> _logger;
        private readonly IEducationService _educationService;
        private readonly IMapper _mapper;

        public EducationController(ILogger<EducationController> logger,
            IEducationService educationService,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _educationService = educationService;
        }
        [Authorize]
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var education = _educationService.GetAll();
                List<EducationResponseDto> response = _mapper.Map<List<EducationResponseDto>>(education);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}"), Route("{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                var education = _educationService.Get(id);
                var responce = _mapper.Map<EducationResponseDto>(education);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost, Route("Create")]
        public IActionResult Create([FromBody] CreateEducationRequestDto requestDto)
        {
            try
            {
                var educationDto = _mapper.Map<EducationDto>(requestDto);
                educationDto = _educationService.Create(educationDto);
                var responce = _mapper.Map<EducationResponseDto>(educationDto);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut, Route("Update")]
        public IActionResult Update([FromBody] UpdateEducationRequestDto dto)
        {
            try
            {
                var educationDto = _mapper.Map<EducationDto>(dto);
                educationDto = _educationService.Update(educationDto);
                var responce = _mapper.Map<EducationResponseDto>(educationDto);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}"), Route("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var result = _educationService.Delete(id);
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
