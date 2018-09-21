using System.ComponentModel.DataAnnotations;

namespace Associations.Models
{
    public class Deck
    {
        [Key]
        public int Id { get; set; }
        public string DeckName { get; set; }
    }
}