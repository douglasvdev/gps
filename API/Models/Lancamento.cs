using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Lancamento
    {
        #region Atributos
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [Display(Name = "Conta")]
        public int ContaId { get; set; }
        public virtual Conta Contas { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [Display(Name = "Jogador")]
        public int JogadorId { get; set; }
        public virtual Jogador Jogadores { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Valor deve ser maior que 0")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [ValidateNever]
        public decimal Valor { get; set; }

        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres")]
        [Display(Name = "Observação")]
        public string? ObsLancamento { get; set; }

        [Required(ErrorMessage = "Obrigatório para definir mês")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] //Verificar forma de exibir data formato brasileiro mas passar em formato USA para Edit
        [Display(Name = "Data Previsão")]
        public DateTime DtPrevisao { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Baixa")]
        public DateTime? DtBaixa { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Inativo")]
        public DateTime? Inativo { get; set; }

        #endregion
    }
}
