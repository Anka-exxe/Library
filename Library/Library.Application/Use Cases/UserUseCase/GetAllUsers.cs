using AutoMapper;
using Library.Application.DTO.UserDto.Response;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Interfaces.Repositories;

namespace Library.Application.Use_Cases.UserUseCase
{
    public class GetAllUsers
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllUsers(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserResponse>> ExecuteAsync()
        {
            IEnumerable<User>? users = await unitOfWork.UserRepository.GetAllAsync();

            if (!users.Any())
            {
                throw new NotFoundException("Users not found");
            }
            else
            {
                IEnumerable<UserResponse> response = mapper.Map<IEnumerable<UserResponse>>(users);
                return response;
            }
        }
    }
}
