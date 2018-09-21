namespace Associations.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using Associations.Models;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Associations.Models.Db>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Db";
        }

        protected override void Seed(Associations.Models.Db context)
        {
            //  This method will be called after migrating to the latest version.
            //new Card { Id = 1, CardNumber = 1, Category = "Groceries", Color = "Blue" },
            //new Card { Id = 2, CardNumber = 2, Category = "Toys", Color = "Blue" },
            //new Card { Id = 3, CardNumber = 3, Category = "Hardware", Color = "Blue" }
        //            public int CardNumber { get; set; }
        //public string Subject { get; set; }
        //public string SpecificSubject { get; set; }
        //public string Author { get; set; }
        //public string Facts { get; set; }
        //public string Question { get; set; }
        //public string Answer { get; set; }
        //public string Category { get; set; }
        //public string Color { get; set; }
        context.Cards.AddOrUpdate(
              new Card
              {
                  CardNumber = 1,
                  DeckId = 1,
                  Subject ="",
                  SpecificSubject="",
                  Author="",
                  Facts="",
                  Question="",
                  Answer="",
                  Category = "Human Anatomy",
                  Color = "Blue",
                  fileInput = ""
              },
              new Card
              {
                  CardNumber = 1,
                  DeckId = 1,
                  Subject = "",
                  SpecificSubject = "",
                  Author = "",
                  Facts = "",
                  Question = "",
                  Answer = "",
                  Category = "Human Anatomy",
                  Color = "Blue",
                  fileInput = ""
              },
              new Card
              {
                  CardNumber = 1,
                  DeckId = 1,
                  Subject = "",
                  SpecificSubject = "",
                  Author = "",
                  Facts = "",
                  Question = "",
                  Answer = "",
                  Category = "Human Anatomy",
                  Color = "Blue",
                  fileInput = ""
              }
            );
            context.Decks.AddOrUpdate(
              new Deck
              {
                  DeckName = "FourFive"
              }
              );
        }
    }
}
