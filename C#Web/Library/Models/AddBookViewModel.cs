namespace Library.Models

{
    using System.ComponentModel.DataAnnotations;
    using static Library.Common.ValidationConstants.Book;

    public class AddBookViewModel
    {
        public AddBookViewModel()
        {
            this.Categories = new List<CategoryViewModel>();
        }

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

        [Required(AllowEmptyStrings = false)]
        public string Url { get; set; } = null!;

        [Required]
        public string Rating { get; set; } = null!;

        [Range(CategoryIdMinValue, CategoryIdMaxValue)]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = null!;
    }
}
