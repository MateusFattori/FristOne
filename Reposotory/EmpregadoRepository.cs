using FirstOne.Data;
using FirstOne.Models;
using FirstOne.Reposotory.Inteface;
using Microsoft.EntityFrameworkCore;

namespace FirstOne.Reposotory
{
    public class EmpregadoRepository : IEmpregadoRepository
    {
        private readonly dbContext dbContext;

        public EmpregadoRepository(dbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Empregado> AddEmpregado(Empregado empregado)
        {
            var result = await dbContext.Empregados.AddAsync(empregado);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeleteEmpregado(int empId)
        {
            var result = await dbContext.Empregados.FirstOrDefaultAsync(
                x => x.EmpId == empId);
            if(result != null)
            {
                dbContext.Empregados.Remove(result);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Empregado> GetEmpregado(int empId)
        {
            return await dbContext.Empregados.FirstOrDefaultAsync(
                x => x.EmpId == empId);
        }

        public async Task<IEnumerable<Empregado>> GetEmpregados()
        {
            return await dbContext.Empregados.ToListAsync();
        }

        public async Task<Empregado> UpdateEmpregado(Empregado empregado)
        {
            var result = await dbContext.Empregados.FirstOrDefaultAsync(
                x => x.EmpId == empregado.EmpId);

            if(result != null)
            {
                result.Email = empregado.Email;
                result.Nome = empregado.Nome;
                result.Sobrenome = empregado.Sobrenome;
                result.Nascimento = empregado.Nascimento;
                result.Genero = empregado.Genero;
                result.DepartamentoId = empregado.DepartamentoId;
                result.FotoUrl = empregado.FotoUrl;

                await dbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
