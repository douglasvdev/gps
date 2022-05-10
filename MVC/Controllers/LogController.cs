using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MVC.Controllers
{
    public class LogController : Controller
    {
        #region CONSTRUTOR
        private readonly Contexto _context;

        public LogController(Contexto context)
        {
            _context = context;
        }
        #endregion

        #region VISUALIZAR
        public async Task<IActionResult> Index(string? mes, string? ano)
        {
            //_Ano(ano);
            //_Mes(mes);
            return View(await _context.Logs
                .Where(l => l.Quando.Month.ToString() == _Mes(mes))
                .Where(l => l.Quando.Year.ToString() == _Ano(ano))
                .ToListAsync());
        }

        public string _Ano(string? ano)
        {
            if (ano == null)
            {
                ano = Convert.ToString(DateTime.Now.Year);
            }

            ViewBag.Ano = ano;

            ViewData["ApenasAno"] = new SelectList(_context.Logs.Select(l => l.Quando.Year).Distinct(), "Ano");

            return ano;
        }

        public string _Mes(string? mes)
        {
            if (mes == null)
            {
                mes = Convert.ToString(DateTime.Now.Month);
            }

            ViewBag.Mes = mes;

            return mes;
        }
        #endregion
    }
}
