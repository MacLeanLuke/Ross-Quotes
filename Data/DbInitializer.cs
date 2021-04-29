using RossQuotes.Data;
using RossQuotes.Models;
using System;
using System.Linq;

namespace RossQuotes.Data
{
    public static class DbInitializer
    {
        public static void Initialize(QuoteContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Authors.Any())
            {
                return;   // DB has been seeded
            }

            var authors = new Author[]
            {
                new Author{AuthorLastName="Carson",AuthorFirstName="Alexander"},
                new Author{AuthorLastName="Meredith",AuthorFirstName="Alonso"},
                new Author{AuthorLastName="Arturo",AuthorFirstName="Anand"},
                new Author{AuthorLastName="Gytis",AuthorFirstName="Barzdukas"},
                new Author{AuthorLastName="Yan",AuthorFirstName="Li"},
                new Author{AuthorLastName="Peggy",AuthorFirstName="Justice"},
                new Author{AuthorLastName="Laura",AuthorFirstName="Norman"},
                new Author{AuthorLastName="Nino",AuthorFirstName="Olivetto"}
            };

            context.Authors.AddRange(authors);
            context.SaveChanges();

            var publishers = new Publisher[]
            {
                new Publisher{PublisherName="Penguin Random House"},
                new Publisher{PublisherName="Hachette Livre"},
                new Publisher{PublisherName="HarperCollins"},
                new Publisher{PublisherName="Macmillan Publishers"},
                new Publisher{PublisherName="Simon & Schuster"}
            };

            context.Publishers.AddRange(publishers);
            context.SaveChanges();

            var tags = new Tag[]
            {
                new Tag{TagName="Daily Quiet Time"},
                new Tag{TagName="Total Life"},
                new Tag{TagName="Teenagers at Church"},
                new Tag{TagName="Teenagers away from Church"},
                new Tag{TagName="Pastors Evangelizing Teenagers"},
                new Tag{TagName="Parents Evangelizing Teenagers"},
                new Tag{TagName="Volunteers Evangelizing Teenagers"},
                new Tag{TagName="Pastors Evangelize Parents"},
                new Tag{TagName="Evangelize Through Events"}
            };

            context.Tags.AddRange(tags);
            context.SaveChanges();

            var titles = new Title[]
            {
                new Title{TitleName="nisi vitae suscipit", 
                            AuthorID = authors.Single( s => s.AuthorLastName == "Arturo").AuthorID, 
                            PublisherID = publishers.Single( s => s.PublisherName == "Hachette Livre").PublisherID, 
                            PublishDate=DateTime.Parse("2019-09-01") },
                new Title{TitleName="tellus pellentesque eu", 
                            AuthorID = authors.Single( s => s.AuthorLastName == "Carson").AuthorID, 
                            PublisherID = publishers.Single( s => s.PublisherName == "HarperCollins").PublisherID, 
                            PublishDate=DateTime.Parse("2018-07-01")},
                new Title{TitleName="sed risus ultricies", 
                            AuthorID = authors.Single( s => s.AuthorLastName == "Gytis").AuthorID, 
                            PublisherID = publishers.Single( s => s.PublisherName == "Macmillan Publishers").PublisherID, 
                            PublishDate=DateTime.Parse("2016-05-01")},
                new Title{TitleName="ultrices in iaculis", 
                            AuthorID = authors.Single( s => s.AuthorLastName == "Laura").AuthorID, 
                            PublisherID = publishers.Single( s => s.PublisherName == "Penguin Random House").PublisherID, 
                            PublishDate=DateTime.Parse("2015-04-01")},
                new Title{TitleName="posuere morbi leo", 
                            AuthorID = authors.Single( s => s.AuthorLastName == "Meredith").AuthorID, 
                            PublisherID = publishers.Single( s => s.PublisherName == "Simon & Schuster").PublisherID, 
                            PublishDate=DateTime.Parse("2014-04-01")},
                new Title{TitleName="ut placerat orci", 
                            AuthorID = authors.Single( s => s.AuthorLastName == "Nino").AuthorID, 
                            PublisherID = publishers.Single( s => s.PublisherName == "Hachette Livre").PublisherID, 
                            PublishDate=DateTime.Parse("2014-03-01")},
                new Title{TitleName="gravida dictum fusce", 
                            AuthorID = authors.Single( s => s.AuthorLastName == "Peggy").AuthorID, 
                            PublisherID = publishers.Single( s => s.PublisherName == "HarperCollins").PublisherID, 
                            PublishDate=DateTime.Parse("2013-02-01")},
                new Title{TitleName="odio ut enim", 
                            AuthorID = authors.Single( s => s.AuthorLastName == "Yan").AuthorID, 
                            PublisherID = publishers.Single( s => s.PublisherName == "Macmillan Publishers").PublisherID, 
                            PublishDate=DateTime.Parse("2012-01-01")}
            };

            context.Titles.AddRange(titles);
            context.SaveChanges();

            var quotes = new Quote[]
            {
                new Quote{Quotation="viverra accumsan in nisl nisi scelerisque eu ultrices vitae auctor", 
                            TitleID = 1, Page=10, TagID = 1},
                new Quote{Quotation="dignissim convallis aenean et tortor at risus viverra adipiscing at", 
                            TitleID = 2, Page=50, TagID = 2},
                new Quote{Quotation="nunc sed velit dignissim sodales ut eu sem integer vitae", 
                            TitleID = 3, Page=5, TagID = 3},
                new Quote{Quotation="sit amet venenatis urna cursus eget nunc scelerisque viverra mauris", 
                            TitleID = 4, Page=105, TagID = 4},
                new Quote{Quotation="arcu ac tortor dignissim convallis aenean et tortor at risus", 
                            TitleID = 5, Page=15, TagID = 5}
            };

            context.Quotes.AddRange(quotes);
            context.SaveChanges();
        }
    }
}