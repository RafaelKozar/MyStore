using MyStore.Core.Messages.CommonMessages.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Catalago.Domain.Events
{
    public class ProdutoAbaixoEstoqueEvent : DomainEvent
    {
        public int QuantidadeRestante { get; private set; }    
        public ProdutoAbaixoEstoqueEvent(Guid aggregateId, int quantiadeRestante) : base(aggregateId)
        {
            QuantidadeRestante = quantiadeRestante;
        }
    }
}
