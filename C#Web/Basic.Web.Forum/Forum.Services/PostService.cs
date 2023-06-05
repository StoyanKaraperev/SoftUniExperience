namespace Forum.Services;

using Forum.Data;
using Forum.Data.Models;
using Forum.Services.Interfaces;
using Forum.ViewModels.Posts;
using Microsoft.EntityFrameworkCore;

public class PostService : IPostServices
{
    private readonly ForumDbContext dbContext;

    public PostService(ForumDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<PostListViewModel>> ListAllAsync()
    {
        IEnumerable<PostListViewModel> allPosts = await this.dbContext
            .Posts
            .Select(ap => new PostListViewModel()
            {
                Id = ap.Id.ToString(), 
                Title = ap.Title, 
                Content = ap.Content
            })
            .ToArrayAsync();

        return allPosts;
    }

    public async Task AddPostAsync(PostAddForumModel postViewModel)
    {
        Post newPost = new Post()
        {
            Title = postViewModel.Title,
            Content = postViewModel.Content
        };

        await this.dbContext.Posts.AddAsync(newPost);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task<PostAddForumModel> GetForEditOrDeleteByIdAsync(string id)
    {
        Post? postToEdit = await this.dbContext
            .Posts
            .FirstOrDefaultAsync(p => p.Id.ToString() == id); 

        return new PostAddForumModel()
        {
            Title = postToEdit.Title,
            Content = postToEdit.Content
        };
    }

    public async Task EditByIdAsync(string id, PostAddForumModel postEditedModel)
    {
        Post postToEdit = await this.dbContext
            .Posts
            .FirstAsync(p => p.Id.ToString() == id); 

        postToEdit.Title = postEditedModel.Title;
        postToEdit.Content = postEditedModel.Content; 

        await this.dbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(string id)
    {
        Post postToDelete = await this.dbContext
            .Posts
            .FirstAsync(p => p.Id.ToString() == id);

        this.dbContext.Posts.Remove(postToDelete);
        await this.dbContext.SaveChangesAsync();
    }
}
