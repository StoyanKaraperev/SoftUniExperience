namespace Forum.Services.Interfaces;

using Forum.ViewModels.Posts;

public interface IPostServices
{
    Task<IEnumerable<PostListViewModel>> ListAllAsync();

    Task AddPostAsync(PostAddForumModel postViewModel);

    Task<PostAddForumModel> GetForEditOrDeleteByIdAsync(string id);

    Task EditByIdAsync(string id, PostAddForumModel postEditedModel);

    Task DeleteByIdAsync(string id);
}
