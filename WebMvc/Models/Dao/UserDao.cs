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
    /// <summary>
    /// Data Access Object for User.
    /// </summary>
    public class UserDao
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDao"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public UserDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A list of users.</returns>
        public List<UserDTO> GetAllUser()
        {
            return MapperProfile.MapperConfig().Map<List<User>, List<UserDTO>>(_unitOfWork.UserRepository.Get().ToList());
        }

        /// <summary>
        /// Gets a user by their email.
        /// </summary>
        /// <param name="email">The email of the user.</param>
        /// <returns>The user.</returns>
        public UserDTO GetUserByEmail(string email)
        {
            return MapperProfile.MapperConfig().Map<User, UserDTO>(_unitOfWork.UserRepository.Get().SingleOrDefault(x => x.UserMail == email && x.UserActive));
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user.</returns>
        public object GetUserById(int id)
        {
            var data = MapperProfile.MapperConfig().Map<User, UserDTO>(_unitOfWork.UserRepository.GetById(id));
            data.UserDonates = MapperProfile.MapperConfig().Map<List<UserDonate>, List<UserDonateDTO>>(_unitOfWork.UserDonateRepository.Get().Where(x => x.UserID == data.ID).ToList());
            return data;
        }

        /// <summary>
        /// Activates or deactivates a user account.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        public void UnOrActiveAccount(int id)
        {
            var data = _unitOfWork.UserRepository.GetById(id);
            data.UserActive = !data.UserActive;
            _unitOfWork.UserRepository.Edit(data);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Sets or unsets a user as a volunteer.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        public void SetOrUnsetVolunteerAccount(int id)
        {
            var data = _unitOfWork.UserRepository.GetById(id);
            data.UserVolunteer = !data.UserVolunteer;
            _unitOfWork.UserRepository.Edit(data);
            _unitOfWork.Commit();
        }

        /// <summary>
        /// Gets all user donations for the current date.
        /// </summary>
        /// <returns>A list of user donations.</returns>
        public object getUserDonateInCurrentDate()
        {
            return MapperProfile.MapperConfig().Map<List<UserDonate>, List<UserDonateDTO>>(_unitOfWork.UserDonateRepository.Get().Where(k => k.DateCreate.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy")).OrderByDescending(x => x.DateCreate).ToList());
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user to register.</param>
        /// <returns>The registered user.</returns>
        public UserDTO Register(UserDTO user)
        {
            var userData = MapperProfile.MapperConfig().Map<UserDTO, User>(user);
            userData.UserPwd = Encrypt.EncryptPasswordMD5(userData.UserPwd);
            userData.UserDateCreate = DateTime.Now;
            userData.UserActive = true;
            //add only user role;
            userData.RoleID = 2;
            var userd = _unitOfWork.UserRepository.CreateOnlyData(userData);
            if (_unitOfWork.Commit()) { return user; };
            return null;
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>The logged-in user.</returns>
        public UserDTO UserLogin(string email, string password)
        {
            return MapperProfile.MapperConfig().Map<User, UserDTO>(_unitOfWork.UserRepository.Get().SingleOrDefault(x => x.UserMail == email && x.UserActive));
        }

        /// <summary>
        /// Resets a user's password.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>True if the password was reset successfully, otherwise false.</returns>
        public bool ResetPassword(string email, string newPassword)
        {
            var data = _unitOfWork.UserRepository.Get().SingleOrDefault(x => x.UserMail == email && x.UserActive);
            if (data == null) return false;
            data.UserPwd = Encrypt.EncryptPasswordMD5(newPassword);
            _unitOfWork.UserRepository.Edit(data);
            return _unitOfWork.Commit();
        }

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="newpass">The new password.</param>
        /// <param name="email">The user's email.</param>
        /// <returns>True if the password was changed successfully, otherwise false.</returns>
        public bool ChangePassword(string newpass, string email)
        {
            var user = _unitOfWork.UserRepository.Get().SingleOrDefault(x => x.UserMail == email);
            if (user == null) return false;
            user.UserPwd = Encrypt.EncryptPasswordMD5(newpass);
            _unitOfWork.UserRepository.Edit(user);
            return _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates a user's information.
        /// </summary>
        /// <param name="user">The user's new information.</param>
        /// <returns>True if the information was updated successfully, otherwise false.</returns>
        public bool UpdateInfo(UserDTO user)
        {
            var data = _unitOfWork.UserRepository.Get().SingleOrDefault(x => x.UserMail == user.UserMail);
            data.UserName = user.UserName;
            data.UserDOB = user.UserDOB;
            data.UserGender = user.UserGender;
            _unitOfWork.UserRepository.Edit(data);
            return _unitOfWork.Commit();
        }
    }
}