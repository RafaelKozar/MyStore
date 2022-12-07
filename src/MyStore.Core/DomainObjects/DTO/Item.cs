using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Core.DomainObjects.DTO
{
    public class Item
    {
        public Guid Id { get; set; } 
        public int Quantidade { get; set; } 
    }
}
