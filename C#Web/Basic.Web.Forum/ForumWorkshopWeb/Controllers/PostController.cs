namespace Forum.Web.Controllers;

using Forum.Services.Interfaces;
using Forum.ViewModels.Posts;
using Microsoft.AspNetCore.Mvc;

public class PostController : Controller
{
    private readonly IPostServices postService;

    public PostController(IPostServices postService)
    {
        this.postService = postService;
    }

    public async Task<IActionResult> All()
    {
        IEnumerable<PostListViewModel> allPosts = await this.postService.ListAllAsync(); 

        return View(allPosts); 
    }

    [HttpGet]
    public IActionResult Add()
    { 
        return View(); 
    }

    [HttpPost]
    public async Task<IActionResult> Add(PostAddForumModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await this.postService.AddPostAsync(model);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error");
            
            return View(model);
        }
        
        return RedirectToAction("All", "Post");
    }

    [HttpGet]
    public async Task<ActionResult> Edit(string id)
    {
        try
        {
            PostAddForumModel postModel =
            await this.postService.GetForEditOrDeleteByIdAsync(id);

            return View(postModel);
        }
        catch (Exception)
        {
            return this.RedirectToAction("All", "Post");
        }
    }

    [HttpPost] 
    public async Task<IActionResult> Edit(string id, PostAddForumModel postModel)
    {
        if (!ModelState.IsValid)
        {
            return this.View(postModel); 
        }

        try
        {
            await this.postService.EditByIdAsync(id, postModel);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error");

            return View(postModel); 
        }

        return RedirectToAction("All", "Post");
    }

    [HttpPost]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            await this.postService.DeleteByIdAsync(id);
        }
        catch (Exception)
        {
            
        }

        return RedirectToAction("All", "Post");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteWithView(string id)
    {
        try
        {
            PostAddForumModel psotModel = 
                await this.postService.GetForEditOrDeleteByIdAsync(id);

            return View(psotModel); 
        }
        catch (Exception)
        {
            
        }

        return this.RedirectToAction("All", "Post");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteWithView(string id, PostAddForumModel postModel)
    {
        try
        { 
                await this.postService.DeleteByIdAsync(id);
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Unexpected error");

            return this.View(postModel);
        }

        return this.RedirectToAction("All", "Post");
    }
}
