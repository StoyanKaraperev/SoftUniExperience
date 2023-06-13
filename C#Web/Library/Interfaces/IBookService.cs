namespace Library.Interfaces
{
    using Library.Models;

    public interface IBookService
    {
        Task<IEnumerable<AllBooksViewModel>> GetAllBooksAsync();

        Task<IEnumerable<AllBooksViewModel>> GetMyBookAsync(string userId); 

        Task<BookViewModel?> GetBookByIdAsync(int id); 

        Task AddBookToCollectionAsync(string userId, BookViewModel book);

        Task RemoveBookFromCollectionAsync(string userId, BookViewModel book);

        Task <AddBookViewModel> GetNewAddBookModelAsync();

        Task AddBookAsync(AddBookViewModel model);

        Task<AddBookViewModel?> GetBookByIdForEditAsync(int id);
        Task EditBookAsync(AddBookViewModel model, int id);
    }
}
