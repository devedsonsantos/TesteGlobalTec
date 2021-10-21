using TesteGlobalTec.Models;
using System.Linq;
using System.Collections.Generic;

namespace TesteGlobalTec.Repositories
{
    public class UserRepository
    {
        public static User GetUser(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User {Id = 1, UserName = "edson", Password = "123", Role = "manager"});
            users.Add(new User {Id = 2, UserName = "santos", Password = "123", Role = "employee"});
            
            return users.Where(x => x.UserName.ToLower() == username.ToLower() && x.Password == x.Password).FirstOrDefault();
        }
    }
}