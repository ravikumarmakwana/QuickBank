using AutoMapper;
using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountCreationRequest, Account>();

            CreateMap<Account, AccountDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.Customer.FirstName} {src.Customer.LastName}"))
                .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => src.AccountType.Name));
        }
    }
}
