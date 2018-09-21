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
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Associations.Controllers
{
    public class CardsController : ApiController
    {
        private Db db = new Db();

        // GET: api/Cards
        public IQueryable<CardDTO> GetCards()
        {
            var card = from b in db.Cards
                       select new CardDTO()
                       {
                           Id = b.Id,
                           DeckId = b.DeckId,
                           CardNumber = b.CardNumber,
                           Subject = b.Subject,
                           SpecificSubject = b.SpecificSubject,
                           Author = b.Author,
                           Facts = b.Facts,
                           Question = b.Question,
                           Answer = b.Answer,
                           Category = b.Category,
                           Color = b.Color,
                           fileInput = b.fileInput
                       };
            return card;
        }


        // GET: api/Cards/5
        [ResponseType(typeof(CardDTO))]
        public async Task<IHttpActionResult> GetCard(int id)
        {
            var card = await db.Cards.Include(b => b.Category).Select(b =>
                new CardDTO()
                {
                    Id = b.Id,
                    DeckId = b.DeckId,
                    CardNumber = b.CardNumber,
                    Subject = b.Subject,
                    SpecificSubject = b.SpecificSubject,
                    Author = b.Author,
                    Facts = b.Facts,
                    Question = b.Question,
                    Answer = b.Answer,
                    Category = b.Category,
                    Color = b.Color
                }).SingleOrDefaultAsync(b => b.DeckId == id);
            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }

        // PUT: api/Cards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCard(int id, Card card)
        {


            if (card.fileInput != null)
            {
                SaveImage(card.fileInput, card.CardNumber.ToString(),"/UserDecks/"+card.Category + card.DeckId );
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != card.Id)
            {
                return BadRequest();
            }
            card.fileInput = "/UserDecks/"+card.Category+card.DeckId+"/"+card.CardNumber+".jpg";

            db.Entry(card).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        public bool SaveImage(string ImgStr, string ImgName, string PathName)
        {
            var regex = new Regex(@"data:(?<mime>[\w/\-\.]+);(?<encoding>\w+),(?<data>.*)", RegexOptions.Compiled);
            var match = regex.Match(ImgStr);
            var mime = match.Groups["mime"].Value;
            var encoding = match.Groups["encoding"].Value;
            var data = match.Groups["data"].Value;

            String path = HttpContext.Current.Server.MapPath("~"+ PathName); //Path
            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }
            string imageName = ImgName + ".jpg";
            //set the image path
            string imgPath = Path.Combine(path, imageName);
            byte[] imageBytes = Convert.FromBase64String(data);
            File.WriteAllBytes(imgPath, imageBytes);
            return true;
        }
        // POST: api/Cards
        [ResponseType(typeof(Card))]
        public async Task<IHttpActionResult> PostCard(Card card)
        {
            if (card.fileInput != null) {
                SaveImage(card.fileInput, card.CardNumber.ToString(), "/UserDecks/" + card.Category + card.DeckId);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            card.fileInput = "";
            db.Cards.Add(card);
            await db.SaveChangesAsync();
       
            return CreatedAtRoute("DefaultApi", new { id = card.Id }, card);
        }

        // DELETE: api/Cards/5
        [ResponseType(typeof(Card))]
        public IHttpActionResult DeleteCard(int id)
        {
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return NotFound();
            }

            db.Cards.Remove(card);
            db.SaveChanges();

            return Ok(card);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardExists(int id)
        {
            return db.Cards.Count(e => e.Id == id) > 0;
        }
    }
}