using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RamowkiWithOpenIddictAuth.Services.User
{
    public class UserService : IUserService
    {
        private ApplicationDbContext dbContext;

        public IEnumerable<RamowkiWithOpenIddictAuth.Models.Authentication.User> ReadUserList()
        {
            return this.dbContext.Users.ToList();
        }

        public RamowkiWithOpenIddictAuth.Models.Authentication.User FindUserById(string id)
        {
            return this.dbContext.Users.Include(u => u.RoleJoins).ThenInclude(j => j.Role).Single(u => u.Id.ToString() == id);
        }

        public RamowkiWithOpenIddictAuth.Models.Authentication.User FindUserByUserName(string username)
        {
            return this.dbContext.Users.Include(u => u.RoleJoins).ThenInclude(j => j.Role).Single(u => u.UserName.ToString() == username);
        }

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}