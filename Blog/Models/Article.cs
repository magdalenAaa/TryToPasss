using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Article
    {
        private ICollection<Tag> tags;

        public Article()
        {
            this.tags = new HashSet<Tag>();
        }
        public Article(string authorId, string title, string Content, int categoryId)
        {
            this.AuthorId = authorId;
            this.Title = title;
            this.Content = Content;
            this.CategoryId = categoryId;
            this.tags = new HashSet<Tag>();
        }

        public Article(string authorId, string title, string Content, int categoryId, string yourSanta) : this(authorId, title, Content, categoryId)
        {
            this.YourSanta = yourSanta;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public bool IsAuthor(string name)
        {
            return this.Author.UserName.Equals(name);
        }


        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        [Required]
        [StringLength(50)]
        public string YourSanta { get; set; }



    }
}
