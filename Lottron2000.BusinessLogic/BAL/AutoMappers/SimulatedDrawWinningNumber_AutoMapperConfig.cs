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
    public static class SimulatedDrawWinningNumber_AutoMapperConfig
    {
        public static void CreateDomainToDbMapping()
        {
            Mapper.CreateMap<LotteryNumbers, SimulatedDrawWinningNumber>()
    .ForMember(dest => dest.Bonus, opt => opt.MapFrom(src => src.Bonus))
    .ForMember(dest => dest.Num1, opt => opt.MapFrom(src => src.Number1))
    .ForMember(dest => dest.Num2, opt => opt.MapFrom(src => src.Number2))
    .ForMember(dest => dest.Num3, opt => opt.MapFrom(src => src.Number3))
    .ForMember(dest => dest.Num4, opt => opt.MapFrom(src => src.Number4))
    .ForMember(dest => dest.Num5, opt => opt.MapFrom(src => src.Number5))
    .ForMember(dest => dest.Num6, opt => opt.MapFrom(src => src.Number6))
    .ForMember(dest => dest.NumCheckSum, opt => opt.MapFrom(src => src.CheckSum))
    ;


        }
    }
}