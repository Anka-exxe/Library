using Library.Application.DTO.AuthorDto;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Use_Cases.AuthorUseCase;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Library.Tests
{
    public class DeleteAuthorUseCaseTest
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly DeleteAuthor useCase;

        public DeleteAuthorUseCaseTest()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            useCase = new DeleteAuthor(unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Execute_AuthorExists_DeleteAuthor()
        {
            var authorId = Guid.NewGuid();
            var author = new Author { Id = authorId };
            var request = new DeleteAuthorRequest { AuthorId = authorId };

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.GetByIdAsync(authorId))
                .ReturnsAsync(author);

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.DeleteAsync(author))
                .Returns(Task.CompletedTask);

            unitOfWorkMock
                .Setup(u => u.SaveChangesAsync())
                .Returns(Task.FromResult(1));

            await useCase.ExecuteAsync(request);

            unitOfWorkMock.Verify(u => u.AuthorRepository.GetByIdAsync(authorId), Times.Once);
            unitOfWorkMock.Verify(u => u.AuthorRepository.DeleteAsync(author), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Execute_AuthorDoesNotExist_ThrowsNotFoundException()
        {
            var authorId = Guid.NewGuid();
            var request = new DeleteAuthorRequest { AuthorId = authorId };

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.GetByIdAsync(authorId))
                .ReturnsAsync((Author)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => useCase.ExecuteAsync(request));
            Assert.Equal($"Author not found", exception.Message);

            unitOfWorkMock.Verify(u => u.AuthorRepository.GetByIdAsync(authorId), Times.Once);
            unitOfWorkMock.Verify(u => u.AuthorRepository.DeleteAsync(It.IsAny<Author>()), Times.Never);
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}