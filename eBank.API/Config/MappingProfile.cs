using AutoMapper;
using eBank.Infra.Data.Entities;
using eBank.Infra.Services.Models;

namespace eBank.API.Config
{
    public class MappingProfile : Profile
    {
        public MappingProfile() : base()
        {
            CreateMap<BankAccount, BankAccountModel>()
                .ForMember(x => x.Conta, m => m.MapFrom(y => y.Number))
                .ForMember(x => x.Saldo, m => m.MapFrom(y => y.Balance));

            CreateMap<BankAccountModel, BankAccount>()
                .ForMember(x => x.Number, m => m.MapFrom(y => y.Conta))
                .ForMember(x => x.Balance, m => m.MapFrom(y => y.Saldo));
        }
    }
}
