using QuizPlatform.DTO.Quiz;
using QuizPlatform.Response;

namespace QuizPlatform.BLL.Options
{
    public interface IOptionBLL
    {
        Task<IResponse<CreateOptionDto>> CreateOptionAsync(CreateOptionDto inputDto);
        Task<IResponse<CreateOptionDto>> EditOptionAsync(CreateOptionDto inputDto);
        Task<IResponse<bool>> DeleteOptionAsync(int id);
        Task<IResponse<OptionsDto>> GetOptionById(int id);
        Task<IResponse<List<OptionsDto>>> GetAllOptionByQuestionId(int id);
    }
}
