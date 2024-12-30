using AutoMapper;
using FluentValidation;
using Library.Application.DTO.AuthorDto;
using Library.Application.Interfaces;
using Library.Application.Use_Cases.AuthorUseCase;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;
using Moq;

namespace Library.Tests
{
    public class AddAuthorUseCaseTests
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IValidator<AddAuthorRequest>> validatorMock;

        private readonly Mock<IValidationService> validationServiceMock;
        private readonly AddAuthor addAuthorUseCase;

        public AddAuthorUseCaseTests()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            validatorMock = new Mock<IValidator<AddAuthorRequest>>();
            validationServiceMock = new Mock<IValidationService>();

            addAuthorUseCase = new AddAuthor(
                unitOfWorkMock.Object,
                mapperMock.Object,
                validatorMock.Object,
                validationServiceMock.Object);
        }

        [Fact]
        public async Task Execute_AddAuthor()
        {
            var dto = new AuthorBaseDTO
            {
                Name = "Alex",
                Surname = "Pushkin",
                Country = "Russia",
                BirthDate = new DateTime(1799, 6, 6)
            };

            var request = new AddAuthorRequest
            {
                Author = dto
            };

            var author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Alex",
                Surname = "Pushkin",
                Country = "USA",
                BirthDate = new DateTime(1799, 6, 6)
            };

            validationServiceMock
                .Setup(v => v.ValidateAsync(It.IsAny<IValidator<AddAuthorRequest>>(), request))
                .Returns(Task.CompletedTask);

            mapperMock.Setup(m => m.Map<Author>(dto)).Returns(author);

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.AddAsync(It.IsAny<Author>()))
                .Returns(Task.CompletedTask);

            unitOfWorkMock
                .Setup(u => u.SaveChangesAsync())
                .Returns(Task.FromResult(1));

            await addAuthorUseCase.ExecuteAsync(request);

            validationServiceMock.Verify(v => v.ValidateAsync(It.IsAny<IValidator<AddAuthorRequest>>(), request), Times.Once);
            mapperMock.Verify(m => m.Map<Author>(dto), Times.Once);
            unitOfWorkMock.Verify(u => u.AuthorRepository.AddAsync(It.IsAny<Author>()), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}