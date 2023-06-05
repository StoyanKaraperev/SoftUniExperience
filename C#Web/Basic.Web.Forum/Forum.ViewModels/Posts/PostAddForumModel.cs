namespace Forum.ViewModels.Posts;

using System.ComponentModel.DataAnnotations;
using static Forum.Common.EntityValidations.Post;

public class PostAddForumModel
{
    [Required]
    [MinLength(TitleMinLength)]
    [MaxLength(TitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MinLength(ContentMinLength)]
    [MaxLength(ContentMaxLength)]
    public string Content { get; set; } = null!;  
}
