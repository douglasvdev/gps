using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api")]
    [ApiController]
    public class LancamentosController : Controller
    {
        #region API - Listar
        /// <summary>
        /// Listar todas as Contas
        /// </summary>
        /// <param name="contexto"></param>
        /// <returns>Teste</returns>
        [HttpGet]
        [Route("lancamentos")]
        public async Task<IActionResult> getAllAsync([FromServices] Contexto contexto)
        {
            var lancamentos = await contexto.Lancamentos.Include(c => c.Contas).Include(j => j.Jogadores).AsNoTracking().ToListAsync();
            return lancamentos == null ? NotFound() : Ok(lancamentos);
        }

        #endregion

        #region API - Unico
        [HttpGet]
        [Route("lancamento/{id}")]
        public async Task<IActionResult> getByIdAsyn(
            [FromServices] Contexto contexto,
            [FromRoute] int id
            )
        {
            var lancamento = await contexto
                .Lancamentos.Include(c => c.Contas).Include(j => j.Jogadores).AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);

            return lancamento == null ? NotFound() : Ok(lancamento);
        }

        #endregion

        #region API - Inserir
        [HttpPost]
        [Route("lancamento")]
        public async Task<IActionResult> PostAsync(
            [FromServices] Contexto contexto,
            [FromBody] Lancamento lancamento
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await contexto.Lancamentos.AddAsync(lancamento);
                await contexto.SaveChangesAsync();
                return Created($"api/lancamento/{lancamento.Id}", lancamento);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region API - Editar
        [HttpPut]
        [Route("lancamento/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] Contexto contexto,
            [FromBody] Lancamento lancamento,
            [FromRoute] int id
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var l = await contexto.Lancamentos
                .FirstOrDefaultAsync(l => l.Id == id);
            if (l == null)
            {
                return NotFound("Conta não encontrada");
            }

            try
            {
                l.ContaId = lancamento.ContaId;
                l.Contas = lancamento.Contas;
                l.JogadorId = lancamento.JogadorId;
                l.Jogadores = lancamento.Jogadores;
                l.Valor = lancamento.Valor;
                l.ObsLancamento = lancamento.ObsLancamento;
                l.DtPrevisao = lancamento.DtPrevisao;
                l.DtBaixa = lancamento.DtBaixa;
                l.Inativo = lancamento.Inativo;

                contexto.Lancamentos.Update(l);
                await contexto.SaveChangesAsync();
                return Ok(l);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region API - Excluir (Desativado)
        /*[HttpDelete]
        [Route("lancamento/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] Contexto contexto,
            [FromRoute] int id
            )
        {
            var l = await contexto.Lancamentos.FirstOrDefaultAsync(l => l.Id == id);

            if (l == null)
            {
                return BadRequest("Conta não encontrada!");
            }

            try
            {
                contexto.Lancamentos.Remove(l);
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
        [Route("lancamento/{id}")]
        public async Task<IActionResult> InativarAsync(
            [FromServices] Contexto contexto,
            [FromRoute] int id
            )
        {
            var l = await contexto.Lancamentos.FirstOrDefaultAsync(l => l.Id == id);

            if (l == null)
            {
                return BadRequest("Conta não encontrada!");
            }
            l.Inativo = DateTime.Now;
            try
            {
                contexto.Lancamentos.Update(l);
                await contexto.SaveChangesAsync();

                return Ok(l);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}
