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
    public class RendimentosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Rendimentos
        public IQueryable<Rendimento> GetRendimentos()
        {
            return db.Rendimentos;
        }

        // GET: api/Rendimentos/5
        [ResponseType(typeof(Rendimento))]
        public async Task<IHttpActionResult> GetRendimento(int id)
        {
            Rendimento rendimento = await db.Rendimentos.FindAsync(id);
            if (rendimento == null)
            {
                return NotFound();
            }

            return Ok(rendimento);
        }

        // PUT: api/Rendimentos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRendimento(int id, Rendimento rendimento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rendimento.RendimentoId)
            {
                return BadRequest();
            }

            db.Entry(rendimento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RendimentoExists(id))
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

        // POST: api/Rendimentos
        [ResponseType(typeof(Rendimento))]
        public async Task<IHttpActionResult> PostRendimento(Rendimento rendimento)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
 
            db.Rendimentos.Add(rendimento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = rendimento.RendimentoId }, rendimento);
        }

        // DELETE: api/Rendimentos/5
        [ResponseType(typeof(Rendimento))]
        public async Task<IHttpActionResult> DeleteRendimento(int id)
        {
            Rendimento rendimento = await db.Rendimentos.FindAsync(id);
            if (rendimento == null)
            {
                return NotFound();
            }

            db.Rendimentos.Remove(rendimento);
            await db.SaveChangesAsync();

            return Ok(rendimento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RendimentoExists(int id)
        {
            return db.Rendimentos.Count(e => e.RendimentoId == id) > 0;
        }
    }
}