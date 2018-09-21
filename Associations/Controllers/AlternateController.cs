using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Associations.Models;

namespace Associations.Controllers
{
    public class AlternateController : Controller
    {
        private Db db = new Db();

        // GET: Alternate
        public async Task<ActionResult> Index()
        {
            return View(await db.Cards.ToListAsync());
        }
        // GET: Alternate/Deck

            public ActionResult Deck(int? id)
        {
            var model = from r in db.Cards
                        where r.DeckId == id
                        select r;
            return View(model);
        }


        // GET: Alternate
        public ActionResult IndexDeck(int? id)
        {
            var model = from r in db.Cards
                        where r.DeckId == id
                        select r;
            return View(model);
        }

        // GET: Alternate/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = await db.Cards.FindAsync(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // GET: Alternate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alternate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DeckId,CardNumber,Subject,SpecificSubject,Author,Facts,Question,Answer,Category,Color,fileInput")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Cards.Add(card);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(card);
        }

        // GET: Alternate/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = await db.Cards.FindAsync(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: Alternate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DeckId,CardNumber,Subject,SpecificSubject,Author,Facts,Question,Answer,Category,Color,fileInput")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(card);
        }



        // GET: Alternate/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = await db.Cards.FindAsync(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: Alternate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Card card = await db.Cards.FindAsync(id);
            db.Cards.Remove(card);
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
