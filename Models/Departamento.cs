using System.ComponentModel.DataAnnotations;

namespace FirstOne.Models
{
    public class Departamento
    {
        [Key]
        public int DepartamentoId { get; set; }
        public string NomeDepartamento { get; set; }
    }
}
