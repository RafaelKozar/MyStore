using MyStore.Vendas.Application.Queries.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Vendas.Application.Queries
{
    public interface IPedidoQueries
    {
        Task<CarrinhoDto> ObterCarrinhoCliente(Guid clienteId);
        Task<IEnumerable<PedidoDto>> ObterPedidosCliente(Guid clienteId);
    }
}
