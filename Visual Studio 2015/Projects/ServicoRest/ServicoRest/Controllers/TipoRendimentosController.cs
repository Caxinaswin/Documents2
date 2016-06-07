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
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;

namespace ServicoRest.Controllers
{
    public class TipoRendimentosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TipoRendimentos
        public IQueryable<TipoRendimento> GetTipoRendimentos()
        {
            return db.TipoRendimentos;
            
        }

        // GET: api/TipoRendimentos/5
        [ResponseType(typeof(TipoRendimento))]
        public async Task<IHttpActionResult> GetTipoRendimento(int id)
        {
            TipoRendimento tipoRendimento = await db.TipoRendimentos.FindAsync(id);
            if (tipoRendimento == null)
            {
                return NotFound();
            }

            return Ok(tipoRendimento);
        }

        // PUT: api/TipoRendimentos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTipoRendimento(int id, TipoRendimento tipoRendimento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoRendimento.TipoRendimentoId)
            {
                return BadRequest();
            }

            db.Entry(tipoRendimento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoRendimentoExists(id))
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

        // POST: api/TipoRendimentos
        [ResponseType(typeof(TipoRendimento))]
        public async Task<IHttpActionResult> PostTipoRendimentos(TipoRendimento tipoRendimento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.TipoRendimentos.Add(tipoRendimento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tipoRendimento.TipoRendimentoId }, tipoRendimento);
        }

        // DELETE: api/TipoRendimentos/5
        [ResponseType(typeof(TipoRendimento))]
        public async Task<IHttpActionResult> DeleteTipoRendimento(int id)
        {
            TipoRendimento tipoRendimento = await db.TipoRendimentos.FindAsync(id);
            if (tipoRendimento == null)
            {
                return NotFound();
            }

            db.TipoRendimentos.Remove(tipoRendimento);
            await db.SaveChangesAsync();

            return Ok(tipoRendimento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoRendimentoExists(int id)
        {
            return db.TipoRendimentos.Count(e => e.TipoRendimentoId == id) > 0;
        }
    }
}