using Microsoft.AspNetCore.Mvc;
using MyStore.Catalago.Application.Services;
using MyStore.Core.Bus;
using MyStore.Vendas.Application.Commands;

namespace WebApi.Controllers
{
    public class CarrinhoController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IMediatrHandler _mediatorHandler;

        public CarrinhoController(IProdutoAppService produtoAppService, IMediatrHandler mediatorHandler)
        {
            _produtoAppService = produtoAppService;
            _mediatorHandler = mediatorHandler;
        }

        [HttpPost]
        [Route("meu-carrinho")]
        public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            if (produto.QuantidadeEstoque < quantidade)
            {
                TempData["Erro"] = "Produto com estoque insuficiente";
                return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
            }

            var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
            await _mediatorHandler.EnviarComando(command);

            // se tudo deu certo


            TempData["Erros"] = "Produto Indisponível";
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
