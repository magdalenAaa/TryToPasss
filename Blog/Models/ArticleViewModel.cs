using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Article> Articles { get; set; }

        public string Tags { get; set; }

        [DisplayName("Your Santa")]
        [Required(ErrorMessage = "Please enter how many Stream Entries are displayed per page.")]
        [StringLength(50)]
        public string YourSanta { get; set; }

    }
}