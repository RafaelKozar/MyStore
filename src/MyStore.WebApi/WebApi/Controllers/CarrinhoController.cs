using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStore.Catalago.Application.Services;
using MyStore.Core.Comunication.Mediator;
using MyStore.Core.DomainObjects;
using MyStore.Core.Messages.CommonMessages.Notifications;
using MyStore.Vendas.Application.Commands;
using System.Text;

namespace WebApi.Controllers
{
    //[ApiController]
    [Route("[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly IMediatorHandler _mediatorHandler;

        public CarrinhoController(IProdutoAppService produtoAppService, 
            IMediatorHandler mediatorHandler, 
            INotificationHandler<DomainNotification> notifications) : base(notifications, mediatorHandler)
        {
            _produtoAppService = produtoAppService;
            _mediatorHandler = mediatorHandler;
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

            //var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, quantidade, produto.Valor);
            var command = new AdicionarItemPedidoCommand(ClienteId, produto.Id, produto.Nome, 2000, 50000);

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


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(JsonResult))]
        [Route("meu-carrinho2")]
        public async Task<ActionResult> AdicionarItem2(Guid id, int quantidade)
        {
            return Ok(new { tess= "kgfdljk" });
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
