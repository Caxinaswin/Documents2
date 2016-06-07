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
using Iteração_3.Models;

namespace Iteração_3.Controllers
{
    public class EncomendasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Encomendas
        public IQueryable<Encomenda> GetEncomendas()
        {
            return db.Encomendas;
        }

        // GET: api/Encomendas/5
        [ResponseType(typeof(Encomenda))]
        public async Task<IHttpActionResult> GetEncomenda(int id)
        {
            Encomenda encomenda = await db.Encomendas.FindAsync(id);
            if (encomenda == null)
            {
                return NotFound();
            }

            return Ok(encomenda);
        }

        // PUT: api/Encomendas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEncomenda(int id, Encomenda encomenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != encomenda.EncomendaId)
            {
                return BadRequest();
            }

            db.Entry(encomenda).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncomendaExists(id))
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

        // POST: api/Encomendas
        [ResponseType(typeof(Encomenda))]
        public async Task<IHttpActionResult> PostEncomenda(Encomenda encomenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Encomendas.Add(encomenda);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = encomenda.EncomendaId }, encomenda);
        }

        // DELETE: api/Encomendas/5
        [ResponseType(typeof(Encomenda))]
        public async Task<IHttpActionResult> DeleteEncomenda(int id)
        {
            Encomenda encomenda = await db.Encomendas.FindAsync(id);
            if (encomenda == null)
            {
                return NotFound();
            }

            db.Encomendas.Remove(encomenda);
            await db.SaveChangesAsync();

            return Ok(encomenda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EncomendaExists(int id)
        {
            return db.Encomendas.Count(e => e.EncomendaId == id) > 0;
        }
    }
}