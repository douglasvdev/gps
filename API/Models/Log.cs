using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Usuario { get; set; }

        [MaxLength(100)]
        public string Acao { get; set; }

        [MaxLength(100)]
        public string Tabela { get; set; }

        [MaxLength(1000)]
        public string? Dados { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Quando { get; set; }
    }
}
