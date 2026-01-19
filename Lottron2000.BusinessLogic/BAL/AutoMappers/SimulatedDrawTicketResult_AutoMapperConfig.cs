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
    public static class SimulatedDrawTicketResult_AutoMapperConfig
    {
        public static void CreateMap()
        {
            Mapper.CreateMap<SimulatedDrawTicketResult, DrawTicketMatch>()
            .ForMember(dest => dest.AmountWon, opt => opt.MapFrom(src => src.AmountWon))
            .ForMember(dest => dest.BonusBallMatch, opt => opt.MapFrom(src => src.BonusMatch))
            .ForMember(dest => dest.DrawSubCategory, opt => opt.MapFrom(src => src.DrawSubCategory))
            .ForMember(dest => dest.IsJackpotWinner, opt => opt.MapFrom(src => src.IsJackpotWinner))
            .ForMember(dest => dest.NumbersMatched, opt => opt.MapFrom(src => src.NumberOfMatches))
            .ForMember(dest => dest.WinningDivision, opt => opt.MapFrom(src => src.WinningDivision))
            .ForMember(dest => dest.TicketUniqueID, opt => opt.MapFrom(src => src.SimulatedDrawTicketID))
            ;
        }

        public static void CreateDomainToDbMapping()
        {
            Mapper.CreateMap<DrawTicketMatch, SimulatedDrawTicketResult>()
    .ForMember(dest => dest.AmountWon, opt => opt.MapFrom(src => src.AmountWon))
    .ForMember(dest => dest.BonusMatch, opt => opt.MapFrom(src => src.BonusBallMatch))
    .ForMember(dest => dest.IsJackpotWinner, opt => opt.MapFrom(src => src.IsJackpotWinner))
    .ForMember(dest => dest.NumberOfMatches, opt => opt.MapFrom(src => src.NumbersMatched))
    .ForMember(dest => dest.WinningDivision, opt => opt.MapFrom(src => src.WinningDivision))
    .ForMember(dest => dest.DrawSubCategory, opt => opt.MapFrom(src => src.DrawSubCategory))
    .ForMember(dest => dest.SimulatedDrawTicketID, opt => opt.MapFrom(src => src.TicketUniqueID))
    ;
        }

    }
}