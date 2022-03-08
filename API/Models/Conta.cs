using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Conta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [Display(Name = "Conta")]
        public string NomeConta { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [MaxLength(1)]
        public string Tipo { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "Observação")]
        public string? ObsConta { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Inativo")]
        public DateTime? Inativo { get; set; }
    }
}
