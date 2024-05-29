using _1.API.Request;
using _3.Data.Models;
using AutoMapper;

namespace _1.API.Mapper;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<UserRequest, User>();
        CreateMap<VehicleRequest, Vehicle>();
        CreateMap<RentRequest, Rent>();
        CreateMap<MaintenanceRequest, Maintenance>();
    }
}