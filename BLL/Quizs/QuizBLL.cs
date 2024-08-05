using AutoMapper;
using QuizPlatform.DTO.Quiz;
using QuizPlatform.Infrastructure;
using QuizPlatform.Response;
using QuizPlatform.Models;

using System;
using QuizPlatform.Repository;
using Microsoft.EntityFrameworkCore;
using QuizPlatform.BLL.Quizs;
using QuizPlatform.Helder;

namespace QuizPlatform.BLL.Quizs
{
    public class QuizBLL : IQuizBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Quiz> _quizRepository;

        public QuizBLL(IUnitOfWork unitOfWork, IMapper mapper, IRepository<Quiz> quizRepository) {
            _unitOfWork=unitOfWork;

            _mapper = mapper;
            _quizRepository=quizRepository;
        }

        public async Task<IResponse<bool>> DeleteQuizAsync(int QuizId)
        {
            var response = new Response<bool>();
            try
            {

                var quiz = await _quizRepository.GetAsync(e => e.Id == QuizId);

                _quizRepository.Delete(quiz);
                if (UploadImage.DeleteImage(quiz.ImageStorages.FuLlPath))
                {
                    await _unitOfWork.CommitAsync();
                    return response.CreateResponse(true);
                }

                return response.CreateResponse(false);
            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }
        }

        public async Task<IResponse<bool>> CreateQuiz(QuizDto QuizDto)
        {
            var response = new Response<bool>();
            try
            {

                



                var mapped = _mapper.Map<Models.Quiz>(QuizDto);
                await _quizRepository.AddAsync(mapped);
                await _unitOfWork.CommitAsync();
                return response.CreateResponse(true);

            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }
        }

        public async Task<IResponse<QuizResultDto>> EditQuizAsync(QuizDto QuizDto)
        {
            var response = new Response<QuizResultDto>();
            try
            {

                var mapped = _mapper.Map<Models.Quiz>(QuizDto);
                var result = _quizRepository.Update(mapped);

                await _unitOfWork.CommitAsync();
                return response.CreateResponse(_mapper.Map<QuizResultDto>(result));

            }
            catch (Exception e)
            {
                return response.CreateResponse(e);

            }
        }

        public async Task<IResponse<QuizResultDto>> GetQuizById(int id)
        {
            var response = new Response<QuizResultDto>();

            try
            {
                var res = await _quizRepository.GetAsync(e => e.Id == id);
                return response.CreateResponse(_mapper.Map<QuizResultDto>(res));
            }
            catch (Exception e)
            {
                return response.CreateResponse(e);


            }


        }

        public async Task<IResponse<List<QuizResultDto>>> GetAllQuiz()
        {
            var response = new Response<List<QuizResultDto>>();

            try
            {
                var res =  _quizRepository.Where(e=>true).ToList();
                 
                return response.CreateResponse(_mapper.Map<List<QuizResultDto>>(res));
            }
            catch (Exception e)
            {
                return response.CreateResponse(e);


            }


        }



    }
}
