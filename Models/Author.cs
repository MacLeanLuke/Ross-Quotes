using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RossQuotes.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string AuthorLastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        [Column("AuthorFirstName")]
        [Display(Name = "First Name")]
        public string AuthorFirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string AuthorMiddleName { get; set; }

        [Display(Name = "Prefix")]
        public string AuthorPrefix { get; set; }

        [Display(Name = "Suffix")]
        public string AuthorSuffix { get; set; }

        [Display(Name = "Author's Full Name")]
        public string FullName
        {
            get
            {
                return AuthorPrefix + " " + AuthorFirstName + " " + AuthorMiddleName + " " +  AuthorLastName  + " " + AuthorSuffix;
            }
        }
        public ICollection<Title> Titles { get; set; }
    }
}