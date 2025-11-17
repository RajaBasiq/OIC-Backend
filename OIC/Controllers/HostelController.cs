using AutoMapper;
using BAL.Dto;
using BAL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OIC.RequestDto;
using OIC.ResponseDto;

namespace OIC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelController : ControllerBase
    {
        private readonly ILogger<HostelController> _logger;
        private readonly IHostelService _hostelService;
        private readonly IMapper _mapper;

        public HostelController(ILogger<HostelController> logger,
            IHostelService hostelService,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _hostelService = hostelService;
        }
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var hostel = _hostelService.GetAll();
                List<HostelResponseDto> response = _mapper.Map<List<HostelResponseDto>>(hostel);
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
                var hostel = _hostelService.Get(id);
                var responce = _mapper.Map<HostelResponseDto>(hostel);
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
        public IActionResult Create([FromBody] CreateHostelRequestDto requestDto)
        {
            try
            {
                ResponseDto.BasicResponse<HostelResponseDto> response = new ResponseDto.BasicResponse<HostelResponseDto>();
                if (requestDto is null) 
                {
                    response.Status = false;
                    response.Message = "request parameter cannot be null";
                    response.Data = null;
                }
                var hostelDto = _mapper.Map<HostelDto>(requestDto);
                var result = _hostelService.Create(hostelDto);
                return result.Status ?Ok(result):BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut, Route("Update"),HttpOptions]
        public IActionResult Update([FromBody] UpdateHostelRequestDto dto)
        {
            try
            {
                var hostelDto = _mapper.Map<HostelDto>(dto);
                hostelDto = _hostelService.Update(hostelDto);
                var responce = _mapper.Map<HostelResponseDto>(hostelDto);
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
                var result = _hostelService.Delete(id);
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
