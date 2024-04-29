using AutoMapper;
using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<AddAddressRequest, Address>();
            CreateMap<UpdateAddressRequest, Address>();
            CreateMap<Address, AddressModel>();
        }
    }
}
