using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClientMvcProdutos.Models;

namespace ClientMvcProdutos.Controllers
{
    public class EncomendasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Encomendas
        public async Task<ActionResult> Index()
        {
            return View(await db.Encomendas.ToListAsync());
        }

        // GET: Encomendas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encomenda encomenda = await db.Encomendas.FindAsync(id);
            if (encomenda == null)
            {
                return HttpNotFound();
            }
            return View(encomenda);
        }

        // GET: Encomendas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Encomendas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EncomendaId,Data")] Encomenda encomenda)
        {
            if (ModelState.IsValid)
            {
                db.Encomendas.Add(encomenda);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(encomenda);
        }

        // GET: Encomendas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encomenda encomenda = await db.Encomendas.FindAsync(id);
            if (encomenda == null)
            {
                return HttpNotFound();
            }
            return View(encomenda);
        }

        // POST: Encomendas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "EncomendaId,Data")] Encomenda encomenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encomenda).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(encomenda);
        }

        // GET: Encomendas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encomenda encomenda = await db.Encomendas.FindAsync(id);
            if (encomenda == null)
            {
                return HttpNotFound();
            }
            return View(encomenda);
        }

        // POST: Encomendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Encomenda encomenda = await db.Encomendas.FindAsync(id);
            db.Encomendas.Remove(encomenda);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
