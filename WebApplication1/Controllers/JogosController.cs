using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamesRente.Models;
using Newtonsoft.Json;

namespace GamesRente.Controllers
{
    [Authorize]
    public class JogosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jogos
        public ActionResult Index()
        {
            var amigos = db.Amigo.Where(x => !x.Excluido && x.Liberado).Select(x => new {x.Id, x.Nome }).ToList();
            
            var listaAmigos = string.Empty;
            var count = 0;
            foreach (var item in amigos)
            {
                if(count == amigos.Count)
                {
                    listaAmigos += (string.Format("'{0}':'{1}'", item.Id, item.Nome));
                }else if (count == 0)
                {
                    listaAmigos += (string.Format("'{0}':'{1}'", item.Id, item.Nome));
                }
                else
                {
                    listaAmigos += (string.Format(",'{0}':'{1}'", item.Id, item.Nome));
                }
                count++;
            }
            ViewBag.Amigos = listaAmigos;
            
            return View(db.Jogos.Where(x=> !x.Excluido).ToList());
        }

        // GET: Jogos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogo jogo = db.Jogos.Find(id);
            if (jogo == null)
            {
                return HttpNotFound();
            }

            ViewBag.Emprestimos = db.Emprestimo.Where(x => x.JogoId == id && !x.Excluido).ToList();
            return View(jogo);
        }

        // GET: Jogos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jogos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                jogo.DataCadastro = DateTime.Now;
                jogo.Liberado = true;
                jogo.Excluido = false;
                db.Jogos.Add(jogo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jogo);
        }

        // GET: Jogos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogo jogo = db.Jogos.Find(id);
            if (jogo == null)
            {
                return HttpNotFound();
            }
            return View(jogo);
        }

        // POST: Jogos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jogo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jogo);
        }

        // GET: Jogos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogo jogo = db.Jogos.Find(id);
            if (jogo == null)
            {
                return HttpNotFound();
            }
            return View(jogo);
        }

        [HttpPost]
        public ActionResult Devolver(int id)
        {
            Jogo jogo = db.Jogos.Find(id);
            jogo.Liberado = true;
            Emprestimo emprestimo = db.Emprestimo.Where(x => x.JogoId == id && !x.Devolvido).FirstOrDefault();
            emprestimo.Devolvido = true;
            emprestimo.DataDevolucao = DateTime.Now;
            db.Entry(emprestimo).State = EntityState.Modified;
            db.Entry(jogo).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { devolvido = true });
        }
        [HttpPost]
        public ActionResult EmprestimoRapido(int id, int amigoId)
        {
            Jogo jogo = db.Jogos.Find(id);
            Amigo amigo = db.Amigo.Find(amigoId);
            Emprestimo emprestimo = new Emprestimo()
            {
                Datacadastro = DateTime.Now,
                Amigo = amigo,
                AmigoId = amigo.Id,
                Jogo = jogo,
                JogoId = jogo.Id,
                Devolvido = false,
                Excluido = false
            };
            jogo.Liberado = false;
            db.Entry(jogo).State = EntityState.Modified;
            db.Emprestimo.Add(emprestimo);
            db.SaveChanges();
            return Json(new { emprestado = true });
        }
        

        // POST: Jogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jogo jogo = db.Jogos.Find(id);
            db.Jogos.Remove(jogo);
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
