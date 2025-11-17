using AutoMapper;
using BAL.Dto;
using BAL.Services.Implementation;
using BAL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OIC.RequestDto;
using OIC.ResponseDto;
using OIC.ResponseDto;

namespace OIC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuardianController : ControllerBase
    {
        private readonly ILogger<GuardianController> _logger;
        private readonly IGuardianService _guardianService;
        private readonly IMapper _mapper;

        public GuardianController(ILogger<GuardianController> logger,
            IGuardianService guardianService,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _guardianService= guardianService;
        }
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var guardian = _guardianService.GetAll();
                List<GuardianResponseDto> response = _mapper.Map<List<GuardianResponseDto>>(guardian);
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
                var student = _guardianService.Get(id);
                var responce = _mapper.Map<GuardianResponseDto>(student);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost, Route("Create")]
        public IActionResult Create([FromBodyAttribute] CreateGuardianRequestDto requestDto)
        {
            try
            {
                var guardianDto = _mapper.Map<GuardianDto>(requestDto);
                guardianDto = _guardianService.Create(guardianDto);
                var responce = _mapper.Map<GuardianResponseDto>(guardianDto);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut, Route("Update")]
        public IActionResult Update([FromBodyAttribute] UpdateGuardianRequestDto dto)
        {
            try
            {
                var guardianDto = _mapper.Map<GuardianDto>(dto);
                guardianDto = _guardianService.Update(guardianDto);
                var responce = _mapper.Map<GuardianResponseDto>(guardianDto);
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
                var result = _guardianService.Delete(id);
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
