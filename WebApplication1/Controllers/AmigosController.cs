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
    public class AmigosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Amigos
        public ActionResult Index()
        {
            return View(db.Amigo.Where(x=> !x.Excluido).ToList());
        }

        // GET: Amigos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amigo amigo = db.Amigo.Find(id);
            if (amigo == null)
            {
                return HttpNotFound();
            }

            ViewBag.Emprestimos = db.Emprestimo.Where(x => x.AmigoId == id && !x.Excluido).ToList();

            return View(amigo);
        }

        // GET: Amigos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Amigos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,DataCadastro,Excluido")] Amigo amigo)
        {
            if (ModelState.IsValid)
            {
                amigo.DataCadastro = DateTime.Now;
                amigo.Liberado = true;
                db.Amigo.Add(amigo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amigo);
        }

        // GET: Amigos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amigo amigo = db.Amigo.Find(id);
            if (amigo == null)
            {
                return HttpNotFound();
            }
            return View(amigo);
        }

        // POST: Amigos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,DataCadastro,Excluido")] Amigo amigo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amigo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amigo);
        }

        // GET: Amigos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amigo amigo = db.Amigo.Find(id);
            if (amigo == null)
            {
                return HttpNotFound();
            }
            return View(amigo);
        }

        // POST: Amigos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Amigo amigo = db.Amigo.Find(id);
            amigo.Excluido = true;
            db.Entry(amigo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Liberar(int id)
        {
            Amigo amigo = db.Amigo.Find(id);
            amigo.Liberado = true;
            db.Entry(amigo).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { liberado = true });
        }
        [HttpPost]
        public ActionResult Bloquear (int id)
        {
            Amigo amigo = db.Amigo.Find(id);
            amigo.Liberado = false;
            db.Entry(amigo).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { bloqueado = true });
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
