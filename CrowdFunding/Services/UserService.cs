using CrowdFunding.DTO;
using CrowdFunding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdFunding.Services
{
    public class UserService : IUserService
    {
        private readonly CFContext _context;

        public UserService(CFContext context)
        {
            _context = context;
        }
        public Response<User> CreateUser(User user)
        {
            if (user == null)
                return new Response<User>
                {
                    Data = null,
                    StatusCode = 50,
                    Description = "The given user was null."
                };

            if (user.FirstName == null || user.LastName == null || 
                user.Email == null || user.Password == null)
            {
                return new Response<User>
                {
                    Data = null,
                    StatusCode = 51,
                    Description = "The given user has null properties."
                };
            }

            if (_context.Users.Where(u => u.Email == user.Email).Any())
                return new Response<User>
                {
                    Data = null,
                    StatusCode = 52,
                    Description = "The given user Email is already taken."
                };

            _context.Users.Add(user);

            if (_context.SaveChanges() != 1)
                return new Response<User>
                {
                    Data = null,
                    StatusCode = 53,
                    Description = "Could not save changes."
                };

            return new Response<User>
            {
                Data = user,
                StatusCode = 0,
                Description = "User saved successfully."
            };
        }
    }
}
