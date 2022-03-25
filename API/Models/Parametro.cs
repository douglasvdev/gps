using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Parametro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatorio")]
        [MaxLength(80, ErrorMessage = "Máximo 80 caracteres")]
        [Display(Name = "Parâmetro")]
        public string DescParametro { get; set; }

        [Index("CodParametro", IsUnique = true)]
        [MaxLength(2)]
        [Display(Name = "Código")]
        public string CodParametro { get; set; }

        [Required(ErrorMessage = "Obrigatorio")]
        [Display(Name = "Pontos")]
        public int Ponto { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Inativo { get; set; }
    }
}
