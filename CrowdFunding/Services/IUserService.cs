using CrowdFunding.DTO;
using CrowdFunding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Services
{
    public interface IUserService
    {
        Response<User> CreateUser(User user);
        Response<User> ReadUser(int id);
        Response<User> ReadCompleteUser(int id);
        Response<User> UpdateUser(User user);
        Response<bool> DeleteUser(int id);

    }
}
