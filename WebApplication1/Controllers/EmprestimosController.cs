using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamesRente.Models;

namespace GamesRente.Controllers
{
    [Authorize]
    public class EmprestimosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Emprestimos
        public ActionResult Index()
        {
            var emprestimo = db.Emprestimo.Include(e => e.Amigo).Include(e => e.Jogo).Where(x=> !x.Excluido);
            return View(emprestimo.ToList());
        }
        
        // GET: Emprestimos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // GET: Emprestimos/Create
        public ActionResult Create()
        {
            ViewBag.AmigoId = new SelectList(db.Amigo.Where(x=> !x.Excluido && x.Liberado), "Id", "Nome");
            ViewBag.JogoId = new SelectList(db.Jogos.Where(x=> x.Liberado && !x.Excluido), "Id", "Nome");
            return View();
        }

        // POST: Emprestimos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Datacadastro,AmigoId,JogoId")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                Jogo jogo = db.Jogos.Find(emprestimo.JogoId);
                jogo.Liberado = false;
                emprestimo.Devolvido = false;
                emprestimo.Datacadastro = DateTime.Now;
                db.Emprestimo.Add(emprestimo);
                db.Entry(jogo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AmigoId = new SelectList(db.Amigo, "Id", "Nome", emprestimo.AmigoId);
            ViewBag.JogoId = new SelectList(db.Jogos, "Id", "Nome", emprestimo.JogoId);
            return View(emprestimo);
        }
        
        // GET: Emprestimos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            ViewBag.AmigoId = new SelectList(db.Amigo, "Id", "Nome", emprestimo.AmigoId);
            ViewBag.JogoId = new SelectList(db.Jogos, "Id", "Nome", emprestimo.JogoId);
            return View(emprestimo);
        }

        // POST: Emprestimos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Datacadastro,AmigoId,JogoId")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emprestimo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AmigoId = new SelectList(db.Amigo, "Id", "Nome", emprestimo.AmigoId);
            ViewBag.JogoId = new SelectList(db.Jogos, "Id", "Nome", emprestimo.JogoId);
            return View(emprestimo);
        }

        // GET: Emprestimos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            emprestimo.Excluido = true;
            db.Entry(emprestimo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Devolver(int id)
        {
            Emprestimo emprestimo = db.Emprestimo.Find(id);
            emprestimo.Devolvido = true;
            emprestimo.DataDevolucao = DateTime.Now;
            Jogo jogo = db.Jogos.Find(emprestimo.JogoId);
            jogo.Liberado = true;
            db.Entry(emprestimo).State = EntityState.Modified;
            db.Entry(jogo).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { devolvido = true });
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
