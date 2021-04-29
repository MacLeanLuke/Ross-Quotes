using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RossQuotes.Models
{
    public class Publisher
    {
        public int PublisherID { get; set; }
        
        [Required]
        [Display(Name = "Publisher Name")]
        public string PublisherName { get; set; }

        public ICollection<Title> Titles { get; set; }
    }
}