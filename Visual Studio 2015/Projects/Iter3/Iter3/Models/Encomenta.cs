using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iter3.Models
{
    public class Encomenta
    {
        public int EncomendaId { get; set; }
        public DateTime Data { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}