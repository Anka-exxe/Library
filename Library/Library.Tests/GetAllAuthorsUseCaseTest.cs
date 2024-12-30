using AutoMapper;
using Library.Application.DTO.AuthorDto;
using Library.Application.Interfaces;
using Library.Application.Use_Cases.AuthorUseCase;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Moq;
using Xunit;

namespace Library.Tests
{
    public class GetAllAuthorsUseCaseTest
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly GetAllAuthors useCase;

        public GetAllAuthorsUseCaseTest()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            useCase = new GetAllAuthors(unitOfWorkMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task Execute_GetAllAuthors()
        {
            var authors = new List<Author>
            {
                new Author {
                    Id = Guid.NewGuid(),
                    Name = "Alex",
                    Surname = "Pushkin",
                    Country = "Russia",
                    BirthDate = new DateTime(1799, 6, 6)
                },
                 new Author {
                    Id = Guid.NewGuid(),
                    Name = "Fedya",
                    Surname = "Dostoevski",
                    Country = "Russia",
                    BirthDate = new DateTime(1821, 11, 11)
                },
            };

            var authorResponses = new List<AuthorResponse>
            {
                new AuthorResponse 
                { 
                    Id = authors[0].Id,  
                    Name = "Alex",
                    Surname = "Pushkin",
                    Country = "Russia",
                    BirthDate = new DateTime(1799, 6, 6)
                },
                new AuthorResponse
                {
                    Id = authors[1].Id,
                    Name = "Fedya",
                    Surname = "Dostoevski",
                    Country = "Russia",
                    BirthDate = new DateTime(1821, 11, 11)
                }
            };

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.GetAllAsync(1, 5))
                .ReturnsAsync(authors);

            mapperMock
                .Setup(m => m.Map<IEnumerable<AuthorResponse>>(authors))
                .Returns(authorResponses); 

            var result = await useCase.ExecuteAsync(1, 5);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal(authorResponses.First().Name, result.First().Name);
            Assert.Equal(authorResponses.Last().Surname, result.Last().Surname);

            unitOfWorkMock.Verify(u => u.AuthorRepository.GetAllAsync(1, 5), Times.Once);
            mapperMock.Verify(m => m.Map<IEnumerable<AuthorResponse>>(authors), Times.Once);
        }
    }
}