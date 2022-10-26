using Microsoft.AspNetCore.Mvc;
using MyStore.Catalago.Application.Services;
using MyStore.Catalago.Application.ViewModels;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace MyStore.WebApp.MVC2.Controllers.Admin
{
    public class AdminProdutosController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;
        private readonly RestClient _restClient;

        public AdminProdutosController(IProdutoAppService produtoAppService, RestClient res)
        {
            _produtoAppService = produtoAppService;
            _restClient = res;
            //_restClient.UseNewtonsoftJson();
        }

        [HttpGet]
        [Route("admin-produtos")]
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("Produto", Method.Get);
            var result = await _restClient.GetAsync<IEnumerable<ProdutoDto>>(request);            
            return View(result);
        }

        [Route("novo-produto")]
        public async Task<IActionResult> NovoProduto()
        {
            return View(await PopularCategorias(new ProdutoDto()));
        }

        [Route("novo-produto")]
        [HttpPost]
        public async Task<IActionResult> NovoProduto(ProdutoDto produtoDto)
        {
            if (!ModelState.IsValid) { Console.WriteLine("Estado não valido"); } ///eturn View(await PopularCategorias(produtoDto));

            var request = new RestRequest("Produto", Method.Post);
            request.AddJsonBody(produtoDto);
            await _restClient.PostAsync(request);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id)
        {
            //var produto = await _produtoAppService.ObterPorId(id);            
            var request = new RestRequest($"Produto/{id.ToString()}", Method.Get);
            var result = await _restClient.GetAsync<ProdutoDto>(request);
            return View(await PopularCategorias(result)) ;
        }

        [HttpPost]
        [Route("editar-produto")]
        public async Task<IActionResult> AtualizarProduto(Guid id, ProdutoDto produtoDto)
        {
            //var produto = await _produtoAppService.ObterPorId(id);
            var request = new RestRequest($"Produto/{id.ToString()}", Method.Get);
            var produto = await _restClient.GetAsync<ProdutoDto>(request);
            produtoDto.QuantidadeEstoque = produto.QuantidadeEstoque;
            //produtoDto = await PopularCategorias(produtoDto);

            ModelState.Remove("QuantidadeEstoque");
            //if (!ModelState.IsValid) return View(await PopularCategorias(produtoDto));

            //await _produtoAppService.AtualizarProduto(produtoDto);
            var requestAtualizar = new RestRequest("Produto/AtualizarProduto", Method.Post);
            requestAtualizar.AddJsonBody(produtoDto);
            await _restClient.PostAsync(requestAtualizar);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id)
        {
            var request = new RestRequest($"Produto/{id.ToString()}", Method.Get);
            var produto = await _restClient.GetAsync<ProdutoDto>(request);
            return View("Estoque", produto);
            //return View("Estoque", await _produtoAppService.ObterPorId(id));
        }

        [HttpPost]
        [Route("produtos-atualizar-estoque")]
        public async Task<IActionResult> AtualizarEstoque(Guid id, int quantidade)
        {
            if (quantidade > 0)
            {
                //await _produtoAppService.ReporEstoque(id, quantidade);
                var request = new RestRequest("Produto/ReporEstoque", Method.Get);
                request.AddParameter("id", id);
                request.AddParameter("quantidade", quantidade);
                var r = await _restClient.GetAsync(request);
            }
            else
            {
                //await _produtoAppService.DebitarEstoque(id, quantidade);
                var request = new RestRequest("Produto/DebitarEstoque", Method.Get);
                request.AddParameter("id", id);
                request.AddParameter("quantidade", quantidade);
                var r = await _restClient.GetAsync(request);
            }


            var requestTodos = new RestRequest("Produto", Method.Get);
            var result = await _restClient.GetAsync<IEnumerable<ProdutoDto>>(requestTodos);
            //return View("Index", await _produtoAppService.ObterTodos());
            return View("Index", result);
        }


        private async Task<ProdutoDto> PopularCategorias(ProdutoDto produto)
        {
            var request = new RestRequest("Produto/ObterCategorias", Method.Get);
            var result = await _restClient.GetAsync<IEnumerable<CategoriaDto>>(request);
            produto.Categorias = result;
            return produto;
        }
    }
}
