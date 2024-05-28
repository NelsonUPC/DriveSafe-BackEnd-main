using _1.API.Request;
using _3.Data;
using _3.Data.Models;
using AutoMapper;

namespace _1.API.Mapper;

public class ModelToRequest : Profile
{
    public ModelToRequest()
    {
        CreateMap<User, UserRequest>();
        CreateMap<Vehicle, VehicleRequest>();
        CreateMap<Rent, RentRequest>();
        CreateMap<Maintenance, MaintenanceRequest>();
    }
}