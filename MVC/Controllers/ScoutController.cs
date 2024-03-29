﻿#nullable disable
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
        /*--------------------------------
        * Acrescentar campo Time em tabela Scout, que vai receber o Time que jogou em cada partida; 
        * Passar Mensalista S, E, ou N como parametro; 
        * Trabalhar no JSON de Scout;
        * -------------------------------------*/

        #region CONSTRUTOR
        private readonly Contexto _context;

        public ScoutController(Contexto context)
        {
            _context = context;
        }
        #endregion

        #region VISUALIZAR

        // GET: Scout
        public async Task<IActionResult> Index(string? ano, string? mensalista)
        {

            //if (dtInicial == null)
            //{
            //    dtInicial = DateTime.MinValue;
            //}

            //if (dtFinal == null)
            //{
            //    dtFinal = DateTime.Now;
            //}

            _Ano(ano);
            _Mensalista(mensalista);

            var contexto = _context.Scouts.Include(l => l.Jogadores).Include(l => l.Parametros)
                .Where(l => l.Inativo == null)
                .Where(l => l.DtPartida.Year == Convert.ToInt32(_Ano(ano)))
                .Where(l => l.Jogadores.Mensalista == _Mensalista(mensalista))
                .Where(l => l.Presente == 1);

            /*var contexto = from sct in _context.Scouts
                           join jog in _context.Jogadores on sct.JogadorId equals jog.Id
                           join par in _context.Parametros on sct.ParametroId equals par.Id
                           group sct by new
                           {
                               jog.NomeJogador
                           } into g
                           orderby g.Sum(s => s.Gol) descending
                           select new
                           {
                               Nome = g.Key.NomeJogador,
                               Gols = g.Sum(s => s.Gol)
                           };
            ViewData["scout"] = contexto;*/

            //ValidarMensalistaSemPresenca();
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
            #region Jogadores Time A
            List<int> listaJogTimeA = new List<int>();
            listaJogTimeA.Add(scout.JogadorIdA1 = jogA1);
            //listaJogTimeA.Add(0);

            listaJogTimeA.Add((int)(scout.JogadorIdA2 = jogA2));

            listaJogTimeA.Add((int)(scout.JogadorIdA3 = jogA3));

            listaJogTimeA.Add((int)(scout.JogadorIdA4 = jogA4));

            listaJogTimeA.Add((int)(scout.JogadorIdA5 = jogA5));

            listaJogTimeA.Add((int)(scout.JogadorIdA6 = jogA6));

            listaJogTimeA.Add((int)(scout.JogadorIdA7 = jogA7));

            listaJogTimeA.Add((int)(scout.JogadorIdA8 = jogA8));

            listaJogTimeA.Add((int)(scout.JogadorIdA9 = jogA9));

            listaJogTimeA.Add((int)(scout.JogadorIdA10 = jogA10));
            #endregion

            #region Gols Time A
            List<int> listaGolTimeA = new List<int>();
            listaGolTimeA.Add((int)(scout.GolsA1 = golA1));

            listaGolTimeA.Add((int)(scout.GolsA2 = golA2));

            listaGolTimeA.Add((int)(scout.GolsA3 = golA3));

            listaGolTimeA.Add((int)(scout.GolsA4 = golA4));

            listaGolTimeA.Add((int)(scout.GolsA5 = golA5));

            listaGolTimeA.Add((int)(scout.GolsA6 = golA6));

            listaGolTimeA.Add((int)(scout.GolsA7 = golA7));

            listaGolTimeA.Add((int)(scout.GolsA8 = golA8));

            listaGolTimeA.Add((int)(scout.GolsA9 = golA9));

            listaGolTimeA.Add((int)(scout.GolsA10 = golA10));

            #endregion

            #region Jogadores Time B
            List<int> listaJogTimeB = new List<int>();
            listaJogTimeB.Add(scout.JogadorIdB1 = jogB1);
            //listaJogTimeB.Add(0);

            listaJogTimeB.Add((int)(scout.JogadorIdB2 = jogB2));

            listaJogTimeB.Add((int)(scout.JogadorIdB3 = jogB3));

            listaJogTimeB.Add((int)(scout.JogadorIdB4 = jogB4));

            listaJogTimeB.Add((int)(scout.JogadorIdB5 = jogB5));

            listaJogTimeB.Add((int)(scout.JogadorIdB6 = jogB6));

            listaJogTimeB.Add((int)(scout.JogadorIdB7 = jogB7));

            listaJogTimeB.Add((int)(scout.JogadorIdB8 = jogB8));

            listaJogTimeB.Add((int)(scout.JogadorIdB9 = jogB9));

            listaJogTimeB.Add((int)(scout.JogadorIdB10 = jogB10));
            #endregion

            #region Gols Time B
            List<int> listaGolTimeB = new List<int>();
            listaGolTimeB.Add((int)(scout.GolsB1 = golB1));

            listaGolTimeB.Add((int)(scout.GolsB2 = golB2));

            listaGolTimeB.Add((int)(scout.GolsB3 = golB3));

            listaGolTimeB.Add((int)(scout.GolsB4 = golB4));

            listaGolTimeB.Add((int)(scout.GolsB5 = golB5));

            listaGolTimeB.Add((int)(scout.GolsB6 = golB6));

            listaGolTimeB.Add((int)(scout.GolsB7 = golB7));

            listaGolTimeB.Add((int)(scout.GolsB8 = golB8));

            listaGolTimeB.Add((int)(scout.GolsB9 = golB9));

            listaGolTimeB.Add((int)(scout.GolsB10 = golB10));

            #endregion

            #region Regra de Negocio

            if (GolA > GolB)
            {
                for (int i = 0; i < listaJogTimeA.Count; i++)
                {
                    if (listaJogTimeA[i] != 0)
                    {
                        scout.Id = 0;
                        scout.DtPartida = DtPartida;
                        scout.JogadorId = listaJogTimeA[i];
                        scout.Presente = 1;
                        scout.ParametroId = 3;   //1=>Presenca(P-1)| 2=>Falta(F-0)| 3=>Vitoria(V-3)| 4=>Derrota(D-0)| 5=>Empate(E-1)| 6=>Justificado(J-1)
                        scout.Gol = listaGolTimeA[i];
                        scout.ObsScout = ObsScout;
                        scout.Time = "TimeA";
                        _context.Add(scout);
                        await _context.SaveChangesAsync();
                    }

                    if (listaJogTimeB[i] != 0)
                    {
                        scout.Id = 0;
                        scout.DtPartida = DtPartida;
                        scout.JogadorId = listaJogTimeB[i];
                        scout.Presente = 1;
                        scout.ParametroId = 4;   //1=>Presenca(P-1)| 2=>Falta(F-0)| 3=>Vitoria(V-3)| 4=>Derrota(D-0)| 5=>Empate(E-1)| 6=>Justificado(J-1)
                        scout.Gol = listaGolTimeB[i];
                        scout.ObsScout = ObsScout;
                        scout.Time = "TimeB";
                        _context.Add(scout);
                        await _context.SaveChangesAsync();
                    }
                }

                
                for (int i = 0; i < ListaFaltas(listaJogTimeA, listaJogTimeB).Count; i++)
                {
                    if (ListaFaltas(listaJogTimeA, listaJogTimeB)[i].Id != 0)
                    {
                        scout.Id = 0;
                        scout.DtPartida = DtPartida;
                        scout.JogadorId = ListaFaltas(listaJogTimeA, listaJogTimeB)[i].Id;
                        scout.Presente = 0;
                        scout.ParametroId = 2;     //1=>Presenca(P-1)| 2=>Falta(F-0)| 3=>Vitoria(V-3)| 4=>Derrota(D-0)| 5=>Empate(E-1)| 6=>Justificado(J-1)
                        scout.Gol = 0;
                        scout.ObsScout = ObsScout;
                        scout.Time = "Faltou";
                        _context.Add(scout);
                        await _context.SaveChangesAsync();
                    }
                }
                
                //ValidarMensalistaSemPresenca();
            }
            else if (GolA < GolB)
            {
                for (int i = 0; i < listaJogTimeA.Count; i++)
                {
                    if (listaJogTimeA[i] != 0)
                    {
                        scout.Id = 0;
                        scout.DtPartida = DtPartida;
                        scout.JogadorId = listaJogTimeA[i];
                        scout.Presente = 1;
                        scout.ParametroId = 4;   //1=>Presenca(P-1)| 2=>Falta(F-0)| 3=>Vitoria(V-3)| 4=>Derrota(D-0)| 5=>Empate(E-1)| 6=>Justificado(J-1)
                        scout.Gol = listaGolTimeA[i];
                        scout.ObsScout = ObsScout;
                        scout.Time = "TimeA";
                        _context.Add(scout);
                        await _context.SaveChangesAsync();
                    }

                    if (listaJogTimeB[i] != 0)
                    {
                        scout.Id = 0;
                        scout.DtPartida = DtPartida;
                        scout.JogadorId = listaJogTimeB[i];
                        scout.Presente = 1;
                        scout.ParametroId = 3;   //1=>Presenca(P-1)| 2=>Falta(F-0)| 3=>Vitoria(V-3)| 4=>Derrota(D-0)| 5=>Empate(E-1)| 6=>Justificado(J-1)
                        scout.Gol = listaGolTimeB[i];
                        scout.ObsScout = ObsScout;
                        scout.Time = "TimeB";
                        _context.Add(scout);
                        await _context.SaveChangesAsync();
                    }
                }

                for (int i = 0; i < ListaFaltas(listaJogTimeA, listaJogTimeB).Count; i++)
                {
                    if (ListaFaltas(listaJogTimeA, listaJogTimeB)[i].Id != 0)
                    {
                        scout.Id = 0;
                        scout.DtPartida = DtPartida;
                        scout.JogadorId = ListaFaltas(listaJogTimeA, listaJogTimeB)[i].Id;
                        scout.Presente = 0;
                        scout.ParametroId = 2;     //1=>Presenca(P-1)| 2=>Falta(F-0)| 3=>Vitoria(V-3)| 4=>Derrota(D-0)| 5=>Empate(E-1)| 6=>Justificado(J-1)
                        scout.Gol = 0;
                        scout.ObsScout = ObsScout;
                        scout.Time = "Faltou";
                        _context.Add(scout);
                        await _context.SaveChangesAsync();
                    }
                }
                //ValidarMensalistaSemPresenca();
            }
            else
            {
                for (int i = 0; i < listaJogTimeA.Count; i++)
                {
                    if (listaJogTimeA[i] != 0)
                    {
                        scout.Id = 0;
                        scout.DtPartida = DtPartida;
                        scout.JogadorId = listaJogTimeA[i];
                        scout.Presente = 1;
                        scout.ParametroId = 5;   //1=>Presenca(P-1)| 2=>Falta(F-0)| 3=>Vitoria(V-3)| 4=>Derrota(D-0)| 5=>Empate(E-1)| 6=>Justificado(J-1)
                        scout.Gol = listaGolTimeA[i];
                        scout.ObsScout = ObsScout;
                        scout.Time = "TimeA";
                        _context.Add(scout);
                        await _context.SaveChangesAsync();
                    }

                    if (listaJogTimeB[i] != 0)
                    {
                        scout.Id = 0;
                        scout.DtPartida = DtPartida;
                        scout.JogadorId = listaJogTimeB[i];
                        scout.Presente = 1;
                        scout.ParametroId = 5;   //1=>Presenca(P-1)| 2=>Falta(F-0)| 3=>Vitoria(V-3)| 4=>Derrota(D-0)| 5=>Empate(E-1)| 6=>Justificado(J-1)
                        scout.Gol = listaGolTimeB[i];
                        scout.ObsScout = ObsScout;
                        scout.Time = "TimeB";
                        _context.Add(scout);
                        await _context.SaveChangesAsync();
                    }
                }

                for (int i = 0; i < ListaFaltas(listaJogTimeA, listaJogTimeB).Count; i++)
                {
                    if (ListaFaltas(listaJogTimeA, listaJogTimeB)[i].Id != 0)
                    {
                        scout.Id = 0;
                        scout.DtPartida = DtPartida;
                        scout.JogadorId = ListaFaltas(listaJogTimeA, listaJogTimeB)[i].Id;
                        scout.Presente = 0;
                        scout.ParametroId = 2;     //1=>Presenca(P-1)| 2=>Falta(F-0)| 3=>Vitoria(V-3)| 4=>Derrota(D-0)| 5=>Empate(E-1)| 6=>Justificado(J-1)
                        scout.Gol = 0;
                        scout.ObsScout = ObsScout;
                        scout.Time = "Faltou";
                        _context.Add(scout);
                        await _context.SaveChangesAsync();
                    }
                }
                //ValidarMensalistaSemPresenca();
            }

            #endregion
            //}

            return RedirectToAction(nameof(Tabela));
        }

        #endregion

        #region METODOS

        public List<Jogador> ListaFaltas(List<int> listaPresenteA, List<int> listaPresenteB)
        {
            var contextoFaltas = _context.Jogadores
                .Where(l => l.Inativo == null)
                .Where(l => l.Mensalista == "S")
                .Where(l => !listaPresenteA.Contains(l.Id) && !listaPresenteB.Contains(l.Id)).ToList();

            List<Jogador> listarFaltantes = new List<Jogador>();
            listarFaltantes.AddRange(contextoFaltas);
            
            return listarFaltantes;
        }

        public async void ValidarMensalistaSemPresenca()
        {
            var contextoMensalistaSemPresenca = _context.Scouts
                //.Where(j => j.Jogadores.Inativo == null)
                .Where(s => s.Jogadores.Mensalista == "S")
                .Where(s => s.Presente == 1)
                .Select(s => s.JogadorId)
                .Distinct().ToList();


            var contextoMensalistas = _context.Jogadores
                .Where(l => !contextoMensalistaSemPresenca.Contains(l.Id))
                .Select(l => l.Id).ToList();

            for (int i = 0; i < contextoMensalistas.Count; i++)
            {
                Jogador jogador = new Jogador();
                jogador.Id = contextoMensalistas[i];
                jogador.NomeJogador = "LEANDRO";
                jogador.Mensalista = "E";
                _context.Update(jogador);
                _context.SaveChangesAsync();
            }
            
        }

        public string _Ano(string? ano)
        {
            if (ano == null)
            {
                ano = Convert.ToString(DateTime.Now.Year);
            }

            ViewBag.Ano = ano;

            ViewData["ApenasAno"] = new SelectList(_context.Scouts.Where(l => l.Inativo == null).Select(l => l.DtPartida.Year).Distinct(), "Ano");

            return ano;
        }

        public string _Mensalista(string? mensalista)
        {
            if (mensalista == null)
            {
                mensalista = "S";
            }

            ViewBag.Mensalista = mensalista;

            ViewData["ListaMensalista"] = new SelectList(_context.Jogadores.Where(j => j.Inativo == null).Select(j => j.Mensalista).Distinct(), "Mensalista");

            return mensalista;
        }

        #endregion

    }
}
