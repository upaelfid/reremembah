using System.Data.Entity;

namespace Associations.Models
{
    public class Db : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Deck> Decks { get; set; }
    }
}