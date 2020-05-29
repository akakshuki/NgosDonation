using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Domain.EF;
using Domain.Repository;
using WebMvc.CommonHelper;
using WebMvc.Configurations;
using WebMvc.Models.ModelView;

namespace WebMvc.Models.Dao
{
    public class UserDao
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserDao()
        {
           
        }

        //Get All List User
        public List<UserDTO> GetAllUser()
        {
            return MapperProfile.MapperConfig().Map<List<User>, List<UserDTO>>(_unitOfWork.UserRepository.Get().ToList());
        }

        public UserDTO GetUserByEmail( string email)
        {
            return MapperProfile.MapperConfig().Map<User, UserDTO>(_unitOfWork.UserRepository.Get().SingleOrDefault(x=>x.UserMail== email));
        }


        public object GetUserById(int id)
        {
            var data = MapperProfile.MapperConfig().Map<User,UserDTO>(_unitOfWork.UserRepository.GetById(id));
            data.UserDonates = MapperProfile.MapperConfig().Map<List<UserDonate>, List<UserDonateDTO>>(_unitOfWork.UserDonateRepository.Get().Where(x=>x.UserID == data.ID).ToList());
            return data;
        }

        public void UnOrActiveAccount(int id)
        {
            var data = _unitOfWork.UserRepository.GetById(id);
            data.UserActive = !data.UserActive;
            _unitOfWork.UserRepository.Edit(data);
            _unitOfWork.Commit();
          }

        public void SetOrUnsetVolunteerAccount(int id)
        {
            var data = _unitOfWork.UserRepository.GetById(id);
            data.UserVolunteer = !data.UserVolunteer;
            _unitOfWork.UserRepository.Edit(data);
            _unitOfWork.Commit();
    }

        public object getUserDonateInCurrentDate()
        {
            return MapperProfile.MapperConfig().Map<List<UserDonate>, List<UserDonateDTO>>(_unitOfWork.UserDonateRepository.Get().Where(k=> k.DateCreate.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy")).OrderByDescending(x=>x.DateCreate).ToList());
        }



        public UserDTO Register(UserDTO user)
        {
            var userData = MapperProfile.MapperConfig().Map<UserDTO, User>(user);
            userData.UserPwd = Encrypt.EncryptPasswordMD5(userData.UserPwd);
            userData.UserDateCreate = DateTime.Now;
            //add only user role;
            userData.RoleID = 2; 
            var userd = _unitOfWork.UserRepository.CreateOnlyData(userData);
            if (_unitOfWork.Commit()) { return user; };
            return null;
        } 


        public UserDTO UserLogin(string email, string password)
        {
            return MapperProfile.MapperConfig().Map<User,UserDTO>(_unitOfWork.UserRepository.Get().SingleOrDefault(x => x.UserMail == email));

        }

        public bool ResetPassword(string email, string newPassword)
        {
            var data = _unitOfWork.UserRepository.Get().SingleOrDefault(x => x.UserMail== email);
            if (data == null) return false;
            data.UserPwd = Encrypt.EncryptPasswordMD5(newPassword);
            _unitOfWork.UserRepository.Edit(data);
            return _unitOfWork.Commit();
        }
    }
}