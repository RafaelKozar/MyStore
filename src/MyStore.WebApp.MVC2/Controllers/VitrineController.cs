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
        [Route("produto-detalhe")]
        public async Task<IActionResult> ProdutoDetalhe(Dictionary<string, string> valuePairs)
        {            
            Console.WriteLine("\\n\\n\\n");
            var request = new RestRequest($"Produto/{valuePairs["id"]}", Method.Get);
            var result = await _restClient.GetAsync<ProdutoDto>(request);            
            if (valuePairs.ContainsKey("messages"))
                result.messages = valuePairs["messages"];

            return View(result);
           
        }
    }
}
