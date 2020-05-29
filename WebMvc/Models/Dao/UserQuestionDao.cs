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
    public class UserQuestionDao
    {
        private IUnitOfWork _unitOfWork;

        public UserQuestionDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<UserQuestionDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<UserQuestion>, List<UserQuestionDTO>>(_unitOfWork.UserQuestionRepository.Get().ToList());
        }

        public UserQuestionDTO GetByid(int id)
        {
            var data = MapperProfile.MapperConfig().Map<UserQuestion, UserQuestionDTO>(_unitOfWork.UserQuestionRepository.GetById(id));
            return data;
        }

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
    }
}