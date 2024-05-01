using AutoMapper;
using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Profiles
{
    public class FixedDepositMapperProfile : Profile
    {
        public FixedDepositMapperProfile()
        {
            CreateMap<FixedDeposit, FixedDepositDto>()
                .ForMember(
                    dest => dest.FixedDepositType,
                    opt => opt.MapFrom(src => src.FixedDepositType.TypeName)
                );

            CreateMap<FixedDepositRequest, FixedDeposit>();
        }
    }
}
