namespace Library.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Comment("User Books")]
    public class IdentityUserBook
    {
        [Comment("Book Collector")]
        [Required]
        [ForeignKey(nameof(IdentityUser))]
        public string CollectorId { get; set; } = null!;

        [Comment("Collector")]
        public virtual IdentityUser Collector { get; set; } = null!;

        [Comment("Book ID")]
        [Required]
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }

        [Comment("Book")]
        public virtual Book Book { get; set; } = null!; 
    }
}
