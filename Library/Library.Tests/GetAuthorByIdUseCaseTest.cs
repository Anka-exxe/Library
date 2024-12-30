using AutoMapper;
using Library.Application.DTO.AuthorDto;
using Library.Application.Exceptions;
using Library.Application.Use_Cases.AuthorUseCase;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Library.Tests
{
    public class GetAuthorByIdUseCaseTest
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly GetAuthorById useCase;

        public GetAuthorByIdUseCaseTest()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            useCase = new GetAuthorById(unitOfWorkMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task Execute_AuthorExists_ReturnsAuthorResponse()
        {
            var authorId = Guid.NewGuid();

            var author = new Author
            {
                Id = authorId,
                Name = "Alex",
                Surname = "Pushkin",
                Country = "Russia",
                BirthDate = new DateTime(1799, 6, 6)
            };

            var request = new GetAuthorByIdRequest { AuthorId = authorId };

            var authorResponse = new AuthorResponse
            {
                Id = author.Id,
                Name = author.Name,
                Surname = author.Surname,
                Country = author.Country,
                BirthDate = author.BirthDate
            };

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.GetByIdAsync(authorId))
                .ReturnsAsync(author);

            mapperMock
                .Setup(m => m.Map<AuthorResponse>(author))
                .Returns(authorResponse);

            var result = await useCase.ExecuteAsync(request);

            Assert.NotNull(result);
            Assert.Equal(authorResponse.Name, result.Name);
            Assert.Equal(authorResponse.Name, result.Name);

            unitOfWorkMock.Verify(u => u.AuthorRepository.GetByIdAsync(authorId), Times.Once);
            mapperMock.Verify(m => m.Map<AuthorResponse>(author), Times.Once);
        }

        [Fact]
        public async Task Execute_AuthorDoesNotExist_ThrowsNotFoundException()
        {
            var authorId = Guid.NewGuid();
            var request = new GetAuthorByIdRequest { AuthorId = authorId };

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.GetByIdAsync(authorId))
                .ReturnsAsync((Author)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => useCase.ExecuteAsync(request));
            Assert.Equal($"Author not found", exception.Message);

            unitOfWorkMock.Verify(u => u.AuthorRepository.GetByIdAsync(authorId), Times.Once);
            mapperMock.Verify(m => m.Map<AuthorResponse>(It.IsAny<Author>()), Times.Never);
        }
    }
}