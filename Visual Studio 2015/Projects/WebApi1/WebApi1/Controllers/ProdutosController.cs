using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi1.Models;

namespace WebApi1.Controllers
{
    public class ProdutosController : ApiController
    {
        public static List<Produto> produtos = new List<Produto>()
        {
            new Produto() {Id = 1,Nome = "Produto 1",Descricao = "Descricao do Produto" },
            new Produto() {Id = 2,Nome = "Produto 1",Descricao = "Descricao do Produto" },
            new Produto() {Id = 3,Nome = "Produto 1",Descricao = "Descricao do Produto" },
            new Produto() {Id = 4,Nome = "Produto 1",Descricao = "Descricao do Produto" },
            new Produto() {Id = 5,Nome = "Produto 1",Descricao = "Descricao do Produto" }

        };

        // GET: api/Produtos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Produtos/5
        public Produto Get(int id)
        {
            Produto prod = produtos.Find(p => p.Id == id);
            if (prod == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            else return prod;
        }

        // POST: api/Produtos
        public HttpResponseMessage Post([FromBody]Produto p)
        {
            if (p == null) return new HttpResponseMessage(HttpStatusCode.BadRequest);
            produtos.Add(p);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        // PUT: api/Produtos/5
        public HttpResponseMessage Put(int id, [FromBody]Produto value)
        {
            Produto prod = produtos.Find(p => p.Id == id);
            if (prod == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            prod.Id = id;
            prod.Nome = prod.Nome;
            prod.Descricao = prod.Descricao;
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        // DELETE: api/Produtos/5
        public void Delete(int id)
        {
            Produto prod = produtos.Find(p => p.Id == id);
            if (prod == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            produtos.Remove(prod);
        }
    }
}
