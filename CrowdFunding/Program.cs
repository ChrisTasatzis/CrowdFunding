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
                Password = "Password1"
            };

            var response = userService.CreateUser(user);

            Console.WriteLine(response.Description);

            response = userService.CreateUser(user);

            Console.WriteLine(response.Description);
        }
    }
}
