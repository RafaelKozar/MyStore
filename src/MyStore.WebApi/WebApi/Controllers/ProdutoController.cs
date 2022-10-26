using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStore.Catalago.Application.Services;
using MyStore.Catalago.Application.ViewModels;
using MyStore.Catalago.Data;
using MyStore.Catalago.Domain;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : Controller
    {
        //private readonly CatalagoContext _context;
        IProdutoAppService _produtoAppService;
        //IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoAppService produtoAppService, IProdutoRepository produtoRepository)
        {
            _produtoAppService = produtoAppService;
            //_produtoRepository = produtoRepository
        }

        [HttpGet]
        public async Task<IEnumerable<ProdutoDto>> Gets()
        {
            return await _produtoAppService.ObterTodos();             
        }

        [Route("ObterCategorias")]
        [HttpGet]
        public async Task<IEnumerable<CategoriaDto>> ObterCategorias()
        {
            return await _produtoAppService.ObterCategorias();
        }

        
        [HttpPost]
        public async Task AdicionarProduto([FromBody] ProdutoDto produtoDto)
        {
            await _produtoAppService.AdicionarProduto(produtoDto);
        }

        [HttpGet("{id}")]
        public async Task<ProdutoDto> ObterPorIdProduto(string id)
        {
            return await _produtoAppService.ObterPorId(Guid.Parse(id));
        }

        [Route("AtualizarProduto")]
        [HttpPost]
        public async Task AtualizarProduto([FromBody] ProdutoDto produtoDto)
        {
            await _produtoAppService.AtualizarProduto(produtoDto);
        }

        [Route("ReporEstoque")]
        [HttpGet]
        public async Task ReporEstoque(Guid id, int quantidade)
        {
           await _produtoAppService.ReporEstoque(id, quantidade);
        }

        [Route("DebitarEstoque")]
        [HttpGet]
        public async Task DebitarEstoque(Guid id, int quantidade)
        {
            await _produtoAppService.DebitarEstoque(id, quantidade);
        }
    }
}
