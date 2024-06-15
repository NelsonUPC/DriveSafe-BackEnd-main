using AutoMapper;
using DriveSafe.Domain.Publishing.Models.Commands;
using DriveSafe.Domain.Publishing.Models.Entities;

namespace DriveSafe.Presentation.Mapper;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
    }
}