using Microsoft.AspNetCore.Mvc;
using MyStore.Catalago.Application.Services;
using MyStore.Core.DomainObjects;
using RestSharp;
using System.Text;

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

            Dictionary<string, string> obj = new Dictionary<string, string>();

            //var parms = new Dictionary<string, string>
            //    {
            //        { "id",id.ToString() },
            //        { "messages","kjfsjdfkl"}
            //    };

            obj.Add("id", id.ToString());
            if (response.messages != null)
                obj.Add("messages", response.messages);

            var builder = new StringBuilder();

            foreach (var key in obj.Keys)
            {
                builder.Append($"{key}={obj[key]}").Append("&");
            }

            var str = builder.ToString();
            str = str.Remove(str.Length - 1);            
            var uri = new Uri($"https://localhost:7088/produto-detalhe?{str}");
            var u = uri.AbsoluteUri;

            return Redirect(u);
           
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
