using DriveSafe.Domain.Publishing.Models.Commands;

namespace DriveSafe.Domain.Publishing.Services;

public interface IUserCommandService
{
    Task<int> Handle(CreateUserCommand command);
    Task<Boolean> Handle(int id, UpdateUserCommand command);
    Task<Boolean> Handle(DeleteUserCommand command);
}