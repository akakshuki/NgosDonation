using Domain.EF;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMvc.Configurations;
using WebMvc.Models.ModelView;

namespace WebMvc.Models.Dao
{
    /// <summary>
    /// Data Access Object for User Question.
    /// </summary>
    public class UserQuestionDao
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserQuestionDao"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public UserQuestionDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all user questions.
        /// </summary>
        /// <returns>A list of user questions.</returns>
        public List<UserQuestionDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<UserQuestion>, List<UserQuestionDTO>>(_unitOfWork.UserQuestionRepository.Get().ToList());
        }

        /// <summary>
        /// Gets a user question by its ID.
        /// </summary>
        /// <param name="id">The ID of the user question.</param>
        /// <returns>The user question.</returns>
        public UserQuestionDTO GetByid(int id)
        {
            var data = MapperProfile.MapperConfig().Map<UserQuestion, UserQuestionDTO>(_unitOfWork.UserQuestionRepository.GetById(id));
            return data;
        }

        /// <summary>
        /// Inserts an answer for a user question.
        /// </summary>
        /// <param name="id">The ID of the user question.</param>
        /// <param name="answer">The answer to the question.</param>
        /// <returns>True if the answer was inserted successfully, otherwise false.</returns>
        public bool InsertAns(int id, string answer)
        {
            var data = _unitOfWork.UserQuestionRepository.GetById(id);
            if (data == null)
            {
                return false;
            }
            data.AnswerContent = answer;
            data.AnswerDateCreate = DateTime.Now;
            data.QuesNew = false;
            _unitOfWork.UserQuestionRepository.Edit(data);
            return _unitOfWork.Commit();
        }

        /// <summary>
        /// Creates a new user question.
        /// </summary>
        /// <param name="userQuestion">The user question to create.</param>
        /// <returns>True if the creation was successful, otherwise false.</returns>
        public bool Create(UserQuestionDTO userQuestion)
        {
            try
            {
                var data = MapperProfile.MapperConfig().Map<UserQuestionDTO, UserQuestion>(userQuestion);
                _unitOfWork.UserQuestionRepository.Create(data);
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}