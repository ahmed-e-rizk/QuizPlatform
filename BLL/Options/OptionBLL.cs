using AutoMapper;
using QuizPlatform.DTO.Quiz;
using QuizPlatform.Infrastructure;
using QuizPlatform.Models;
using QuizPlatform.Repository;
using QuizPlatform.Response;

namespace QuizPlatform.BLL.Options
{
    public class OptionBLL :IOptionBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Option> _optionRepository;

        public OptionBLL(IUnitOfWork unitOfWork, IMapper mapper,  IRepository<Option> optionRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _optionRepository = optionRepository;
        }

        public async Task<IResponse<CreateOptionDto>> CreateOptionAsync(CreateOptionDto inputDto)
        {
            var response = new Response<CreateOptionDto>();

            try
            {
                await _optionRepository.AddAsync(_mapper.Map<Models.Option>(inputDto));
                await _unitOfWork.CommitAsync();
                return response.CreateResponse(inputDto);

            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }
        }
        public async Task<IResponse<CreateOptionDto>> EditOptionAsync(CreateOptionDto inputDto)
        {
            var response = new Response<CreateOptionDto>();

            try
            {
                _optionRepository.Update(_mapper.Map<Models.Option>(inputDto));
                await _unitOfWork.CommitAsync();
                return response.CreateResponse(inputDto);

            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }
        }
        public async Task<IResponse<bool>> DeleteOptionAsync(int id)
        {
            var response = new Response<bool>();

            try
            {
                var res = await _optionRepository.GetAsync(e => e.Id == id);
                _optionRepository.Delete(res);
                await _unitOfWork.CommitAsync();
                return response.CreateResponse(true);

            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }
        }

        public async Task<IResponse<OptionsDto>> GetOptionById(int id)
        {
            var response = new Response<OptionsDto>();


            try
            {
                var res =   await _optionRepository.GetAsync(e => e.Id == id);
                return response.CreateResponse(_mapper.Map<OptionsDto>(res));

            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }


        }

        public async Task<IResponse<List<OptionsDto>>> GetAllOptionByQuestionId(int id)
        {
            var response = new Response<List<OptionsDto>>();

            try
            {
                var res = _optionRepository.Where(e => e.QuestionId == id);
                return response.CreateResponse(_mapper.Map<List<OptionsDto>>(res));

            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }
        }

    }
}
