using AutoMapper;
using FluentValidation;
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
    public class UpdateAuthorUseCaseTest
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IValidator<UpdateAuthorRequest>> validatorMock;
        private readonly Mock<IValidationService> validationServiceMock;
        private readonly UpdateAuthor useCase;

        public UpdateAuthorUseCaseTest()
        {
            unitOfWorkMock = new Mock<IUnitOfWork>();
            mapperMock = new Mock<IMapper>();
            validatorMock = new Mock<IValidator<UpdateAuthorRequest>>();
            validationServiceMock = new Mock<IValidationService>();

            useCase = new UpdateAuthor(
                unitOfWorkMock.Object,
                mapperMock.Object,
                validatorMock.Object,
                validationServiceMock.Object);
        }

        [Fact]
        public async Task Execute_ValidRequest_UpdatesAuthorSuccessfully()
        {
            var authorId = Guid.NewGuid();

            var dto = new AuthorBaseDTO
            {
                Name = "Alex",
                Surname = "Pushkin",
                Country = "Russia",
                BirthDate = new DateTime(1799, 6, 6)
            };

            var request = new UpdateAuthorRequest
            {
                Author = dto,
                AuthorId = authorId,
            };

            var author = new Author
            {
                Id = authorId,
                Name = "Alex",
                Surname = "Pushkin",
                Country = "USA",
                BirthDate = new DateTime(1800, 7, 7)
            };

            validationServiceMock
               .Setup(v => v.ValidateAsync(validatorMock.Object, request))
               .Returns(Task.CompletedTask);

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.GetByIdAsync(authorId))
                .ReturnsAsync(author);

            mapperMock.Setup(m => m.Map<Author>(request)).Returns(author);

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.UpdateAsync(It.IsAny<Author>()))
                .Returns(Task.CompletedTask);

            unitOfWorkMock
                .Setup(u => u.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            await useCase.ExecuteAsync(request);

            validationServiceMock.Verify(v => v.ValidateAsync(validatorMock.Object, request), Times.Once);
            unitOfWorkMock.Verify(u => u.AuthorRepository.GetByIdAsync(authorId), Times.Once);
            unitOfWorkMock.Verify(u => u.AuthorRepository.UpdateAsync(It.IsAny<Author>()), Times.Once);
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Execute_AuthorDoesNotExist_ThrowsNotFoundException()
        {
            var authorId = Guid.NewGuid();
            var authorDto = new AuthorBaseDTO
            {
                Name = "Mikle",
                Surname = "Bulgakov",
                Country = "Germany",
                BirthDate = DateTime.Now.AddYears(-25)
            };

            var request = new UpdateAuthorRequest
            {
                Author = authorDto,
                AuthorId = authorId,
            };

            unitOfWorkMock
                .Setup(u => u.AuthorRepository.GetByIdAsync(authorId))
                .ReturnsAsync((Author)null);

            var exception = await Assert.ThrowsAsync<NotFoundException>(() => useCase.ExecuteAsync(request));
            Assert.Equal($"Author not found", exception.Message);

            unitOfWorkMock.Verify(u => u.AuthorRepository.GetByIdAsync(authorId), Times.Once);
            unitOfWorkMock.Verify(u => u.AuthorRepository.UpdateAsync(It.IsAny<Author>()), Times.Never);
            unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}