using FirstOne.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Reposotory.Inteface
{
    public interface IEmpregadoRepository
    {
        Task<IEnumerable<Empregado>> GetEmpregados();
        Task<Empregado> GetEmpregado(int empId);
        Task<Empregado> AddEmpregado(Empregado empregado);
        Task<Empregado> UpdateEmpregado(Empregado empregado);
        void DeleteEmpregado(int empId);

        // Novo mÃ©todo para obter empregados por departamento
        Task<IEnumerable<Empregado>> GetEmpregadosPorDepartamento(int departamentoId);
    }
}