using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ServicoRest.Models;

namespace ServicoRest.Controllers
{
    public class DespesasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Despesas
        public IQueryable<Despesa> GetDespesas()
        {
            return db.Despesas;
        }

        // GET: api/Despesas/5
        [ResponseType(typeof(Despesa))]
        public async Task<IHttpActionResult> GetDespesa(int id)
        {
            Despesa despesa = await db.Despesas.FindAsync(id);
            if (despesa == null)
            {
                return NotFound();
            }

            return Ok(despesa);
        }

        // PUT: api/Despesas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDespesa(int id, Despesa despesa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != despesa.DespesaId)
            {
                return BadRequest();
            }

            db.Entry(despesa).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DespesaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Despesas
        [ResponseType(typeof(Despesa))]
        public async Task<IHttpActionResult> PostDespesa(Despesa despesa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Despesas.Add(despesa);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = despesa.DespesaId }, despesa);
        }

        // DELETE: api/Despesas/5
        [ResponseType(typeof(Despesa))]
        public async Task<IHttpActionResult> DeleteDespesa(int id)
        {
            Despesa despesa = await db.Despesas.FindAsync(id);
            if (despesa == null)
            {
                return NotFound();
            }

            db.Despesas.Remove(despesa);
            await db.SaveChangesAsync();

            return Ok(despesa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DespesaExists(int id)
        {
            return db.Despesas.Count(e => e.DespesaId == id) > 0;
        }
    }
}