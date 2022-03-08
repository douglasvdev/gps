using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Jogador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Jogador")]
        public string NomeJogador { get; set; }

        [Required(ErrorMessage = "Obrigatorio")]
        [MaxLength(1, ErrorMessage = "S, N ou E")]
        public string Mensalista { get; set; }

        [StringLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "Observação")]
        public string? ObsJogador { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Inativo { get; set; }

    }
}
