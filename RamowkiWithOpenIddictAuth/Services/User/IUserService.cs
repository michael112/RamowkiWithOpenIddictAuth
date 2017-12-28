using System.Collections.Generic;

namespace RamowkiWithOpenIddictAuth.Services.User
{
    public interface IUserService
    {
        IEnumerable<RamowkiWithOpenIddictAuth.Models.Authentication.User> ReadUserList();
        RamowkiWithOpenIddictAuth.Models.Authentication.User FindUserById(string id);
        RamowkiWithOpenIddictAuth.Models.Authentication.User FindUserByUserName(string username);
        /* implementacja pozostałych metod nastąpi w terminie późniejszym:
        void CreateUser(UserJson user);
        void UpdateUser(string id, UserJson user);
        void DeleteUser(string id);
        */
    }
}