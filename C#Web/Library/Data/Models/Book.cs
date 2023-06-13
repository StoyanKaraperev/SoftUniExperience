namespace Library.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Library.Common.ValidationConstants.Book;

    [Comment("Library of books")]
    public class Book
    {
        public Book()
        {
            this.UsersBooks = new List<IdentityUserBook>();
        }

        [Comment("Primary key")]
        [Required]
        [Key]
        public int Id { get; set; }

        [Comment("Title of the book")]
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Comment("Author of the book")]
        [Required]
        [MaxLength(AuthorMaxLength)]
        public string Author { get; set; } = null!;

        [Comment("Description of the book")]
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Comment("Image URL of the book")]
        [Required]
        public string ImageUrl { get; set; } = null!;

        [Comment("Rating of the book")]
        [Required]
        public decimal Rating { get; set; }

        [Comment("Category ID of the book")]
        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Comment("Category of the book")]
        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<IdentityUserBook> UsersBooks { get; set; } = null!; 
    }
}
