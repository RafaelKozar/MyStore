using Microsoft.AspNetCore.Mvc;
using MyStore.Catalago.Application.ViewModels;
using RestSharp;

namespace MyStore.WebApp.MVC2.Controllers
{
    public class VitrineController : Controller
    {   
        private readonly RestClient _restClient;

        public VitrineController(RestClient res)
        {            
            _restClient = res;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            var request = new RestRequest("Produto", Method.Get);
            var result = await _restClient.GetAsync<IEnumerable<ProdutoDto>>(request);
            return View(result);
            //return View(await _produtoAppService.ObterTodos());
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            var request = new RestRequest($"Produto/{id.ToString()}", Method.Get);
            var result = await _restClient.GetAsync<ProdutoDto>(request);
            return View(result);
            //return View(await _produtoAppService.ObterPorId(id));
        }
    }
}
