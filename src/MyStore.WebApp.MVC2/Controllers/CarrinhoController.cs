using Microsoft.AspNetCore.Mvc;
using MyStore.Catalago.Application.Services;
using RestSharp;

namespace MyStore.WebApp.MVC2.Controllers
{
    public class CarrinhoController : Controller
    {

        private readonly IProdutoAppService _produtoAppService;

        public CarrinhoController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        [HttpPost]
        [Route("meu-carrinho")]
        public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
        {
            throw new NotImplementedException();    
            //var request = new RestRequest("Produto", Method.Post);
            //request.AddJsonBody(produtoDto);
            //await _restClient.PostAsync(request);

            //var produto = await _produtoAppService.ObterPorId(id);
            //if (produto == null) return BadRequest();

            //if (produto.QuantidadeEstoque < quantidade)
            //{
            //    TempData["Erro"] = "Produto com estoque insuficiente";
            //    return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
            //}
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
