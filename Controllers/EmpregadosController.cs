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

        public EmpregadosController(IEmpregadoRepository empregado)
        {
            _empregadoRepository = empregado;
        }

        [HttpGet]
        public async Task<ActionResult<Empregado>> GetEmpregado()
        {
            try
            {
                return Ok(await _empregadoRepository.GetEmpregados());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter empregados");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Empregado>> AddEmpregados([FromBody]Empregado empregado)
        {
            try
            {
                if (empregado == null) return BadRequest();

                var createEmp = await _empregadoRepository.AddEmpregado(empregado);

                return CreatedAtAction(nameof(GetEmpregado),
                    new { id = createEmp.EmpId }, createEmp);

            }
            catch (Exception) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar");
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
    }
}
