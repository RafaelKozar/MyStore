using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStore.Catalago.Application.Services;
using MyStore.Core.Comunication.Mediator;
using MyStore.Core.DomainObjects;
using MyStore.Core.Messages.CommonMessages.Notifications;
using MyStore.Vendas.Application.Commands;
using MyStore.Vendas.Application.Queries;
using MyStore.Vendas.Application.Queries.DTOs;
using System.Text;

namespace WebApi.Controllers
{
    //[ApiController]
    [Route("[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IPedidoQueries _pedidoQueries;

        public CarrinhoController(IProdutoAppService produtoAppService,
            IMediatorHandler mediatorHandler,
            INotificationHandler<DomainNotification> notifications, IPedidoQueries pedidoQueries) : base(notifications, mediatorHandler)
        {
            _produtoAppService = produtoAppService;
            _mediatorHandler = mediatorHandler;
            _pedidoQueries = pedidoQueries;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseObject))]
        [Route("meu-carrinho")]
        public async Task<ActionResult> AdicionarItem(Guid id, int quantidade)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            if (produto.QuantidadeEstoque < quantidade)
            {
                ///TempData["Erro"] = "Produto com estoque insuficiente";
                //return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
                return Ok(new
                {
                    ActionName = "produto-detalhe",
                    ControllerName = "Vitrine",
                    property = id,
                    message = "Produto com estoque insuficiente"
                });
            }

            var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
            //para teste
            //var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, 2000, 0);

            await _mediatorHandler.EnviarComando(command);

            if (OperacaoValida())
            {
                return Ok(new 
                {
                    ActionName = "Index"                   
                });
                //return RedirectToAction("Index");
            }


            var messages = ObterMensagensErro().ToList();
            
            //string str = string.Empty;
            //messages.ForEach(w =>
            //{
            //    str += $"{w} +";
            //});

            var builder = new StringBuilder();

            foreach (var m in messages)
            {
                builder.Append(m).Append("-");
            }

            var str = builder.ToString();
            str = str.Remove(str.Length - 2);

            //TempData["Erros"] = "Produto Indisponível";            
            return Ok(new ResponseObject
            {
                ActionName = "produto-detalhe",
                ControllerName = "Vitrine",
                property = id,
                messages = str
            }) ;
            
            //return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }

        [Route("meu-carrinho3")]
        public async Task<CarrinhoDto> Index()
        {
            return await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JsonResult))]
        [Route("meu-carrinho2")]
        public async Task<ActionResult> AdicionarItem2(Guid id, int quantidade)
        {
            return Ok(new { tess = "kgfdljk" });
        }


        [HttpPost]
        [Route("remover-item")]
        public async Task<IActionResult> RemoverItem(Guid id)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            var command = new RemoverItemPedidoCommand(ClienteId, id);
            await _mediatorHandler.EnviarComando(command);

            

            if (OperacaoValida())
            {
                return Ok();
                ///return RedirectToAction("Index");
            }

            var result = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
            ConcateneMensagens(result);            
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarrinhoDto))]
        [Route("atualizar-item")]
        public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
        {
            var produto = await _produtoAppService.ObterPorId(id);
            if (produto == null) return BadRequest();

            var command = new AtualizarItemPedidoCommand(ClienteId, id, quantidade);
            await _mediatorHandler.EnviarComando(command);

            

            if (OperacaoValida())
            {
                return Ok();
                //return RedirectToAction("Index");
            }

            var result = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
            ConcateneMensagens(result);
            return Ok(result);
        }

        [HttpPost]
        [Route("aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
        {
            var command = new AplicarVoucherPedidoCommand(ClienteId, voucherCodigo);
            await _mediatorHandler.EnviarComando(command);

            

            if (OperacaoValida())
            {
                return Ok();
                //return RedirectToAction("Index");
            }

            var result = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);
            ConcateneMensagens(result);
            return Ok(result);
        }

        public void ConcateneMensagens(CarrinhoDto carrinho)
        {
            var messages = ObterMensagensErro().ToList();

            //string str = string.Empty;
            //messages.ForEach(w =>
            //{
            //    str += $"{w} +";
            //});

            var builder = new StringBuilder();

            foreach (var m in messages)
            {
                builder.Append(m).Append("-");
            }

            var str = builder.ToString();
            str = str.Remove(str.Length - 2);
            carrinho.Messages = str;
        }

        //[Route("resumo-da-compra")]
        //public async Task<IActionResult> ResumoDaCompra()
        //{
        //    return View(await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        //}

        //[HttpPost]
        //[Route("iniciar-pedido")]
        //public async Task<IActionResult> IniciarPedido(CarrinhoViewModel carrinhoViewModel)
        //{
        //    var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteId);

        //    var command = new IniciarPedidoCommand(carrinho.PedidoId, ClienteId, carrinho.ValorTotal, carrinhoViewModel.Pagamento.NomeCartao,
        //        carrinhoViewModel.Pagamento.NumeroCartao, carrinhoViewModel.Pagamento.ExpiracaoCartao, carrinhoViewModel.Pagamento.CvvCartao);

        //    await _mediatorHandler.EnviarComando(command);

        //    if (OperacaoValida())
        //    {
        //        return RedirectToAction("Index", "Pedido");
        //    }

        //    return View("ResumoDaCompra", await _pedidoQueries.ObterCarrinhoCliente(ClienteId));
        //}



    }
}
