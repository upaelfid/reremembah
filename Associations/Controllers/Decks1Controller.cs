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
using Associations.Models;
using System.Web;

namespace Associations.Controllers
{
    public class Decks1Controller : ApiController
    {
        private Db db = new Db();

        // GET: api/Decks1
        public IQueryable<DeckDTO> GetDecks()
        {
            var deck = from b in db.Decks
                       select new DeckDTO()
                       {
                           Id = b.Id,
                           DeckName = b.DeckName
                       };
            return deck;
        }

        // GET: api/Decks1/5
        [ResponseType(typeof(Deck))]
        public async Task<IHttpActionResult> GetDeck(int id)
        {
            var deck = await db.Decks.Include(b => b.DeckName).Select(b =>
                new DeckDTO()
                {
                    Id = b.Id,
                    DeckName = b.DeckName
                }).SingleOrDefaultAsync(b => b.Id == id);

            if (deck == null)
            {
                return NotFound();
            }

            return Ok(deck);
        }

        // PUT: api/Decks1/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDeck(int id, Deck deck)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deck.Id)
            {
                return BadRequest();
            }

            db.Entry(deck).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeckExists(id))
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

        // POST: api/Decks1
        [ResponseType(typeof(Deck))]
        public async Task<IHttpActionResult> PostDeck(Deck deck)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //add deck
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
            String path = HttpContext.Current.Server.MapPath("~/UserDecks/" + deck.DeckName + deck.Id); //Path
            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            //return Redirect(new Uri(Url.Action("Index", "Home", new { foo = "bar" })));
            return CreatedAtRoute("DefaultApi", new { id = deck.Id }, deck);

        }

        // DELETE: api/Decks1/5
        [ResponseType(typeof(Deck))]
        public async Task<IHttpActionResult> DeleteDeck(int id)
        {
            Deck deck = await db.Decks.FindAsync(id);
            if (deck == null)
            {
                return NotFound();
            }

            db.Decks.Remove(deck);
            await db.SaveChangesAsync();

            return Ok(deck);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DeckExists(int id)
        {
            return db.Decks.Count(e => e.Id == id) > 0;
        }
    }
}