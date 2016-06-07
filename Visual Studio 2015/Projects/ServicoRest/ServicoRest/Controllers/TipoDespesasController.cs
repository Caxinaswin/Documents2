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
using Microsoft.AspNet.Identity;

namespace ServicoRest.Controllers
{
    public class TipoDespesasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TipoDespesas
        public IQueryable<TipoDespesa> GetTipoDespesas()
        {
            return db.TipoDespesas;
        }

        // GET: api/TipoDespesas/5
        [ResponseType(typeof(TipoDespesa))]
        public async Task<IHttpActionResult> GetTipoDespesa(int id)
        {
            TipoDespesa tipoDespesa = await db.TipoDespesas.FindAsync(id);
            if (tipoDespesa == null)
            {
                return NotFound();
            }

            return Ok(tipoDespesa);
        }

        // PUT: api/TipoDespesas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTipoDespesa(int id, TipoDespesa tipoDespesa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoDespesa.TipoDespesaId)
            {
                return BadRequest();
            }

            db.Entry(tipoDespesa).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoDespesaExists(id))
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

        // POST: api/TipoDespesas
        [ResponseType(typeof(TipoDespesa))]
        public async Task<IHttpActionResult> PostTipoDespesa(TipoDespesa tipoDespesa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipoDespesa.TipoDespesaId }, tipoDespesa);
        }

        // DELETE: api/TipoDespesas/5
        [ResponseType(typeof(TipoDespesa))]
        public async Task<IHttpActionResult> DeleteTipoDespesa(int id)
        {
            TipoDespesa tipoDespesa = await db.TipoDespesas.FindAsync(id);
            if (tipoDespesa == null)
            {
                return NotFound();
            }

            db.TipoDespesas.Remove(tipoDespesa);
            await db.SaveChangesAsync();

            return Ok(tipoDespesa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoDespesaExists(int id)
        {
            return db.TipoDespesas.Count(e => e.TipoDespesaId == id) > 0;
        }
    }
}