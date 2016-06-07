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
    public class TipoPagamentosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TipoPagamentos
        public IQueryable<TipoPagamento> GetTipoPagamentos()
        {
            return db.TipoPagamentos;
        }

        // GET: api/TipoPagamentos/5
        [ResponseType(typeof(TipoPagamento))]
        public async Task<IHttpActionResult> GetTipoPagamento(int id)
        {
            TipoPagamento tipoPagamento = await db.TipoPagamentos.FindAsync(id);
            if (tipoPagamento == null)
            {
                return NotFound();
            }

            return Ok(tipoPagamento);
        }

        // PUT: api/TipoPagamentos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTipoPagamento(int id, TipoPagamento tipoPagamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoPagamento.TipoPagamentoId)
            {
                return BadRequest();
            }
            db.Entry(tipoPagamento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoPagamentoExists(id))
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

        // POST: api/TipoPagamentos
        [ResponseType(typeof(TipoPagamento))]
        public async Task<IHttpActionResult> PostTipoPagamento(TipoPagamento tipoPagamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tipoPagamento.ApplicationUserId = User.Identity.GetUserId();
            db.TipoPagamentos.Add(tipoPagamento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipoPagamento.TipoPagamentoId }, tipoPagamento);
        }

        // DELETE: api/TipoPagamentos/5
        [ResponseType(typeof(TipoPagamento))]
        public async Task<IHttpActionResult> DeleteTipoPagamento(int id)
        {
            TipoPagamento tipoPagamento = await db.TipoPagamentos.FindAsync(id);
            if (tipoPagamento == null)
            {
                return NotFound();
            }

            db.TipoPagamentos.Remove(tipoPagamento);
            await db.SaveChangesAsync();

            return Ok(tipoPagamento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoPagamentoExists(int id)
        {
            return db.TipoPagamentos.Count(e => e.TipoPagamentoId == id) > 0;
        }
    }
}