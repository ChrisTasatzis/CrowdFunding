using CrowdFunding.DTO;
using CrowdFunding.Models;

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

            if (user.Id != 0)
                return new Response<User>
                {
                    Data = null,
                    StatusCode = 51,
                    Description = "The given user had a non zero id."
                };

            if (user.FirstName == null || user.LastName == null ||
                user.Email == null || user.PasswordHash == null)
            {
                return new Response<User>
                {
                    Data = null,
                    StatusCode = 52,
                    Description = "The given user has null properties."
                };
            }

            if (_context.Users.Where(u => u.Email == user.Email).Any())
                return new Response<User>
                {
                    Data = null,
                    StatusCode = 53,
                    Description = "The given user Email is already taken."
                };

            _context.Users.Add(user);

            if (_context.SaveChanges() != 1)
                return new Response<User>
                {
                    Data = null,
                    StatusCode = 54,
                    Description = "Could not save changes."
                };

            return new Response<User>
            {
                Data = user,
                StatusCode = 0,
                Description = "User saved successfully."
            };
        }

        public Response<User> ReadUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return new Response<User>
                {
                    Data = null,
                    StatusCode = 50,
                    Description = "No user with this id exists."
                };

            return new Response<User>
            {
                Data = user,
                StatusCode = 0,
                Description = "User Found."
            };

        }

        public Response<User> UpdateUser(User user)
        {
            if (user == null)
                return new Response<User>
                {
                    Data = null,
                    StatusCode = 50,
                    Description = "No user provided."
                };

            if (user.Id == 0)
                return new Response<User>
                {
                    Data = null,
                    StatusCode = 51,
                    Description = "No user id provided."
                };

            var userDb = _context.Users.FirstOrDefault(u => u.Id == user.Id);

            if (userDb == null)
                return new Response<User>
                {
                    Data = null,
                    StatusCode = 52,
                    Description = "No user found with the id provided."
                };

            if (user.FirstName != null) userDb.FirstName = user.FirstName;
            if (user.LastName != null) userDb.LastName = user.LastName;
            if (user.Email != null) userDb.Email = user.Email;
            if (user.PasswordHash != null) userDb.PasswordHash = user.PasswordHash;

            if (_context.SaveChanges() != 1)
                return new Response<User>
                {
                    Data = null,
                    StatusCode = 53,
                    Description = "Could not save changes."
                };

            return new Response<User>
            {
                Data = userDb,
                StatusCode = 0,
                Description = "User successfully updated."
            };
        }

        public Response<bool> DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 50,
                    Description = "No user with this id exists."
                };

            _context.Users.Remove(user);

            if (_context.SaveChanges() != 1)
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 53,
                    Description = "Could not save changes."
                };

            return new Response<bool>
            {
                Data = true,
                StatusCode = 0,
                Description = "User deleted successfully."
            };
        }

      
    }
}
