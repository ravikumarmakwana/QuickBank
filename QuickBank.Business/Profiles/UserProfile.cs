using AutoMapper;
using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegistrationRequest, ApplicationUser>();
        }
    }
}
