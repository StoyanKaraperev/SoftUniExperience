namespace Forum.Data.Seeding;

using Forum.Data.Models;

class PostSeeder
{
    internal ICollection<Post> GeneratePost()
    {
        List<Post> posts = new List<Post>();
        Post currentPost;

        currentPost = new Post()
        {
            Title = "My first post",
            Content = "What is ASP.NET?"
        };
        posts.Add(currentPost);

        currentPost = new Post()
        {
            Title = "My second post",
            Content = "ASP.NET is an open source web framework, created by Microsoft, for building modern web apps and services with .NET."
        };
        posts.Add(currentPost);

        currentPost = new Post()
        {
            Title = "My third post",
            Content = "ASP.NET is cross platform and runs on Windows, Linux, macOS, and Docker."
        };
        posts.Add(currentPost); 

        return posts.ToList();
    }
}
