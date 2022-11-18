using Microsoft.AspNetCore.Mvc;
using MyStore.Catalago.Application.Services;
using MyStore.Core.DomainObjects;
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
            var response = await _restClient.GetAsync<ResponseObject>(request);

            //return null;
            return RedirectToAction("ProdutoDetalhe", "Vitrine", Guid.Parse("9703a726-6257-49c3-884b-49be87521325"));
            //if (string.IsNullOrEmpty(response.ControllerName))
            //    return RedirectToAction("Index");
            

            //return RedirectToAction(response.ActionName, response.ControllerName, response.property);

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
