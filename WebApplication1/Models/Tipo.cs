using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamesRente.Models
{
    public enum Tipo
    {
        
        [Display(Name ="RPG")]
        RPG,
        [Display(Name ="Ação")]
        Acao,
        [Display(Name = "Arcade")]
        Arcade,
        [Display(Name = "Simulação")]
        Simulacao
    }
}