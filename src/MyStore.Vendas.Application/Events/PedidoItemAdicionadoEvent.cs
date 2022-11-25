using MyStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Vendas.Application.Events
{
    public class PedidoItemAdicionadoEvent : Event
    {
        public Guid ClientId { get; private set; }
        public Guid PedidoId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; } 
        public decimal ValorUnitiraio { get; private set; } 
        public int Quantidade { get; private set; }

        public PedidoItemAdicionadoEvent(Guid clientId, Guid pedidoId, Guid produtoId, decimal valorUnitiraio, int quantidade, string produtoNome)
        {
            AggregateId = pedidoId;
            ClientId = clientId;
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            ValorUnitiraio = valorUnitiraio;
            Quantidade = quantidade;
            ProdutoNome = produtoNome;
        }
    }
}
