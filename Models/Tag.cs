using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RossQuotes.Models
{
    public class Tag
    {
        public int TagID { get; set; }
        [Required]
        [Display(Name = "Tag")]
        public string TagName { get; set; }

        public ICollection<Quote> Quotes { get; set; }
    }
}