using Microsoft.AspNetCore.Mvc;
using MyStore.Catalago.Application.Services;
using RestSharp;

namespace MyStore.WebApp.MVC2.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly RestClient _restClient;

        public CarrinhoController(IProdutoAppService produtoAppService, RestClient restClient)
        {
            _produtoAppService = produtoAppService;
            _restClient = restClient;
        }

        [HttpPost]
        [Route("meu-carrinho")]
        public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
        {
            var request = new RestRequest("Carrinho/meu-carrinho", Method.Get);
            request.AddParameter("id", id);
            request.AddParameter("quantidade", quantidade);
            await _restClient.GetAsync(request);
            return Ok();   

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
