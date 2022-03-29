using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class ParametrosController : ControllerBase
    {
        #region API - Listar
        /// <summary>
        /// Listar todas os Parametros
        /// </summary>
        /// <param name="contexto"></param>
        /// <returns>Teste</returns>
        [HttpGet]
        [Route("parametros")] //http://localhost:5172/api/parametros
        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto)
        {
            var parametros = await contexto.Parametros.AsNoTracking().ToListAsync();
            return parametros == null ? NotFound() : Ok(parametros);
        }

        #endregion

        #region API - Unico
        [HttpGet]
        [Route("parametro/{id}")]
        public async Task<IActionResult> getByIdAsyn(
            [FromServices] Contexto contexto,
            [FromRoute] int id
            )
        {
            var parametro = await contexto
                .Parametros.AsNoTracking()
                .FirstOrDefaultAsync(j => j.Id == id);

            return parametro == null ? NotFound() : Ok(parametro);
        }

        #endregion

        #region API - Inserir
        [HttpPost]
        [Route("parametro")]
        public async Task<IActionResult> PostAsync(
            [FromServices] Contexto contexto,
            [FromBody] Parametro parametro
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                parametro.CodParametro = parametro.CodParametro.ToUpper();
                await contexto.Parametros.AddAsync(parametro);
                await contexto.SaveChangesAsync();
                return Created($"api/parametro/{parametro.Id}", parametro);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region API - Editar
        [HttpPut]
        [Route("parametro/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] Contexto contexto,
            [FromBody] Parametro parametro,
            [FromRoute] int id
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var p = await contexto.Parametros
                .FirstOrDefaultAsync(p => p.Id == id);
            if (p == null)
            {
                return NotFound("Parametro não encontrado");
            }

            try
            {
                p.DescParametro = parametro.DescParametro;
                p.CodParametro = parametro.CodParametro.ToUpper();
                p.Ponto = parametro.Ponto;
                p.Inativo = parametro.Inativo;

                contexto.Parametros.Update(p);
                await contexto.SaveChangesAsync();
                return Ok(p);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region API - Excluir (Desativado)
        /*[HttpDelete]
        [Route("parametro/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] Contexto contexto,
            [FromRoute] int id
            )
        {
            var j = await contexto.Parametros.FirstOrDefaultAsync(j => j.Id == id);

            if (j == null)
            {
                return BadRequest("Parâmetro não encontrada!");
            }

            try
            {
                contexto.Parametros.Remove(j);
                await contexto.SaveChangesAsync();

                return Ok(j);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }*/
        #endregion

        #region API - Excluir
        [HttpDelete]
        [Route("parametro/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] Contexto contexto,
            [FromRoute] int id
            )
        {
            var p = await contexto.Parametros.FirstOrDefaultAsync(p => p.Id == id);

            if (p == null)
            {
                return BadRequest("Parâmetro não encontrada!");
            }

            p.Inativo = DateTime.Now;

            try
            {
                contexto.Parametros.Update(p);
                await contexto.SaveChangesAsync();

                return Ok(p);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
