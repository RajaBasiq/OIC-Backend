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
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _studentService;
        private readonly IStudentOnboardingService _studentOnboardingService;
        private readonly IMapper _mapper;

        public StudentController(ILogger<StudentController> logger,
            IStudentService studentService,
            IStudentOnboardingService studentOnboardingService,
            IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _studentService = studentService;
            _studentOnboardingService = studentOnboardingService;
        }
        [Authorize]
        [HttpGet, Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var students = _studentService.GetAll();
                List<StudentMiniResponse> response = _mapper.Map<List<StudentMiniResponse>>(students);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet, Route("GetStudent/{id}")]
        public IActionResult GetStudent(long Id)
        {
            try
            {
                var student = _studentService.GetStudent(Id);
                StudentResponseDto response = _mapper.Map<StudentResponseDto>(student);
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
                var student = _studentService.Get(id);
                var responce = _mapper.Map<StudentResponseDto>(student);
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
        public IActionResult Create([FromBody] CreateStudentRequestDto requestDto)
        {
            try
            {
                var studentDto = _mapper.Map<StudentDto>(requestDto);
                studentDto=_studentService.Create(studentDto);
                var responce = _mapper.Map<StudentResponseDto>(studentDto);
                return Ok(responce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost, Route("OnBoardStudent")]
        public IActionResult OnBoardStudent([FromBody] OnBoardStudentRequestDto requestDto)
        {
            try
            {
                OIC.ResponseDto.IBasicResponse<bool> response = new OIC.ResponseDto.BasicResponse<bool>();
                if (requestDto is null) 
                {
                    response.Status = false;
                    response.Message = "request parameter can not be null";
                    response.Data = false;
                    return BadRequest(response);
                }
                OnBoardStudentDto dto = _mapper.Map<OnBoardStudentDto>(requestDto);
                var result = _studentOnboardingService.OnBoardStudent(dto).Result;
                return result.Status? Ok(result):BadRequest(result); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        
        [HttpPut, Route("UpdateStudentDetails")]
        public IActionResult UpdateStudentDetails([FromBody] UpdateStudentRequestDto requestDto)
        {
            try
            {
                OIC.ResponseDto.IBasicResponse<bool> response = new OIC.ResponseDto.BasicResponse<bool>();
                if (requestDto is null) 
                {
                    response.Status = false;
                    response.Message = "request parameter can not be null";
                    response.Data = false;
                    return BadRequest(response);
                }
                UpdateStudentDto dto = _mapper.Map<UpdateStudentDto>(requestDto);

                var result = _studentOnboardingService.UpdateStudentDetails(dto).Result;
                return result.Status? Ok(result):BadRequest(result); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        
        [HttpPut, Route("Update")]
        public IActionResult Update([FromBodyAttribute] UpdateStudentRequestDto dto)
        {
            try
            {
                var studentDto = _mapper.Map<StudentDto>(dto);
                studentDto = _studentService.Update(studentDto);
                var responce = _mapper.Map<StudentResponseDto>(studentDto);
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
                var result=_studentService.Delete(id);
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
