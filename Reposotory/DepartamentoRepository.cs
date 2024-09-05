using FirstOne.Data;
using FirstOne.Models;
using FirstOne.Reposotory.Inteface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstOne.Reposotory
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly dbContext _dbContext;

        public DepartamentoRepository(dbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Departamento> AddDepartamento(Departamento departamento)
        {
            var result = await _dbContext.Departamentos.AddAsync(departamento);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async void DeleteDepartamento(int departamentoId)
        {
            var result = await _dbContext.Departamentos.FirstOrDefaultAsync(
                x => x.DepartamentoId == departamentoId);
            if (result != null)
            {
                _dbContext.Departamentos.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Departamento> GetDepartamento(int departamentoId)
        {
            return await _dbContext.Departamentos.FirstOrDefaultAsync(
                x => x.DepartamentoId == departamentoId);
        }

        public async Task<IEnumerable<Departamento>> GetDepartamentos()
        {
            return await _dbContext.Departamentos.ToListAsync();
        }

        public async Task<Departamento> UpdateDepartamento(Departamento departamento)
        {
            var result = await _dbContext.Departamentos.FirstOrDefaultAsync(
                x => x.DepartamentoId == departamento.DepartamentoId);

            if (result != null)
            {
                result.NomeDepartamento = departamento.NomeDepartamento;
                await _dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }
}