using Xunit;
using Microsoft.EntityFrameworkCore;
using Library.Infrastructure.Data;
using Library.Infrastructure.Data.Repositories;
using Library.Domain.Entities;

namespace Library.Tests
{
    public class AuthorRepositoryTest
    {
        private readonly LibraryDbContext context;
        private readonly AuthorRepository repository;

        public AuthorRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: "LibraryDB")
                .Options;

            context = new LibraryDbContext(options);
            repository = new AuthorRepository(context);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllAuthors()
        {
            var total = await repository.GetAllAsync(1, 100);
            Assert.NotNull(total);

            var authors = new List<Author>
            {
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Alex",
                    Surname = "Pushkin",
                    Country = "Russia",
                    BirthDate = new DateTime(1800, 01, 01)
                },
                new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Fedya",
                    Surname = "Dostoevski",
                    Country = "Russia",
                    BirthDate = new DateTime(1800, 01, 01)
                }
            };

            await context.Authors.AddRangeAsync(authors);
            await context.SaveChangesAsync();

            var result = await repository.GetAllAsync(1, 100);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count() - total.Count());
        }

        [Fact]
        public async Task AddAsync_SavesAuthor()
        {
            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Oscar",
                Surname = "Uaild",
                Country = "USA",
                BirthDate = new DateTime(1800, 01, 01)
            };

            await repository.AddAsync(author);
            await context.SaveChangesAsync();

            var result = await context.Authors.FindAsync(author.Id);
            Assert.NotNull(result);
            Assert.Equal(author.Name, result.Name);
            Assert.Equal(author.Surname, result.Surname);
            Assert.Equal(author.Country, result.Country);
        }
    }
}