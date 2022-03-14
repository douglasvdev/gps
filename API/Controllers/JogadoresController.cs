using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class JogadoresController : Controller
    {
        #region API - Listar
        /// <summary>
        /// Listar todas as Contas
        /// </summary>
        /// <param name="contexto"></param>
        /// <returns>Teste</returns>
        [HttpGet]
        [Route("jogadores")] //http://localhost:5172/api/jogadores
        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto)
        {
            var jogadores = await contexto.Jogadores.AsNoTracking().ToListAsync();
            return jogadores == null ? NotFound() : Ok(jogadores);
        }

        #endregion

        #region API - Unico
        [HttpGet]
        [Route("jogador/{id}")]
        public async Task<IActionResult> getByIdAsyn(
            [FromServices] Contexto contexto,
            [FromRoute] int id
            )
        {
            var jogador = await contexto
                .Jogadores.AsNoTracking()
                .FirstOrDefaultAsync(j => j.Id == id);

            return jogador == null ? NotFound() : Ok(jogador);
        }

        #endregion

        #region API - Inserir
        [HttpPost]
        [Route("jogador")]
        public async Task<IActionResult> PostAsync(
            [FromServices] Contexto contexto,
            [FromBody] Jogador jogador
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await contexto.Jogadores.AddAsync(jogador);
                await contexto.SaveChangesAsync();
                return Created($"api/jogador/{jogador.Id}", jogador);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region API - Editar
        [HttpPut]
        [Route("jogador/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] Contexto contexto,
            [FromBody] Jogador jogador,
            [FromRoute] int id
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var j = await contexto.Jogadores
                .FirstOrDefaultAsync(j => j.Id == id);
            if (j == null)
            {
                return NotFound("Jogador não encontrado");
            }

            try
            {
                j.NomeJogador = jogador.NomeJogador;
                j.Mensalista = jogador.Mensalista;
                j.ObsJogador = jogador.ObsJogador;
                j.Inativo = jogador.Inativo;

                contexto.Jogadores.Update(j);
                await contexto.SaveChangesAsync();
                return Ok(j);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region API - Excluir
        [HttpDelete]
        [Route("jogador/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] Contexto contexto,
            [FromRoute] int id
            )
        {
            var j = await contexto.Jogadores.FirstOrDefaultAsync(j => j.Id == id);

            if (j == null)
            {
                return BadRequest("Conta não encontrada!");
            }

            try
            {
                contexto.Jogadores.Remove(j);
                await contexto.SaveChangesAsync();

                return Ok(j);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
