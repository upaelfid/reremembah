using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Associations.Models
{

    public class CardDTO
    {
        public int Id { get; set; }
        public int DeckId { get; set; }
        public int CardNumber { get; set; }
        public string Subject { get; set; }
        public string SpecificSubject { get; set; }
        public string Author { get; set; }
        public string Facts { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public string fileInput { get; set; }
    }
}