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
    public class AlternateDecksController : Controller
    {
        private Db db = new Db();

        // GET: AlternateDecks
        public async Task<ActionResult> Index()
        {
            return View(await db.Decks.ToListAsync());
        }

        // GET: AlternateDecks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = await db.Decks.FindAsync(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // GET: AlternateDecks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlternateDecks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,DeckName")] Deck deck)
        {
            if (ModelState.IsValid)
            {
                db.Decks.Add(deck);
                await db.SaveChangesAsync();


                for (var i = 1; i < 51; i++)
                {
                    var cardyB = new Card();
                    cardyB.CardNumber = i;
                    cardyB.Category = deck.DeckName;
                    cardyB.DeckId = deck.Id;
                    db.Cards.Add(cardyB);
                    await db.SaveChangesAsync();
                }

                //Add directory
                String path = Server.MapPath("~/UserDecks/" + deck.DeckName + deck.Id);

                //Check if directory exist
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }


                return RedirectToAction("Index");
            }





            return View(deck);
        }

        // GET: AlternateDecks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = await db.Decks.FindAsync(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // POST: AlternateDecks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,DeckName")] Deck deck)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deck).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(deck);
        }

        // GET: AlternateDecks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deck deck = await db.Decks.FindAsync(id);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }

        // POST: AlternateDecks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Deck deck = await db.Decks.FindAsync(id);
            db.Decks.Remove(deck);
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
