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
    public class ParametroController : Controller
    {
        private readonly Contexto _context;

        public ParametroController(Contexto context)
        {
            _context = context;
        }

        // GET: Parametro
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parametros.ToListAsync());
        }

        // GET: Parametro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametro = await _context.Parametros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parametro == null)
            {
                return NotFound();
            }

            return View(parametro);
        }

        // GET: Parametro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parametro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DescParametro,CodParametro,Ponto,Inativo")] Parametro parametro)
        {
            if (ModelState.IsValid)
            {
                parametro.CodParametro = parametro.CodParametro.ToUpper();
                _context.Add(parametro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parametro);
        }

        // GET: Parametro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametro = await _context.Parametros.FindAsync(id);
            if (parametro == null)
            {
                return NotFound();
            }
            return View(parametro);
        }

        // POST: Parametro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DescParametro,CodParametro,Ponto,Inativo")] Parametro parametro)
        {
            if (id != parametro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    parametro.CodParametro = parametro.CodParametro.ToUpper();
                    _context.Update(parametro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParametroExists(parametro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parametro);
        }

        // GET: Parametro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametro = await _context.Parametros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parametro == null)
            {
                return NotFound();
            }

            return View(parametro);
        }

        // POST: Parametro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parametro = await _context.Parametros.FindAsync(id);
            _context.Parametros.Remove(parametro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #region DELETAR

        // GET: Conta/Edit/5
        public async Task<IActionResult> Inativar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parametro = await _context.Parametros.FindAsync(id);
            parametro.Inativo = DateTime.Now;
            if (parametro == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parametro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParametroExists(parametro.Id))
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
            return View(parametro);

        }

        #endregion

        private bool ParametroExists(int id)
        {
            return _context.Parametros.Any(e => e.Id == id);
        }
    }
}
