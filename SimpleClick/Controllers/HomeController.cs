using ContadorCliques.Data;
using ContadorCliques.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContadorCliques.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        // Injeção de dependência do DbContext
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Action GET - Exibe a página inicial com o contador atual e histórico
        /// </summary>
        public async Task<IActionResult> Index()
        {
            // Busca o contador do banco de dados (sempre o primeiro registro)
            var contador = await _context.Contadores.FirstOrDefaultAsync();
            
            // Se não existir, cria um novo
            if (contador == null)
            {
                contador = new ContadorModel
                {
                    Cliques = 0,
                    UltimaAtualizacao = DateTime.Now
                };
                _context.Contadores.Add(contador);
                await _context.SaveChangesAsync();
            }

            // Busca os últimos 10 cliques do histórico (mais recentes primeiro)
            var historico = await _context.HistoricoClicks
                .OrderByDescending(h => h.DataHoraClick)
                .Take(10)
                .ToListAsync();

            // Cria o ViewModel com os dados
            var viewModel = new ContadorViewModel
            {
                Contador = contador,
                Historico = historico
            };

            return View(viewModel);
        }

        /// <summary>
        /// Action POST - Incrementa o contador quando o botão é clicado
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Contar()
        {
            // Busca o contador
            var contador = await _context.Contadores.FirstOrDefaultAsync();
            
            if (contador != null)
            {
                // Incrementa o contador
                contador.Cliques++;
                contador.UltimaAtualizacao = DateTime.Now;

                // Adiciona um registro no histórico
                var historicoClick = new HistoricoClickModel
                {
                    DataHoraClick = DateTime.Now,
                    NumeroClick = contador.Cliques
                };
                
                _context.HistoricoClicks.Add(historicoClick);
                
                // Salva as alterações no banco de dados
                await _context.SaveChangesAsync();
            }

            // Redireciona de volta para a página inicial
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action POST - Reseta o contador para zero
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Resetar()
        {
            // Busca o contador
            var contador = await _context.Contadores.FirstOrDefaultAsync();
            
            if (contador != null)
            {
                // Reseta o contador para zero
                contador.Cliques = 0;
                contador.UltimaAtualizacao = DateTime.Now;

                // Opcional: Limpar todo o histórico
                var todosHistoricos = await _context.HistoricoClicks.ToListAsync();
                _context.HistoricoClicks.RemoveRange(todosHistoricos);
                
                // Salva as alterações
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Action GET - Exibe todo o histórico de cliques
        /// </summary>
        public async Task<IActionResult> HistoricoCompleto()
        {
            var historico = await _context.HistoricoClicks
                .OrderByDescending(h => h.DataHoraClick)
                .ToListAsync();

            return View(historico);
        }
    }
}