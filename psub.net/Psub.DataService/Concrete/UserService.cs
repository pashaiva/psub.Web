using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Psub.DataService.DTO;
using Psub.DataAccess.Abstract;
using Psub.DataService.Abstract;
using Psub.Domain.Entities;

namespace Psub.DataService.Concrete
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public int SaveOrUpdate(UserDTO user)
        {
            var saveUser = user.Id > 0 ? GetUserById(user.Id) : new User();
            saveUser.Id = user.Id;
            saveUser.Name = user.Name;
            saveUser.Email = user.Email;
            saveUser.City = user.City;

            if (user.Id == 0)
            {
                saveUser.NickName = user.NickName;
                saveUser.Password = user.Password;
                saveUser.UserGuid = Guid.NewGuid().ToString();
                saveUser.DateRegistration = DateTime.Now;
            }
            else
            {
                if (user.Password == user.ConfirmPassword)
                    saveUser.Password = user.Password;
            }

            return _userRepository.SaveOrUpdate(saveUser);
        }

        public UserDTO GetUserDTOByGuid(string guid)
        {
            var user = _userRepository.Query().FirstOrDefault(m => m.UserGuid == guid);
            return user != null ? new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                NickName = user.NickName,
                DateRegistration = user.DateRegistration,
                City = user.City,
                Password = user.Password,
                TeamId = user.TeamId,
                LastUrl = user.LastUrl,
                Email = user.Email
            } : new UserDTO();
        }

        public List<UserDTO> GetUsersDTO()
        {
            var list = new List<UserDTO>();
            foreach (var user in _userRepository.Query())
            {
                list.Add(new UserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    NickName = user.NickName,
                    DateRegistration = user.DateRegistration,
                    City = user.City,
                    Password = user.Password,
                    TeamId = user.TeamId,
                    LastUrl = user.LastUrl,
                    Email = user.Email
                });
            }
            return list;
        }

        public User GetUserById(int id)
        {
            return _userRepository.Get(id);
        }

        public void SetUserGuid(string guid)
        {
            HttpContext.Current.Session["UserGuid"] = guid;
        }

        public void LogOffUser()
        {
            HttpContext.Current.Session["UserGuid"] = string.Empty;
        }

        public UserDTO GetCurrentUser()
        {
            try
            {
                var userGuid = HttpContext.Current.Session["UserGuid"].ToString();
                var user = GetUserDTOByGuid(userGuid);
                user.UserGuid = userGuid;
                HttpContext.Current.Session["isAdmin"] = user.NickName == "ipi";
                return user;
            }
            catch (Exception)
            {
                HttpContext.Current.Session["isAdmin"] = false;
                return new UserDTO
                    {
                        Name = "Guest",
                        NickName = "Guest",
                        UserGuid = "Guest"
                    };
            }
        }

        public User GetUserByLogin(string login)
        {
            return _userRepository.Query().FirstOrDefault(m => m.NickName == login);
        }

        public bool IsUserRegisterValid(UserDTO user)
        {
            return !_userRepository.Query().Any(m => m.Email == user.Email || m.NickName == user.NickName);
        }

        public bool IsUserlogOnValid(UserDTO user)
        {
            return _userRepository.Query().Any(m => m.Password == user.Password && m.NickName == user.NickName);
        }

        public User GetUserByGuid(string guid)
        {
            return _userRepository.Query().FirstOrDefault(m => m.UserGuid == guid);
        }

        public bool IsAdmin()
        {
            var isAdmin = GetCurrentUser().NickName == "ipi";
            HttpContext.Current.Session["isAdmin"] = isAdmin;
            return isAdmin;
        }
    }
}
