using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Scout
    {
        #region Atributos
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data da partida")]
        public DateTime DtPartida { get; set; }
        //public virtual Jogador fk_jogador { get; set; }

        public int JogadorId { get; set; }
        public virtual Jogador Jogadores { get; set; }

        [Display(Name = "Presença")]
        public bool Presente { get; set; }
        //public virtual Parametro fk_parametro { get; set; }

        [Display(Name = "Resultado")]
        public int ParametroId { get; set; }
        public virtual Parametro Parametros { get; set; }
        //public int Ponto { get; set; }
        
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        public int? Gol { get; set; }
        public int? Assistencia { get; set; }
        public string? ObsScout { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Inativo")]
        public DateTime? Inativo { get; set; }
        #endregion


        #region TIME A

        [NotMapped]
        [Required(ErrorMessage = "Obrigatorio")]
        [Display(Name = "Jogador A1")]
        public int JogadorIdA1 { get; set; }
        //public virtual Jogador JogadoresA1 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols A1")]
        public int? GolsA1 { get; set; }

        //A2
        [NotMapped]
        [Display(Name = "Jogador A2")]
        public int? JogadorIdA2 { get; set; }
        //public virtual Jogador JogadoresA2 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols A2")]
        public int? GolsA2 { get; set; }

        //A3
        [NotMapped]
        [Display(Name = "Jogador A3")]
        public int? JogadorIdA3 { get; set; }
        //public virtual Jogador JogadoresA3 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols A3")]
        public int? GolsA3 { get; set; }

        //A4
        [NotMapped]
        [Display(Name = "Jogador A4")]
        public int? JogadorIdA4 { get; set; }
        //public virtual Jogador JogadoresA4 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols A4")]
        public int? GolsA4 { get; set; }

        //A5
        [NotMapped]
        [Display(Name = "Jogador A5")]
        public int? JogadorIdA5 { get; set; }
        //public virtual Jogador JogadoresA5 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols A5")]
        public int? GolsA5 { get; set; }

        //A6
        [NotMapped]
        [Display(Name = "Jogador A6")]
        public int? JogadorIdA6 { get; set; }
        //public virtual Jogador JogadoresA6 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols A6")]
        public int? GolsA6 { get; set; }

        //A7
        [NotMapped]
        [Display(Name = "Jogador A7")]
        public int? JogadorIdA7 { get; set; }
        //public virtual Jogador JogadoresA7 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols A7")]
        public int? GolsA7 { get; set; }

        //A8
        [NotMapped]
        [Display(Name = "Jogador A8")]
        public int? JogadorIdA8 { get; set; }
        //public virtual Jogador JogadoresA8 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols A8")]
        public int? GolsA8 { get; set; }

        //A9
        [NotMapped]
        [Display(Name = "Jogador A9")]
        public int? JogadorIdA9 { get; set; }
        //public virtual Jogador JogadoresA9 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols A9")]
        public int? GolsA9 { get; set; }

        //A10
        [NotMapped]
        [Display(Name = "Jogador A10")]
        public int? JogadorIdA10 { get; set; }
        //public virtual Jogador JogadoresA10 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols A10")]
        public int? GolsA10 { get; set; }

        #endregion

        #region TIME B
        //B1
        [NotMapped]
        [Required(ErrorMessage = "Obrigatorio")]
        [Display(Name = "Jogador B1")]
        public int JogadorIdB1 { get; set; }
        //public virtual Jogador JogadoresB1 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols B1")]
        public int? GolsB1 { get; set; }

        //B2
        [NotMapped]
        [Display(Name = "Jogador B2")]
        public int? JogadorIdB2 { get; set; }
        //public virtual Jogador JogadoresB2 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols B2")]
        public int? GolsB2 { get; set; }

        //B3
        [NotMapped]
        [Display(Name = "Jogador B3")]
        public int? JogadorIdB3 { get; set; }
        //public virtual Jogador JogadoresB3 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols B3")]
        public int? GolsB3 { get; set; }

        //B4
        [NotMapped]
        [Display(Name = "Jogador B4")]
        public int? JogadorIdB4 { get; set; }
        //public virtual Jogador JogadoresB4 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols B4")]
        public int? GolsB4 { get; set; }

        //B5
        [NotMapped]
        [Display(Name = "Jogador B5")]
        public int? JogadorIdB5 { get; set; }
        //public virtual Jogador JogadoresB5 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols B5")]
        public int? GolsB5 { get; set; }

        //B6
        [NotMapped]
        [Display(Name = "Jogador B6")]
        public int? JogadorIdB6 { get; set; }
        //public virtual Jogador JogadoresB6 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols B6")]
        public int? GolsB6 { get; set; }

        //B7
        [NotMapped]
        [Display(Name = "Jogador B7")]
        public int? JogadorIdB7 { get; set; }
        //public virtual Jogador JogadoresB7 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols B7")]
        public int? GolsB7 { get; set; }

        //B8
        [NotMapped]
        [Display(Name = "Jogador B8")]
        public int? JogadorIdB8 { get; set; }
        //public virtual Jogador JogadoresB8 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols B8")]
        public int? GolsB8 { get; set; }

        //B9
        [NotMapped]
        [Display(Name = "Jogador B9")]
        public int? JogadorIdB9 { get; set; }
        //public virtual Jogador JogadoresB9 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols B9")]
        public int? GolsB9 { get; set; }

        //B10
        [NotMapped]
        [Display(Name = "Jogador B10")]
        public int? JogadorIdB10 { get; set; }
        //public virtual Jogador JogadoresB10 { get; set; }
        [NotMapped]
        [Range(0, 99, ErrorMessage = "Deve ser maior que 0")]
        [Display(Name = "Gols B10")]
        public int? GolsB10 { get; set; }
        #endregion
    }
}
