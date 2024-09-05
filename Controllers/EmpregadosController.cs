using FirstOne.Models;
using FirstOne.Reposotory.Inteface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpregadosController : ControllerBase
    {
        private readonly IEmpregadoRepository _empregadoRepository;
        private readonly IDepartamentoRepository _departamentoRepository;

        public EmpregadosController(IEmpregadoRepository empregadoRepository, IDepartamentoRepository departamentoRepository)
        {
            _empregadoRepository = empregadoRepository;
            _departamentoRepository = departamentoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Empregado>>> GetEmpregados()
        {
            try
            {
                return Ok(await _empregadoRepository.GetEmpregados());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter empregados");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Empregado>> GetEmpregado(int id)
        {
            try
            {
                var result = await _empregadoRepository.GetEmpregado(id);
                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter empregado");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Empregado>> AddEmpregado([FromBody] Empregado empregado)
        {
            try
            {
                if (empregado == null) return BadRequest();

                var createdEmpregado = await _empregadoRepository.AddEmpregado(empregado);

                return CreatedAtAction(nameof(GetEmpregado),
                    new { id = createdEmpregado.EmpId }, createdEmpregado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar empregado");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Empregado>> UpdateEmpregado([FromBody] Empregado empregado)
        {
            try
            {
                var empToUpdate = await _empregadoRepository.GetEmpregado(empregado.EmpId);

                if (empToUpdate == null) return NotFound($"Empregado com id {empregado.EmpId} não encontrado");

                return await _empregadoRepository.UpdateEmpregado(empregado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar empregado");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteEmpregado(int id)
        {
            try
            {
                var empToDelete = await _empregadoRepository.GetEmpregado(id);

                if (empToDelete == null) return NotFound($"Empregado com id {id} não encontrado");

                _empregadoRepository.DeleteEmpregado(id);

                return Ok($"Empregado com id {id} deletado");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar empregado");
            }
        }

        // Método para associar empregado a um departamento
        [HttpPut("{empId:int}/Departamento/{departamentoId:int}")]
        public async Task<ActionResult<Empregado>> AssociarEmpregadoADepartamento(int empId, int departamentoId)
        {
            try
            {
                var empregado = await _empregadoRepository.GetEmpregado(empId);
                if (empregado == null)
                    return NotFound($"Empregado com id {empId} não encontrado");

                var departamento = await _departamentoRepository.GetDepartamento(departamentoId);
                if (departamento == null)
                    return NotFound($"Departamento com id {departamentoId} não encontrado");

                empregado.DepartamentoId = departamentoId;
                var updatedEmpregado = await _empregadoRepository.UpdateEmpregado(empregado);

                return Ok(updatedEmpregado);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao associar empregado ao departamento");
            }
        }

        // Método para listar todos os empregados de um departamento
        [HttpGet("Departamento/{departamentoId:int}/Empregados")]
        public async Task<ActionResult<IEnumerable<Empregado>>> GetEmpregadosPorDepartamento(int departamentoId)
        {
            try
            {
                var departamento = await _departamentoRepository.GetDepartamento(departamentoId);
                if (departamento == null)
                    return NotFound($"Departamento com id {departamentoId} não encontrado");

                var empregados = await _empregadoRepository.GetEmpregadosPorDepartamento(departamentoId);

                return Ok(empregados);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter empregados do departamento");
            }
        }
    }
}