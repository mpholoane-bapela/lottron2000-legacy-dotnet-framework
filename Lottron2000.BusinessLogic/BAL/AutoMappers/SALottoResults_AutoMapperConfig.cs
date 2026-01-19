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
    public static class SALottoResults_AutoMapperConfig
    {
        public static void CreateMap()
        {
            Mapper.CreateMap<SALottoResult, LotteryNumbers>()
            .ForMember(dest => dest.Number1, opt => opt.MapFrom(src => src.Ball1))
            .ForMember(dest => dest.Number2, opt => opt.MapFrom(src => src.Ball2))
            .ForMember(dest => dest.Number3, opt => opt.MapFrom(src => src.Ball3))
            .ForMember(dest => dest.Number4, opt => opt.MapFrom(src => src.Ball4))
            .ForMember(dest => dest.Number5, opt => opt.MapFrom(src => src.Ball5))
            .ForMember(dest => dest.Number6, opt => opt.MapFrom(src => src.Ball6))
            .ForMember(dest => dest.Bonus, opt => opt.MapFrom(src => src.BonusBall))
            .ForMember(dest => dest.CheckSumCount, opt => opt.MapFrom(src => src.SALottoResultCheckSums.FirstOrDefault().ResultCheckSumSa.Count))
            .ForMember(dest => dest.TicketUniqueID, opt => opt.ResolveUsing(r=>Guid.NewGuid().ToString()))
            ;
        }

    }
}