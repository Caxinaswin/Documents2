using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Iteração_3.Models
{
        public class Encomenda
        {
            public int EncomendaId { get; set; }
            public DateTime Data { get; set; }
            public ICollection<Produto> Produtos { get; set; }
        }
}