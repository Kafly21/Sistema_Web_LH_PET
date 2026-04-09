using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LH_PET_WEB.Data;
using LH_PET_WEB.Models.ViewModels;


namespace SP_03_UC08_LH_PET_WEB.Controllers
{
   [Authorize(Roles = "Admin")] // Apenas o Dono/Gerente pode ver os lucros
   public class RelatoriosController : Controller
    {
        private readonly ContextoBanco _contexto;

        public RelatoriosController(ContextoBanco contexto)
        {
        _contexto = contexto;
        }

        public async Task<IActionResult> Index()
        {
            var hoje = DateTime.Today;
            var inicioDoMes = new DateTime(hoje.Year, hoje.Month, 1);

            // 1. Vendas do Mês Atual
            var vendasMes = await _contexto.Vendas
            .Where(v => v.DataVenda >= inicioDoMes)
            .ToListAsync();

            // 2. Vendas Apenas de Hoje
            var vendasHoje = vendasMes
            .Where(v => v.DataVenda.Date == hoje)
            .ToList();

            // 3. Agrupa por Forma de Pagamento (Mês) 
            var faturamentoPagamento = vendasMes
            .GroupBy(v => v.FormaPagamento)
            .ToDictionary(g => g.Key, g => g.Sum(v => v.Total));

            // 4. Descobre o TOP 5 Produtos Mais Vendidos no Mês
            var topProdutos = await _contexto.ItensVenda
            .Include(i => i.Produto)
            .Include(i => i.Venda)
            .Where(i => i.Venda!.DataVenda >= inicioDoMes)
            .GroupBy(i => new { i.ProdutoId, i.Produto!.Nome })
            .Select(g => new TopProdutoViewModel
            {
                NomeProduto = g.Key.Nome, QuantidadeVendida = g.Sum(i => i.Quantidade),
                ValorTotalGerado = g.Sum(i => i.Quantidade * i.PrecoUnitario)
            })

            .OrderByDescending(p => p.QuantidadeVendida)
            .Take(5) // Pega só os 5 primeiros
            .ToListAsync();

            // 5. Monta o pacote para enviar para a View
            var viewModel = new RelatorioDashboardViewModel
            {
                FaturamentoHoje = vendasHoje.Sum(v => v.Total), FaturamentoMes = vendasMes.Sum(v => v.Total), QuantidadeVendasMes = vendasMes.Count, FaturamentoPorPagamento = faturamentoPagamento, ProdutosMaisVendidos = topProdutos
            };

            return View(viewModel);
        }
    }
}