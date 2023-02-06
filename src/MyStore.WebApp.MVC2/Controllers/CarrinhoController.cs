using Microsoft.AspNetCore.Mvc;
using MyStore.Catalago.Application.Services;
using MyStore.Core.DomainObjects;
using MyStore.Vendas.Application.Queries;
using MyStore.Vendas.Application.Queries.DTOs;
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

        [Route("meu-carrinho")]
        public async Task<IActionResult> Index()
        {
            //meu - carrinho3
            var request = new RestRequest("Carrinho/meu-carrinho3", Method.Get);         
            var response = await _restClient.GetAsync<CarrinhoDto>(request);
            return View(response);
        }

        [HttpPost]
        [Route("remover-item")]
        public async Task<IActionResult> RemoverItem(Guid id)
        {
            var request = new RestRequest("Carrinho/remover-item", Method.Post);
            request.AddParameter("id", id);            
            var response = await _restClient.PostAsync<CarrinhoDto>(request);
            if(response == null) return RedirectToAction("Index");
            return View("Index", response);
        }

        [HttpPost]
        [Route("atualizar-item")]
        public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
        {
            var request = new RestRequest("Carrinho/atualizar-item", Method.Post);
            request.AddParameter("id", id);
            request.AddParameter("quantidade", quantidade);
            var response = await _restClient.PostAsync<CarrinhoDto>(request);
            if (response == null) return RedirectToAction("Index");
            return View("Index", response);
        }


        [HttpPost]
        [Route("aplicar-voucher")]
        public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
        {
            var request = new RestRequest("Carrinho/aplicar-voucher", Method.Post);            
            request.AddParameter("voucherCodigo", voucherCodigo);
            var response = await _restClient.PostAsync<CarrinhoDto>(request);
            if (response == null) return RedirectToAction("Index");
            return View("Index", response);
        }

        [Route("resumo-da-compra")]
        public async Task<IActionResult> ResumoDaCompra()
        {
            var request = new RestRequest("Carrinho/resumo-da-compra", Method.Get);
          
            return View(await _restClient.GetAsync<CarrinhoDto>(request));           
        }

        [HttpPost]
        [Route("iniciar-pedido")]
        public async Task<IActionResult> IniciarPedido(CarrinhoDto carrinho)
        {
            var request = new RestRequest("Carrinho/iniciar-pedido", Method.Post);
            request.AddJsonBody(carrinho);
            var response = await _restClient.PostAsync<CarrinhoDto>(request);
            if (response == null) return RedirectToAction("Index");
            return View("Index", response);
        }

    }
}
