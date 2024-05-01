using AutoMapper;
using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Profiles
{
    public class TransactionMapperProfile : Profile
    {
        public TransactionMapperProfile()
        {
            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.TransactionAmount, opt => opt.MapFrom(src => src.Amount));
        }
    }
}
