using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Commands;
using DriveSafe.Domain.Publishing.Models.Entities;

namespace DriveSafe.Presentation.Mapper;

public class ModelToRequest : Profile
{
    public ModelToRequest()
    {
        CreateMap<User, CreateUserCommand>();
        CreateMap<User, UpdateUserCommand>();
    }
}