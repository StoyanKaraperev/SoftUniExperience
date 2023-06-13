namespace Library.Models

{
    using Library.Data.Models;
    using System.ComponentModel.DataAnnotations;
    using static Library.Common.ValidationConstants.Book;

    public class BookViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(AuthorMinLength)]
        [MaxLength(AuthorMaxLength)]
        public string Author { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MinLength(ImageUrlMinLength)]
        public string Url { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), RatingMinValue, RatingMaxValue, ConvertValueInInvariantCulture = true)]
        public decimal Rating { get; set; }

        [Range(CategoryIdMinValue, CategoryIdMaxValue)]
        public int CategoryId { get; set; } 
    }
}
