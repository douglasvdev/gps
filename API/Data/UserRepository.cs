using API.Models;

namespace API.Data
{
    public class UserRepository
    {
        public static Usuario Get(string username, string password)
        {
            var users = new List<Usuario>
            {
                new() { Id = 1, User = "batman", Senha = "batman", Nivel = "manager" },
                new() { Id = 2, User = "robin", Senha = "robin", Nivel = "employee" }

            };

            return users.FirstOrDefault(x => x.User == username && x.Senha == password);

        }


    }
}
