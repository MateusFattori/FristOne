using FirstOne.Models;

namespace FirstOne.Reposotory.Inteface
{
    public interface IDepartamentoRepository
    {
        IEnumerable<Departamento> GetDepartamentos();
        Departamento GetDepartamento(int id);
    }
}
