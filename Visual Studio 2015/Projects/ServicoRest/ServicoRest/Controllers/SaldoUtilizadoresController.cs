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
    public class SaldoUtilizadoresController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SaldoUtilizadores
        public IQueryable<SaldoUtilizador> GetSaldoUtilizador()
        {
            return db.SaldoUtilizador;
        }

        // GET: api/SaldoUtilizadores/5
        [ResponseType(typeof(SaldoUtilizador))]
        public async Task<IHttpActionResult> GetSaldoUtilizador(int id)
        {
            SaldoUtilizador saldoUtilizador = await db.SaldoUtilizador.FindAsync(id);
            if (saldoUtilizador == null)
            {
                return NotFound();
            }

            return Ok(saldoUtilizador);
        }

        // PUT: api/SaldoUtilizadores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSaldoUtilizador(int id, SaldoUtilizador saldoUtilizador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != saldoUtilizador.Id)
            {
                return BadRequest();
            }

            db.Entry(saldoUtilizador).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaldoUtilizadorExists(id))
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

        // POST: api/SaldoUtilizadores
        [ResponseType(typeof(SaldoUtilizador))]
        public async Task<IHttpActionResult> PostSaldoUtilizador(SaldoUtilizador saldoUtilizador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SaldoUtilizador.Add(saldoUtilizador);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = saldoUtilizador.Id }, saldoUtilizador);
        }

        // DELETE: api/SaldoUtilizadores/5
        [ResponseType(typeof(SaldoUtilizador))]
        public async Task<IHttpActionResult> DeleteSaldoUtilizador(int id)
        {
            SaldoUtilizador saldoUtilizador = await db.SaldoUtilizador.FindAsync(id);
            if (saldoUtilizador == null)
            {
                return NotFound();
            }

            db.SaldoUtilizador.Remove(saldoUtilizador);
            await db.SaveChangesAsync();

            return Ok(saldoUtilizador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SaldoUtilizadorExists(int id)
        {
            return db.SaldoUtilizador.Count(e => e.Id == id) > 0;
        }
    }
}