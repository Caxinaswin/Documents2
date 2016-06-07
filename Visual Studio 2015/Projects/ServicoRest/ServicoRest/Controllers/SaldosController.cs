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
    public class SaldosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Saldosasda
        public IQueryable<Saldo> GetSaldos()
        {
            return db.Saldos;
        }

        // GET: api/Saldos/5
        [ResponseType(typeof(Saldo))]
        public async Task<IHttpActionResult> GetSaldo(int id)
        {
            Saldo saldo = await db.Saldos.FindAsync(id);
            if (saldo == null)
            {
                return NotFound();
            }

            return Ok(saldo);
        }

        // PUT: api/Saldos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSaldo(int id, Saldo saldo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != saldo.SaldoId)
            {
                return BadRequest();
            }

            db.Entry(saldo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaldoExists(id))
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

        // POST: api/Saldos
        [ResponseType(typeof(Saldo))]
        public async Task<IHttpActionResult> PostSaldo(Saldo saldo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Saldos.Add(saldo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = saldo.SaldoId }, saldo);
        }

        // DELETE: api/Saldos/5
        [ResponseType(typeof(Saldo))]
        public async Task<IHttpActionResult> DeleteSaldo(int id)
        {
            Saldo saldo = await db.Saldos.FindAsync(id);
            if (saldo == null)
            {
                return NotFound();
            }

            db.Saldos.Remove(saldo);
            await db.SaveChangesAsync();

            return Ok(saldo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SaldoExists(int id)
        {
            return db.Saldos.Count(e => e.SaldoId == id) > 0;
        }
    }
}