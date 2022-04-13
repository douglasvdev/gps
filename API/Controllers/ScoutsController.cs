using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ScoutsController : Controller
    {
        #region API - Listar
        /// <summary>
        /// Listar todas as Contas
        /// </summary>
        /// <param name="contexto"></param>
        /// <returns>Teste</returns>
        [HttpGet]
        [Route("scouts")]
        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto)
        {
            var scouts = await contexto.Scouts.Include(p => p.Parametros).Include(j => j.Jogadores)
                .Select(s => new { s.Id, s.DtPartida, s.JogadorId, s.Jogadores, s.Presente, s.ParametroId, s.Parametros, s.Gol, s.Assistencia, s.Time, s.Inativo})
                .AsNoTracking().ToListAsync();
            return scouts == null ? NotFound() : Ok(scouts);
        }

        #endregion

        #region API - Unico
        [HttpGet]
        [Route("scout/{id}")]
        public async Task<IActionResult> getByIdAsyn(
            [FromServices] Contexto contexto,
            [FromRoute] int id
            )
        {
            var scout = await contexto
                .Scouts.Include(p => p.Parametros).Include(j => j.Jogadores).AsNoTracking()
                .Select(s => new { s.Id, s.DtPartida, s.JogadorId, s.Jogadores, s.Presente, s.ParametroId, s.Parametros, s.Gol, s.Assistencia, s.Time, s.Inativo })
                .FirstOrDefaultAsync(l => l.Id == id);

            return scout == null ? NotFound() : Ok(scout);
        }

        #endregion

        #region API - Inserir
        [HttpPost]
        [Route("scout")]
        public async Task<IActionResult> PostAsync(
            [FromServices] Contexto contexto,
            [FromBody] Scout scout
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await contexto.Scouts.AddAsync(scout);
                await contexto.SaveChangesAsync();
                return Created($"api/scout/{scout.Id}", scout);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region API - Editar
        [HttpPut]
        [Route("scout/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] Contexto contexto,
            [FromBody] Scout scout,
            [FromRoute] int id
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var s = await contexto.Scouts
                .FirstOrDefaultAsync(s => s.Id == id);
            if (s == null)
            {
                return NotFound("Scout não encontrado");
            }

            try
            {
                s.DtPartida = scout.DtPartida;
                s.JogadorId = scout.JogadorId;
                s.Jogadores = scout.Jogadores;
                s.Presente = scout.Presente;
                s.ParametroId = scout.ParametroId;
                s.Parametros = scout.Parametros;
                s.Gol = scout.Gol;
                s.Assistencia = scout.Assistencia;
                s.ObsScout = scout.ObsScout;
                s.Time = scout.Time;
                s.Inativo = scout.Inativo;

                contexto.Scouts.Update(s);
                await contexto.SaveChangesAsync();
                return Ok(s);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region API - Excluir (Desativado)
        /*[HttpDelete]
        [Route("scout/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] Contexto contexto,
            [FromRoute] int id
            )
        {
            var l = await contexto.Scouts.FirstOrDefaultAsync(l => l.Id == id);

            if (l == null)
            {
                return BadRequest("Scout não encontrado!");
            }

            try
            {
                contexto.Scouts.Remove(l);
                await contexto.SaveChangesAsync();

                return Ok(l);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }*/
        #endregion

        #region API - Excluir Novo
        [HttpDelete]
        [Route("scout/{id}")]
        public async Task<IActionResult> InativarAsync(
            [FromServices] Contexto contexto,
            [FromRoute] int id
            )
        {
            var s = await contexto.Scouts.FirstOrDefaultAsync(s => s.Id == id);

            if (s == null)
            {
                return BadRequest("Scout não encontrado!");
            }
            s.Inativo = DateTime.Now;
            try
            {
                contexto.Scouts.Update(s);
                await contexto.SaveChangesAsync();

                return Ok(s);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
