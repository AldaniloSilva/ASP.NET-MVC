using System;
using System.ComponentModel.DataAnnotations;

namespace CadastroFerramentas.Models
{
    public class FerramentasViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int FabricanteId { get; set; }       
    }       
}
