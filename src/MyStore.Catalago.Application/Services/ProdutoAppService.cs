using AutoMapper;
using Microsoft.Data.SqlClient;
using MyStore.Catalago.Application.ViewModels;
using MyStore.Catalago.Data;
using MyStore.Catalago.Domain;
using MyStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Catalago.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly IEstoqueService _estoqueService;

        public ProdutoAppService(IProdutoRepository produtoRepository,
                              IMapper mapper,
                              IEstoqueService estoqueService)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _estoqueService = estoqueService;
        }

        public async Task<IEnumerable<ProdutoDto>> ObterPorCategoria(int codigo)
        {
            return _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterPorCategoria(codigo));
        }

        public async Task<ProdutoDto> ObterPorId(Guid id)
        {
            return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id));
        }

        //private static string db_source = "Data Source=ARTCWBINTDVC007\SQLEXPRESS; Inital Catalog=MyStoreDb";
        private static string db_source = "ARTCWBINTDVC007\\SQLEXPRESS";
        private static string db_user = "userk";
        private static string db_password = "Deus2@22";
        private static string db_database = "MyStoreDb";

        private SqlConnection GetConnection()
        {

            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;            
            _builder.UserID = db_user;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public async Task<IEnumerable<ProdutoDto>> ObterTodos()
        {
            //List<Produto> _product_lst = new List<Produto>();
            //string _statement = "SELECT * from Produtos";
            //SqlConnection _connection = GetConnection();

            //_connection.Open();

            //SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);

            //using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            //{
            //    while (_reader.Read())
            //    {
            //        Console.WriteLine(_reader.GetGuid(0));
            //        Console.WriteLine(_reader.GetGuid(1));
            //        Console.WriteLine(_reader.GetString(2));
            //    }
            //}
            //_connection.Close();
            //return null;

            return _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterTodos());
            //var ctx = new CatalagoContext();
        }

        public async Task<IEnumerable<CategoriaDto>> ObterCategorias()
        {
            return _mapper.Map<IEnumerable<CategoriaDto>>(await _produtoRepository.ObterCategorias());
        }

        public async Task AdicionarProduto(ProdutoDto ProdutoDto)
        {
            var produto = _mapper.Map<Produto>(ProdutoDto);
            _produtoRepository.Adicionar(produto);

            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task AtualizarProduto(ProdutoDto ProdutoDto)
        {
            var produto = _mapper.Map<Produto>(ProdutoDto);
            _produtoRepository.Atualizar(produto);

            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<ProdutoDto> DebitarEstoque(Guid id, int quantidade)
        {
            if (!_estoqueService.DebitarEstoque(id, quantidade).Result)
            {
                throw new DomainException("Falha ao debitar estoque");
            }

            return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id));
        }

        public async Task<ProdutoDto> ReporEstoque(Guid id, int quantidade)
        {
            if (!_estoqueService.ReporEstoque(id, quantidade).Result)
            {
                throw new DomainException("Falha ao repor estoque");
            }

            return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorId(id));
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
            _estoqueService?.Dispose();
        }
    }
}
