﻿using CrowdTouring_Projeto.ViewModel;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CrowdTouring_Projeto.Models
{
    public class DesafiosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Desafios
        public ActionResult Index()
        {
         

            var desafios = db.Desafios.Include(d => d.TipoAvaliacao).Include(d => d.User).Include(d => d.Tags);
            var count = db.Desafios.Count();
            carregaInformacaoUtilizador();
            carregaRecentes(desafios,count);
            carregaFiltros();

            return View(desafios.ToList());
        }

        private void carregaInformacaoUtilizador()
        {
            var userId = User.Identity.GetUserId();

            var utilizador = db.Users.Where(m => m.Id == userId).FirstOrDefault();
            PerfilUtilizador perfilUtilizador = new PerfilUtilizador();
            perfilUtilizador.tags = utilizador.Tags;
            perfilUtilizador.pontos = utilizador.pontos;
            perfilUtilizador.path = utilizador.ImagePath;
            ViewBag.User = perfilUtilizador;
        }

        private void carregaRecentes(IQueryable<Desafio> desafios,int count)
        {
            var numeroDesafios = 5;
            if (count < 5)
            {
                numeroDesafios = count;
            }
            ViewBag.Recentes = (from d in desafios
                                orderby d.DataCriacao descending
                                select d).Take(numeroDesafios);
        }


        private void carregaFiltros()
        {
            List<SelectListItem> filtro = new List<SelectListItem>();
            filtro.Add(new SelectListItem
            {
                Text = "Recentes",
                Value = "1"
            });
            filtro.Add(new SelectListItem
            {
                Text = "Mais Vistas",
                Value = "2",
                Selected = true
            });
            filtro.Add(new SelectListItem
            {
                Text = "Valor monetário",
                Value = "3"
            });
            filtro.Add(new SelectListItem
            {
                Text = "Prazo a expirar",
                Value = "4"
            });

            ViewBag.filtro = filtro;
        }

        // GET: Desafios/Details/5
        public ActionResult Details(int? id)
        {
            var userId = User.Identity.GetUserId();
            var desafioVisualizacoes = db.Desafios.Where(d => d.DesafioId == id && d.ApplicationUserId != userId).FirstOrDefault();
            System.Diagnostics.Debug.WriteLine(desafioVisualizacoes);
            if (desafioVisualizacoes != null)
            {
                desafioVisualizacoes.Visualizacoes++;
                db.Entry(desafioVisualizacoes).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desafio desafio = db.Desafios.Find(id);
            if (desafio == null)
            {
                return HttpNotFound();
            }
            return View(desafio);
        }

        private void preencherTagDesafio()
        {
            var tags = db.Tags;
            var viewModel = new List<TagDesafio>();
            foreach (var tag in tags)
            {
                viewModel.Add(new TagDesafio
                {
                    TagId = tag.Id,
                    Nome = tag.NomeTag,
                    Seleccionado = false
                });
            }
            ViewBag.TagsDesafio = viewModel;
        }

        private void preencherTagDesafio(Desafio desafio)
        {
            var tags = db.Tags;
            var tagsUsers = new HashSet<int>(desafio.Tags.Select(a => a.Id));
            var viewModel = new List<TagDesafio>();
            foreach (var tag in tags)
            {
                viewModel.Add(new TagDesafio
                {
                    TagId = tag.Id,
                    Nome = tag.NomeTag,
                    Seleccionado = tagsUsers.Contains(tag.Id)
                });
            }
            ViewBag.TagsDesafio = viewModel;
        }

        // GET: Desafios/Create
        public ActionResult Create()
        {

            var tags = db.Tags.ToList();
            var DesafioCreate = new DesafioCreate();
            preencherTagDesafio();
            return View(DesafioCreate);
        }

        // POST: Desafios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DesafioCreate model, string[] selectedTag,HttpPostedFileBase file)
        {
            TipoAvaliacao TipoAvaliacao = db.TiposAvaliacao.Where(i => i.TipoAvaliacaoId == 1).FirstOrDefault();
            System.Diagnostics.Debug.WriteLine(model.lat);
            if (ModelState.IsValid)
            {
                Desafio desafio = new Desafio();
                Anexo anexo = new Anexo();
                desafio.lat = model.lat;
                desafio.lon = model.lon;
                desafio.TipoTrabalho = model.TipoTrabalho;
                desafio.Descricao = model.Descricao;
                desafio.DataCriacao = DateTime.Now;
                desafio.TipoAvaliacao = TipoAvaliacao;
                desafio.valor = model.ValorMonetario;
                desafio.ApplicationUserId = User.Identity.GetUserId();

                var listaTags = atualizarTagsDesafio(desafio, selectedTag);
                desafio.Tags = listaTags;


                db.Desafios.Add(desafio);

                db.SaveChanges();

                if (file.ContentLength > 0)
                {
                    string filePath = Path.Combine(HttpContext.Server.MapPath("~/Anexos/"),
                                                   Path.GetFileName(file.FileName));
                    file.SaveAs(filePath);
                    anexo.Caminho = filePath;
                    anexo.DesafioId = desafio.DesafioId;
                    anexo.NomeFicheiro = file.FileName;
                    db.Anexos.Add(anexo);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            preencherTagDesafio();
            return View(model);
        }

        [HttpPost]
        public ActionResult listaDesafiosTag(string listaTags)
        {
            System.Diagnostics.Debug.WriteLine(listaTags);
            var desafios = db.Desafios.Include(d => d.TipoAvaliacao).Include(d => d.User).Include(d => d.Tags);
            var count = db.Desafios.Count();
            carregaInformacaoUtilizador();
            carregaRecentes(desafios,count);
            carregaFiltros();

            return View("Index",desafios.ToList());
        }

        [HttpPost]
        public ActionResult filtrarDesafios(int filtro)
        {

            
            var desafios = db.Desafios.Include(d => d.TipoAvaliacao).Include(d => d.User).Include(d => d.Tags);
            var count = db.Desafios.Count();
            carregaInformacaoUtilizador();
            carregaRecentes(desafios, count);
            carregaFiltros();

            if(filtro == 1)
            {
                desafios = db.Desafios.Include(d => d.TipoAvaliacao).Include(d => d.User).Include(d => d.Tags).OrderByDescending(d => d.DataCriacao);
            }

            if(filtro == 2)
            {
                desafios = db.Desafios.Include(d => d.TipoAvaliacao).Include(d => d.User).Include(d => d.Tags).OrderByDescending(d => d.Visualizacoes);
            }
            if(filtro == 3)
            {

                desafios = db.Desafios.Include(d => d.TipoAvaliacao).Include(d => d.User).Include(d => d.Tags).OrderByDescending(d => d.valor);

            }
            if (filtro == 4)
            {

            }

            return View("Index", desafios.ToList());
        }

        private ICollection<Tag> atualizarTagsDesafio(Desafio desafio,
   string[] selectedTag)
        {
            ICollection<Tag> listaTags;
            var selectedTagsHS = new HashSet<string>(selectedTag);
            var Tags = db.Tags.ToList();
            listaTags = new List<Tag>();
            foreach (var Tag in Tags)
            {
                if (selectedTagsHS.Contains(Tag.Id.ToString()))
                {
                    listaTags.Add(Tag);
                    Tag.Desafios.Add(desafio);

                }
            }
            return listaTags;
        }

        // GET: Desafios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desafio desafio = db.Desafios.Find(id);
            if (desafio == null)
            {
                return HttpNotFound();
            }
            return View(desafio);
        }

        // POST: Desafios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DesafioId,TipoTrabalho,ApplicationUserId,Descricao,TipoAvaliacaoId,valor,Visualizacoes,DataCriacao,lat,lon,IdSolucaoVencedora")] Desafio desafio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(desafio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(desafio);
        }

        // GET: Desafios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desafio desafio = db.Desafios.Find(id);
            if (desafio == null)
            {
                return HttpNotFound();
            }
            return View(desafio);
        }

        // POST: Desafios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Desafio desafio = db.Desafios.Find(id);
            db.Desafios.Remove(desafio);
            db.SaveChanges();
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
