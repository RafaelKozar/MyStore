using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Core.DomainObjects
{
    public class ResponseObject
    {
        public string? ActionName { get; set; }
        public string? ControllerName { get; set; }
        public object? property { get; set; }
        public string? messages { get; set; }  
    }
}
