namespace API.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Senha { get; set; }
        public string? Nivel { get; set; }
    }
}
