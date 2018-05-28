using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamesRente.Models
{
    public class Amigo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [Display(Name= "Data de Cadastro")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; }
        public bool Excluido { get; set; }
        [Display(Name ="Liberado para Emprestimos?")]
        public bool Liberado { get; set; }

    }
}