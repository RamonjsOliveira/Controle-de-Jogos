using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamesRente.Models
{
    public class Emprestimo
    {
        //[Key]
        public int Id { get; set; }

        [Display(Name ="Data do Emprestimo")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Datacadastro { get; set; }
        [Display(Name = "Data da Devolução")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataDevolucao { get; set; }
        public bool Excluido { get; set; }
        public bool Devolvido { get; set; }
        [Display(Name ="Amigo")]
        public int AmigoId { get; set; }
        [Display(Name ="Jogo")]
        public int JogoId { get; set; }
        public virtual Jogo Jogo { get; set; }
        public virtual Amigo Amigo { get; set; }
    }
}