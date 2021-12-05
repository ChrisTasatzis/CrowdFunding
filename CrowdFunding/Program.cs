using CrowdFunding.Models;
using CrowdFunding.Services;

namespace CrowdFunding
{
    public class Program
    {

        public static void Main(string[] args)
        {
            UserTest();
        }

        public static void UserTest()
        {
            using var context = new CFContext();

            var userService = new UserService(context);

            var user = new User()
            {
                FirstName = "FirstName1",
                LastName = "LastName1",
                Email = "Email1",
                PasswordHash = "Password1"
            };

            // Create user successfully
            var response = userService.CreateUser(user);
            Console.WriteLine(response.Description);

            // Invalid, email not unique
            var response2 = userService.CreateUser(user);
            Console.WriteLine(response2.Description);

            // Delete user successfully
            var response3 = userService.DeleteUser(response.Data.Id);
            Console.WriteLine(response3.Description);

            // No user with this id
            var response4 = userService.DeleteUser(response.Data.Id);
            Console.WriteLine(response4.Description);

            // No user
            var response5 = userService.UpdateUser(null);
            Console.WriteLine(response5.Description);

            // No user id
            user.Id = 0;
            var response6 = userService.UpdateUser(user);
            Console.WriteLine(response6.Description);

            // No user with this id
            var response7 = userService.UpdateUser(response.Data);
            Console.WriteLine(response7.Description);

            // Successfull Update
            var response8 = userService.CreateUser(user);
            response8.Data.FirstName = "OtherName";
            response8.Data.LastName = "Sth else";
            var response9 = userService.UpdateUser(response8.Data);
            Console.WriteLine(response9.Description);
            var response10 = userService.ReadUser(response9.Data.Id);
            Console.WriteLine($"{response10.Data.FirstName} {response10.Data.LastName}");
            userService.DeleteUser(response10.Data.Id);

        }
    }
}
