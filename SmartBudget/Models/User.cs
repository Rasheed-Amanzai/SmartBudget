namespace SmartBudget.Models
{
    public class User
    {
        public static List<User> Users = new List<User>();

        public string Username { get; set; }
        public string Password { get; set; }

        public static bool ValidateLogin(string username, string password)
        {
            return Users.Any(u => u.Username == username && u.Password == password);
        }

        public static void RegisterUser(string username, string password)
        {
            Users.Add(new User { Username = username, Password = password });
        }
    }
}