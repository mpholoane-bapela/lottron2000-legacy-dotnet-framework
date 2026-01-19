using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lottron2000.Data;
using Lottron2000.Models;

namespace Lottron2000.BusinessLogic.BAL.AutoMappers
{
    public static class SimulatedDrawResult_AutoMapperConfig
    {
        public static void CreateDomainToDbMapping()
        {
            Mapper.CreateMap<DrawTicketSetMatch, SimulatedDrawResult>()
            .ForMember(dest => dest.AmountLost, opt => opt.MapFrom(src => src.AmountLost))
            .ForMember(dest => dest.AmountWon, opt => opt.MapFrom(src => src.AmountWon))
            .ForMember(dest => dest.AmountSpent, opt => opt.MapFrom(src => src.AmountSpent))
            .ForMember(dest => dest.BonusMatches, opt => opt.MapFrom(src => src.BonusMatches))
            .ForMember(dest => dest.Num1Matches, opt => opt.MapFrom(src => src.Num1Matches))
            .ForMember(dest => dest.Num2Matches, opt => opt.MapFrom(src => src.Num2Matches))
            .ForMember(dest => dest.Num3Matches, opt => opt.MapFrom(src => src.Num3Matches))
            .ForMember(dest => dest.Num4Matches, opt => opt.MapFrom(src => src.Num4Matches))
            .ForMember(dest => dest.Num5Matches, opt => opt.MapFrom(src => src.Num5Matches))
            .ForMember(dest => dest.Num6Matches, opt => opt.MapFrom(src => src.Num6Matches))
    ;
        }

    }
}
