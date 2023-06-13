namespace Library.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using static Library.Common.ValidationConstants.Category;

    [Comment("Category of the book")]
    public class Category
    {
        public Category()
        {
            this.Books = new List<Book>();
        }

        [Comment("Primary key")]
        [Key]
        public int Id { get; set; }

        [Comment("Name of the category")]
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Book> Books { get; set; } = null!; 
    }
}
