#nullable disable
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Data.Entity;  //Erro - Substituir pelo using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers
{
    public class ScoutController : Controller
    {
        #region CONSTRUTOR
        private readonly Contexto _context;

        public ScoutController(Contexto context)
        {
            _context = context;
        }
        #endregion

        #region VISUALIZAR

        // GET: Scout
        public async Task<IActionResult> Index(string? ano)
        {

            //if (dtInicial == null)
            //{
            //    dtInicial = DateTime.MinValue;
            //}

            //if (dtFinal == null)
            //{
            //    dtFinal = DateTime.Now;
            //}

            if (ano == null)
            {
                ano = Convert.ToString(DateTime.Now.Year);
            }

            //ViewBag.DtF = dtFinal;
            ViewBag.Ano = ano;

            ViewData["ApenasAno"] = new SelectList(_context.Scouts.Where(l => l.Inativo == null).Select(l => l.DtPartida.Year).Distinct(), "Ano");

            var contexto = _context.Scouts.Include(l => l.Jogadores).Include(l => l.Parametros)
                .Where(l => l.Inativo == null)
                .Where(l => l.DtPartida.Year == Convert.ToInt32(ano));
            return View(await contexto.ToListAsync());
        }

        #endregion

        #region CRIAR

        public IActionResult Tabela()
        {
            ViewData["JogadorIdA1"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdA2"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdA3"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdA4"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdA5"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdA6"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdA7"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdA8"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdA9"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdA10"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");

            ViewData["JogadorIdB1"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdB2"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdB3"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdB4"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdB5"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdB6"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdB7"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdB8"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdB9"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");
            ViewData["JogadorIdB10"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null), "Id", "NomeJogador");

            //ViewData["ParametroId"] = new SelectList(_context.Parametros, "Id", "DescParametro");
            return View();
        }

        // POST: Scout/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Tabela(DateTime DtPartida, String ObsScout,
            int jogA1, int golA1,
            int jogA2, int golA2,
            int jogA3, int golA3,
            int jogA4, int golA4,
            int jogA5, int golA5,
            int jogA6, int golA6,
            int jogA7, int golA7,
            int jogA8, int golA8,
            int jogA9, int golA9,
            int jogA10, int golA10,
            int jogB1, int golB1,
            int jogB2, int golB2,
            int jogB3, int golB3,
            int jogB4, int golB4,
            int jogB5, int golB5,
            int jogB6, int golB6,
            int jogB7, int golB7,
            int jogB8, int golB8,
            int jogB9, int golB9,
            int jogB10, int golB10,
            [Bind("Id,DtPartida,JogadorId,Presente,CodParametro,Gol,Assistencia,ObsScout,Inativo")] Scout scout)
        {
            var GolA = golA1 + golA2 + golA3 + golA4 + golA5 + golA6 + golA7 + golA8 + golA9 + golA10;
            var GolB = golB1 + golB2 + golB3 + golB4 + golB5 + golB6 + golB7 + golB8 + golB9 + golB10;


            //if (ModelState.IsValid)
            //{
            if (GolA > GolB)
            {
                scout.JogadorId = jogA1;
                scout.Presente = true;
                scout.ParametroId = 3;
                scout.Gol = golA1;
                _context.Add(scout);
                await _context.SaveChangesAsync();

                scout.Id = 0;
                scout.JogadorId = jogB1;
                scout.Presente = true;
                scout.ParametroId = 4;
                scout.Gol = golB1;
                _context.Add(scout);
                await _context.SaveChangesAsync();

                //return RedirectToAction(nameof(Index));
            }
            //}
            //ViewData["JogadorId"] = new SelectList(_context.Jogadores, "Id", "Mensalista", scout.JogadorId);
            //ViewData["ParametroId"] = new SelectList(_context.Parametros, "Id", "DescParametro", scout.ParametroId);
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
