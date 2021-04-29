using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RossQuotes.Models
{
    public class Title
    {
        public int TitleID { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string TitleName { get; set; }
        [Required]
        [Display(Name = "Author Name")]
        public int AuthorID { get; set; }
        [Required]
        [Display(Name = "Publisher Name")]
        public int PublisherID { get; set; }
        [Required]
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }

        public ICollection<Quote> Quotes { get; set; }
        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        
    }
}