using System.Collections.Generic;
using Psub.DTO.Entities;
using Psub.Domain.Entities;

namespace Psub.DataService.Abstract
{
    public interface IUserService
    {
        int SaveOrUpdate(UserDTO user);
        User GetUserByGuid(string guid);
        User GetUserById(int id);
        List<UserDTO> GetUsersDTO();
        void LogOffUser();
        void SetUserGuid(string guid);
        UserDTO GetCurrentUser();
        User GetUserByLogin(string login);
        bool IsUserRegisterValid(UserDTO user);
        bool IsUserlogOnValid(UserDTO user);
        bool IsAdmin();
    }
}
