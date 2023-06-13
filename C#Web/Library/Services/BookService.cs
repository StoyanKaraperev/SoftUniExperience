namespace Library.Services
{
    using Library.Data;
    using Library.Data.Models;
    using Library.Interfaces;
    using Library.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class BookService : IBookService
    {
        private readonly LibraryDbContext dbContext;

        public BookService(LibraryDbContext libraryDbContext)
        {
            this.dbContext = libraryDbContext;
        }

        public async Task<IEnumerable<AllBooksViewModel>> GetAllBooksAsync()
        {
            return await this.dbContext
                .Books
                .Select(b => new AllBooksViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Category = b.Category.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<AllBooksViewModel>> GetMyBookAsync(string userId)
        {
            return await this.dbContext.UsersBooks
                .Where(ub => ub.CollectorId == userId)
                .Select(ub => new AllBooksViewModel
                {
                    Id = ub.Book.Id,
                    Title = ub.Book.Title,
                    Author = ub.Book.Author, 
                    ImageUrl = ub.Book.ImageUrl,
                    Description = ub.Book.Description,
                    Category = ub.Book.Category.Name
                })
                .ToListAsync();
        }

        public async Task<BookViewModel?> GetBookByIdAsync(int id)
        {
            return await this.dbContext
                .Books
                .Where(b => b.Id == id)
                .Select(b => new BookViewModel
                { 
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Url = b.ImageUrl,
                    Description = b.Description,
                    CategoryId = b.CategoryId
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddBookToCollectionAsync(string userId, BookViewModel book)
        {
            bool alredyAdded = await dbContext.UsersBooks
                .AnyAsync(ub => ub.CollectorId == userId && ub.BookId == book.Id);

            if (alredyAdded == false)
            {
                var userBook = new IdentityUserBook
                {
                    CollectorId = userId,
                    BookId = book.Id,
                };

                await dbContext.UsersBooks.AddAsync(userBook);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task RemoveBookFromCollectionAsync(string userId, BookViewModel book)
        {
            var userBook = await dbContext.UsersBooks
                .FirstOrDefaultAsync(ub => ub.CollectorId == userId && ub.BookId == book.Id);

            if (userBook != null)
            {
                dbContext.UsersBooks.Remove(userBook);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<AddBookViewModel> GetNewAddBookModelAsync()
        {
            var categories = await dbContext
                .Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            var model = new AddBookViewModel
            { 
                Categories = categories 
            };

            return model;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            Book book = new Book
            {
                Title = model.Title,
                Author = model.Author,
                ImageUrl = model.Url,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Rating = decimal.Parse(model.Rating)
            };

            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
        }

        public async Task<AddBookViewModel?> GetBookByIdForEditAsync(int id)
        {
            var categories = await dbContext
                .Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id, 
                    Name = c.Name,
                })
                .ToListAsync();

            return await dbContext
                .Books
                .Where(b => b.Id == id)
                .Select(b => new AddBookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    Url = b.ImageUrl,
                    Description = b.Description,
                    Rating = b.Rating.ToString(),
                    Categories = categories
                })
                .FirstOrDefaultAsync();
        }

        public async Task EditBookAsync(AddBookViewModel model, int id)
        {
            var book = await dbContext.Books.FindAsync(id);

            if (book != null)
            {
                book.Title = model.Title;
                book.Author = model.Author;
                book.ImageUrl = model.Url;
                book.Description = model.Description;
                book.CategoryId = model.CategoryId;
                book.Rating = decimal.Parse(model.Rating); 

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
