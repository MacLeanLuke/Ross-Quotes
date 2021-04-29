using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RossQuotes.Models
{
    public class Quote
    {
        public int QuoteID { get; set; }
        [Required]
        [Display(Name = "Quotation")]
        public string Quotation { get; set; }

        [Required]
        [Display(Name = "Title of Quote Source")]
        public int TitleID { get; set; }

        [Required]
        [Display(Name = "Page of Quote Source")]
        public int Page { get; set; }

        [Required]
        [Display(Name = "Quote Tag")]
        public int TagID { get; set; }

        public Title Title { get; set; }
        public Tag Tag { get; set; }
    }
}