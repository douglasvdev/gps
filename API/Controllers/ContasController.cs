using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore; // Usando System.Data.Entity dava -> The source IQueryable doesn't implement IDbAsyncEnumerable<API.Models.Conta>

namespace API.Controllers
{
    //[Route("api/[controller]")]
    [Route("api")]
    [ApiController]
    public class ContasController : ControllerBase
    {
        #region API - Listar
        /// <summary>
        /// oBTER TODOS OS USUÁRIOS
        /// </summary>
        /// <param name="contexto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("contas")]
        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto)
        {
            var contas = await contexto.Contas.AsNoTracking().ToListAsync();
            return contas == null ? NotFound() : Ok(contas);
        }

        #endregion

        #region API - Unico
        [HttpGet]
        [Route("contas/{id}")]
        public async Task<IActionResult> getByIdAsyn(
            [FromServices] Contexto contexto,
            [FromRoute] int id
            )
        {
            var conta = await contexto
                .Contas.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return conta == null ? NotFound() : Ok(conta);
        }

        #endregion

        #region API - Inserir
        [HttpPost]
        [Route("contas")]
        public async Task<IActionResult> PostAsync(
            [FromServices] Contexto contexto,
            [FromBody] Conta conta
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await contexto.Contas.AddAsync(conta);
                await contexto.SaveChangesAsync();
                return Created($"api/contas/{conta.Id}", conta);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region API - Editar
        [HttpPut]
        [Route("contas/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] Contexto contexto,
            [FromBody] Conta conta,
            [FromRoute] int id
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var c = await contexto.Contas
                .FirstOrDefaultAsync(c => c.Id == id);
            if (c == null)
            {
                return NotFound("Conta não encontrada");
            }

            try
            {
                c.NomeConta = conta.NomeConta;
                c.Tipo = conta.Tipo;
                c.ObsConta = conta.ObsConta;
                c.Inativo = conta.Inativo;

                contexto.Contas.Update(c);
                await contexto.SaveChangesAsync();
                return Ok(c);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region API - Excluir
        [HttpDelete]
        [Route("contas/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] Contexto contexto,
            [FromRoute] int id
            )
        {
            var c = await contexto.Contas.FirstOrDefaultAsync(c => c.Id == id);

            if (c == null)
            {
                return BadRequest("Conta não encontrada!");
            }

            try
            {
                contexto.Contas.Remove(c);
                await contexto.SaveChangesAsync();

                return Ok(c);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
