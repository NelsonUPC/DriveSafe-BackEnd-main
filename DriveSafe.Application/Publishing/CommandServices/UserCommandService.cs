using System.Data;
using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Commands;
using DriveSafe.Domain.Publishing.Models.Entities;
using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Domain.Publishing.Services;

namespace DriveSafe.Application.Publishing.CommandServices;

public class UserCommandService : IUserCommandService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserCommandService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateUserCommand command)
    {
        var user = _mapper.Map<CreateUserCommand, User>(command);
        
        var existingUser = await _userRepository.IsEmailInUseAsync(command.Gmail);
        
        //if (existingUser != null) throw new ConstraintException("Email already in use");
        
        return await _userRepository.SaveAsync(user);
    }

    public async Task<Boolean> Handle(int id, UpdateUserCommand command)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        var user = _mapper.Map<UpdateUserCommand, User>(command);
        if (existingUser == null) throw new ConstraintException("User not found");
        return await _userRepository.UpdateAsync(user, id);
    }

    public async Task<Boolean> Handle(DeleteUserCommand command)
    {
        var existingUser = _userRepository.GetByIdAsync(command.Id);
        if (existingUser == null) throw new ConstraintException("User not found");
        return await _userRepository.DeleteAsync(command.Id);
    }
}