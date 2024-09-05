using FirstOne.Models;
using FirstOne.Reposotory.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IEmpregadoRepository _empregadoRepository;

        public DepartamentosController(IDepartamentoRepository departamentoRepository, IEmpregadoRepository empregadoRepository)
        {
            _departamentoRepository = departamentoRepository;
            _empregadoRepository = empregadoRepository;
        }

        // GET: api/departamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamentos()
        {
            try
            {
                return Ok(await _departamentoRepository.GetDepartamentos());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter departamentos");
            }
        }

        // GET: api/departamentos/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Departamento>> GetDepartamento(int id)
        {
            try
            {
                var departamento = await _departamentoRepository.GetDepartamento(id);

                if (departamento == null) return NotFound($"Departamento com id {id} não encontrado");

                return Ok(departamento);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter departamento");
            }
        }

        // POST: api/departamentos
        [HttpPost]
        public async Task<ActionResult<Departamento>> AddDepartamento(Departamento departamento)
        {
            try
            {
                if (departamento == null) return BadRequest("Dados inválidos");

                var createdDepartamento = await _departamentoRepository.AddDepartamento(departamento);

                return CreatedAtAction(nameof(GetDepartamento), new { id = createdDepartamento.DepartamentoId }, createdDepartamento);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar departamento");
            }
        }

        // PUT: api/departamentos/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Departamento>> UpdateDepartamento(int id, Departamento departamento)
        {
            try
            {
                if (id != departamento.DepartamentoId) return BadRequest("ID do departamento não corresponde");

                var departamentoToUpdate = await _departamentoRepository.GetDepartamento(id);

                if (departamentoToUpdate == null) return NotFound($"Departamento com id {id} não encontrado");

                return await _departamentoRepository.UpdateDepartamento(departamento);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar departamento");
            }
        }

        // DELETE: api/departamentos/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteDepartamento(int id)
        {
            try
            {
                var departamentoToDelete = await _departamentoRepository.GetDepartamento(id);

                if (departamentoToDelete == null) return NotFound($"Departamento com id {id} não encontrado");

                _departamentoRepository.DeleteDepartamento(id);

                return Ok($"Departamento com id {id} deletado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar departamento");
            }
        }

        // GET: api/departamentos/{id}/empregados
        [HttpGet("{id:int}/empregados")]
        public async Task<ActionResult<IEnumerable<Empregado>>> GetEmpregadosPorDepartamento(int id)
        {
            try
            {
                var departamento = await _departamentoRepository.GetDepartamento(id);

                if (departamento == null) return NotFound($"Departamento com id {id} não encontrado");

                var empregados = await _empregadoRepository.GetEmpregados();

                var empregadosNoDepartamento = empregados.Where(e => e.DepartamentoId == id);

                return Ok(empregadosNoDepartamento);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter empregados do departamento");
            }
        }

        // POST: api/departamentos/{departamentoId}/associarempregado/{empregadoId}
        [HttpPost("{departamentoId:int}/associarempregado/{empregadoId:int}")]
        public async Task<ActionResult> AssociarEmpregadoAoDepartamento(int departamentoId, int empregadoId)
        {
            try
            {
                var departamento = await _departamentoRepository.GetDepartamento(departamentoId);
                var empregado = await _empregadoRepository.GetEmpregado(empregadoId);

                if (departamento == null) return NotFound($"Departamento com id {departamentoId} não encontrado");
                if (empregado == null) return NotFound($"Empregado com id {empregadoId} não encontrado");

                empregado.DepartamentoId = departamentoId;
                await _empregadoRepository.UpdateEmpregado(empregado);

                return Ok($"Empregado com id {empregadoId} foi associado ao departamento com id {departamentoId}");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao associar empregado ao departamento");
            }
        }
    }
}
