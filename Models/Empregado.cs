using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FirstOne.Models
{
    public class Empregado
    {
        [Key]
        public int EmpId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Sobrenome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Nascimento { get; set; }
        public Genero Genero { get; set; }
        public int DepartamentoId { get; set; }
        public string FotoUrl { get; set; } = string.Empty;
    }
}
