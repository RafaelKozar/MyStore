using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Core.DomainObjects.DTO
{
    public class ListaProdutosPedido
    {
        public Guid PedidoId { get; set; }  
        public ICollection<Item> Itens { get; set; }
    }
}
