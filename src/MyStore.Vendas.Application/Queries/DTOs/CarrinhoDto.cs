using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Vendas.Application.Queries.DTOs
{
    public class CarrinhoDto
    {
        public Guid PedidoId { get; set; }
        public Guid ClienteId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorDesconto { get; set; }
        public string VoucherCodigo { get; set; }

        public List<CarrinhoItemDto> Items { get; set; } = new List<CarrinhoItemDto>();
        public CarrinhoPagamentoDto Pagamento { get; set; }   

        public string Messages { get; set; }    
    }
}
