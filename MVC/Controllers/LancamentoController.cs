#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace MVC.Controllers
{
    public class LancamentoController : Controller
    {
        #region CONSTRUTOR
        private readonly Contexto _context;

        public LancamentoController(Contexto context)
        {
            _context = context;
        }
        #endregion

        #region VISUALIZAR

        // GET: Lancamento
        public async Task<IActionResult> Index(string? mes, string? ano)
        {

            if (mes == null)
            {
                mes = Convert.ToString(DateTime.Now.Month);
            }

            if (ano == null)
            {
                ano = Convert.ToString(DateTime.Now.Year);
            }

            ViewBag.Mes = mes;
            ViewBag.Ano = ano;

            ViewData["ApenasAno"] = new SelectList(_context.Lancamentos.Where(l => l.Inativo == null).Select(l => l.DtPrevisao.Year).Distinct(), "Ano");

            var contexto = _context.Lancamentos.Include(l => l.Contas).Include(l => l.Jogadores)
                .Where(l => l.Inativo == null)
                .Where(l => l.DtPrevisao.Year == Convert.ToInt32(ano))
                .Where(l => l.DtPrevisao.Month == Convert.ToInt32(mes));
            return View(await contexto.ToListAsync());
        }

        // GET: Lancamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lancamento = await _context.Lancamentos
                .Include(l => l.Contas)
                .Include(l => l.Jogadores)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lancamento == null)
            {
                return NotFound();
            }

            return View(lancamento);
        }

        #endregion

        #region RESUMO

        // GET: Lancamento
        public async Task<IActionResult> Resumo(string? mes, string? ano)
        {

            if (mes == null)
            {
                mes = Convert.ToString(DateTime.Now.Month);
            }

            if (ano == null)
            {
                ano = Convert.ToString(DateTime.Now.Year);
            }

            ViewBag.Mes = mes;
            ViewBag.Ano = ano;

            ViewData["ApenasAno"] = new SelectList(_context.Lancamentos.Where(l => l.Inativo == null).Select(l => l.DtPrevisao.Year).Distinct(), "Ano");

            //Resumo Receita Geral
            var contextoReceita = _context.Lancamentos.Include(l => l.Jogadores).Include(l => l.Contas)
                .Where(l => l.Contas.Tipo == "E")
                .Where(l => l.Inativo == null)
                .Where(l => l.DtPrevisao.Year == Convert.ToInt32(ano))
                .Where(l => l.DtPrevisao.Month == Convert.ToInt32(mes))
                .Sum(l => l.Valor);
            ViewBag.GeralReceita = contextoReceita;

            //Resumo Despesa Geral
            var contextoDespesa = _context.Lancamentos.Include(l => l.Jogadores).Include(l => l.Contas)
                .Where(l => l.Contas.Tipo == "S")
                .Where(l => l.Inativo == null)
                .Where(l => l.DtPrevisao.Year == Convert.ToInt32(ano))
                .Where(l => l.DtPrevisao.Month == Convert.ToInt32(mes))
                .Sum(l => l.Valor);
            ViewBag.GeralDespesa = contextoDespesa;

            //Resumo Saldo Geral
            var contextoSaldo = _context.Lancamentos.Include(l => l.Jogadores).Include(l => l.Contas)
                .Where(l => l.Contas.Tipo == "E")
                .Where(l => l.Inativo == null)
                .Where(l => l.DtPrevisao.Year == Convert.ToInt32(ano))
                .Where(l => l.DtPrevisao.Month == Convert.ToInt32(mes))
                .Sum(l => l.Valor)
                +
                _context.Lancamentos.Include(l => l.Jogadores).Include(l => l.Contas)
                .Where(l => l.Contas.Tipo == "S")
                .Where(l => l.Inativo == null)
                .Where(l => l.DtPrevisao.Year == Convert.ToInt32(ano))
                .Where(l => l.DtPrevisao.Month == Convert.ToInt32(mes))
                .Sum(l => l.Valor); ;

            ViewBag.GeralSaldo = contextoSaldo;

            //Resumo Receita Aberto
            var contextoReceitaAberto = _context.Lancamentos.Include(l => l.Jogadores).Include(l => l.Contas)
                .Where(l => l.Contas.Tipo == "E")
                .Where(l => l.Inativo == null)
                .Where(l => l.DtPrevisao.Year == Convert.ToInt32(ano))
                .Where(l => l.DtPrevisao.Month == Convert.ToInt32(mes))
                .Where(l => l.DtBaixa == null)
                .Sum(l => l.Valor);
            ViewBag.GeralReceitaAberto = contextoReceitaAberto;

            //Resumo Despesa Aberto
            var contextoDespesaAberto = _context.Lancamentos.Include(l => l.Jogadores).Include(l => l.Contas)
                .Where(l => l.Contas.Tipo == "S")
                .Where(l => l.Inativo == null)
                .Where(l => l.DtPrevisao.Year == Convert.ToInt32(ano))
                .Where(l => l.DtPrevisao.Month == Convert.ToInt32(mes))
                .Where(l => l.DtBaixa == null)
                .Sum(l => l.Valor);
            ViewBag.GeralDespesaAberto = contextoDespesaAberto;

            //Resumo Saldo Aberto
            var contextoSaldoAberto = _context.Lancamentos.Include(l => l.Jogadores).Include(l => l.Contas)
                .Where(l => l.Contas.Tipo == "E")
                .Where(l => l.Inativo == null)
                .Where(l => l.DtPrevisao.Year == Convert.ToInt32(ano))
                .Where(l => l.DtPrevisao.Month == Convert.ToInt32(mes))
                .Where(l => l.DtBaixa == null)
                .Sum(l => l.Valor)
                +
                _context.Lancamentos.Include(l => l.Jogadores).Include(l => l.Contas)
                .Where(l => l.Contas.Tipo == "S")
                .Where(l => l.Inativo == null)
                .Where(l => l.DtPrevisao.Year == Convert.ToInt32(ano))
                .Where(l => l.DtPrevisao.Month == Convert.ToInt32(mes))
                .Where(l => l.DtBaixa == null)
                .Sum(l => l.Valor);

            ViewBag.GeralSaldoAberto = contextoSaldoAberto;

            //Tipo Saldo
            /*ViewData["SaldoTipo"] = new SelectList(_context.Lancamentos.Include(l => l.Jogadores).Include(l => l.Contas)
                .Where(l => l.Inativo == null)
                .Where(l => l.DtPrevisao.Year == Convert.ToInt32(ano))
                .Where(l => l.DtPrevisao.Month == Convert.ToInt32(mes))
                .Where(l => l.DtBaixa == null)
                //.Select(l => l.Valor)
                .GroupBy(l => l.ContaId)
                .Select(g => new {Conta = g.Key, count = g.Count()})
                //.Sum(l => l.Valor)
                );*/

            /*var contextoTipos = _context.Lancamentos.Include(l => l.Jogadores).Include(l => l.Contas)
                .Where(l => l.Inativo == null)
                .Where(l => l.DtPrevisao.Year == Convert.ToInt32(ano))
                .Where(l => l.DtPrevisao.Month == Convert.ToInt32(mes))
                .Where(l => l.DtBaixa == null);

            var saldoTipo = from tipo in contextoTipos
                            group tipo by tipo.Contas.Tipo into tiposAgrupados
                            select tiposAgrupados;

            ViewData["TesteTipo"] = saldoTipo;*/

            return View();
        }

        #endregion

        #region CRIAR

        // GET: Lancamento/Create
        public IActionResult Create()
        {
            ViewData["ContaId"] = new SelectList(_context.Contas.Where(l => l.Inativo == null), "Id", "NomeConta");
            ViewData["JogadorId"] = new SelectList(_context.Jogadores.Where(l => l.Inativo == null), "Id", "NomeJogador");
            return View();
        }

        // POST: Lancamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContaId,JogadorId,Valor,ObsLancamento,DtPrevisao,DtBaixa,Inativo")] Lancamento lancamento)
        {

            /*var tipoConta = _context.Contas.Where(c => c.Id == lancamento.ContaId).Where(c => c.Tipo == "S");
            if (tipoConta.Any())
            {
                lancamento.Valor = lancamento.Valor * (-1);
            }*/

            lancamento.Valor = ConverterValor(lancamento.ContaId, lancamento.Valor);

            //if (ModelState.IsValid)
            //{
            _context.Add(lancamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["ContaId"] = new SelectList(_context.Contas.Where(l => l.Inativo == null), "Id", "NomeConta", lancamento.ContaId);
            //ViewData["JogadorId"] = new SelectList(_context.Jogadores.Where(l => l.Inativo == null), "Id", "NomeJogador", lancamento.JogadorId);
            //return View(lancamento);
        }

        #endregion

        #region EDITAR

        // GET: Lancamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lancamento = await _context.Lancamentos.FindAsync(id);
            if (lancamento == null)
            {
                return NotFound();
            }
            ViewData["ContaId"] = new SelectList(_context.Contas, "Id", "NomeConta", lancamento.ContaId);
            ViewData["JogadorId"] = new SelectList(_context.Jogadores, "Id", "NomeJogador", lancamento.JogadorId);
            return View(lancamento);
        }

        // POST: Lancamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContaId,JogadorId,Valor,ObsLancamento,DtPrevisao,DtBaixa,Inativo")] Lancamento lancamento)
        {
            if (id != lancamento.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    lancamento.Valor = ConverterValor(lancamento.ContaId, lancamento.Valor);
                    _context.Update(lancamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LancamentoExists(lancamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["ContaId"] = new SelectList(_context.Contas, "Id", "NomeConta", lancamento.ContaId);
            //ViewData["JogadorId"] = new SelectList(_context.Jogadores, "Id", "NomeJogador", lancamento.JogadorId);
            //return View(lancamento);
        }

        #endregion

        #region DELETAR (Desativado)

        // GET: Lancamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lancamento = await _context.Lancamentos
                .Include(l => l.Contas)
                .Include(l => l.Jogadores)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lancamento == null)
            {
                return NotFound();
            }

            return View(lancamento);
        }

        // POST: Lancamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lancamento = await _context.Lancamentos.FindAsync(id);
            _context.Lancamentos.Remove(lancamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region DELETAR

        // GET: Conta/Edit/5
        public async Task<IActionResult> Inativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lancamento = await _context.Lancamentos.FindAsync(id);
            lancamento.Inativo = DateTime.Now;
            if (lancamento == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lancamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LancamentoExists(lancamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["MsgSucesso"] = "Deletado com Sucesso";  //Transportar valor de MsgSucesso para função de alertify
                return RedirectToAction(nameof(Index));
            }
            return View(lancamento);

        }

        #endregion

        #region METODOS

        private bool LancamentoExists(int id)
        {
            return _context.Lancamentos.Any(e => e.Id == id);
        }

        private decimal ConverterValor(int idConta, decimal valor)
        {
            ViewData["ContaId"] = new SelectList(_context.Contas.Where(l => l.Inativo == null), "Id", "NomeConta");
            var tipoConta = _context.Contas.Where(c => c.Id == idConta).Where(c => c.Tipo == "S");
            if (tipoConta.Any())
            {
                valor = valor * (-1);
            }

            return valor;
        }

        #endregion
    }
}
