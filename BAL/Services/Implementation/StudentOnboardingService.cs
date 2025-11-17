using AutoMapper;
using BAL.Dto;
using BAL.Services.Interfaces;
using DAL;
using DAL.Repository.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace BAL.Services.Implementation
{
    public class StudentOnboardingService : IStudentOnboardingService
    {
        private readonly ILogger<StudentOnboardingService> _logger;
        private readonly IMapper _mapper;
        private readonly IStudentOnboardingRepo _studentOnboardingRepo;
        private readonly IValidator<OnBoardStudentDto> _onBoardStudentDto;
        private readonly IValidator<UpdateStudentDto> _onUpdateStudentDto;



        public StudentOnboardingService(ILogger<StudentOnboardingService> logger,
            IMapper mapper,
            IStudentOnboardingRepo studentOnboardingRepo,
            IValidator<OnBoardStudentDto> onBoardStudentDto,
            IValidator<UpdateStudentDto> onUpdateStudentDto
            )
        {
            _mapper = mapper;
            _logger = logger;
            _studentOnboardingRepo = studentOnboardingRepo;
            _onBoardStudentDto = onBoardStudentDto;
            _onUpdateStudentDto = onUpdateStudentDto;
        }

        public async Task<IBasicResponse<bool>> OnBoardStudent(OnBoardStudentDto requestDto)
        {
            IBasicResponse<bool> response = new BasicResponse<bool>();
            try
            {
                var request = _mapper.Map<DAL.Dto.OnBoardStudentDto>(requestDto);
                ValidationResult contactValidation = _onBoardStudentDto.Validate(requestDto);

                if (!contactValidation.IsValid)
                {
                    response.Status = false;
                    response.Message =  string.Join(',',contactValidation.Errors.ToList());
                    response.Data = false;
                    return response;
                }
                var result = await _studentOnboardingRepo.OnBoardStudent(request);
                response.Status = result;
                response.Message = result ? "Student onboarded successfully." : "Failed to onboard student.";
                response.Data = result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }
        public async Task<IBasicResponse<bool>> UpdateStudentDetails(UpdateStudentDto requestDto)
        {
            IBasicResponse<bool> response = new BasicResponse<bool>();
            try
            {

                var model = _mapper.Map<BAL.DataModel.UpdateStudentModel>(requestDto);
                var request = _mapper.Map<DAL.Dto.UpdateStudentDto>(model);
                ValidationResult contactValidation = _onUpdateStudentDto.Validate(requestDto);

                if (!contactValidation.IsValid)
                {
                    response.Status = false;
                    response.Message = string.Join(',', contactValidation.Errors.ToList());
                    response.Data = false;
                    return response;
                }
                var result = await _studentOnboardingRepo.UpdateStudentDetails(request);
                response.Status = result;
                response.Message = result ? "Student onboarded successfully." : "Failed to onboard student.";
                response.Data = result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return response;
        }
    }
}
