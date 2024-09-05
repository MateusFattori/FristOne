using FirstOne.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Reposotory.Inteface
{
    public interface IDepartamentoRepository
    {
        Task<IEnumerable<Departamento>> GetDepartamentos();
        Task<Departamento> GetDepartamento(int departamentoId);
        Task<Departamento> AddDepartamento(Departamento departamento);
        Task<Departamento> UpdateDepartamento(Departamento departamento);
        void DeleteDepartamento(int departamentoId);
    }
}