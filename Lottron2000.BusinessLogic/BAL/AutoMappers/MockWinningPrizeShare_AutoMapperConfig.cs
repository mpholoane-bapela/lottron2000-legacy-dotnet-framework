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
    public static class MockWinningPrizeShare_AutoMapperConfig
    {

        public static void CreateMap()
        {
            Mapper.CreateMap<MockWinningPrizeShare, WinningPrizeWinner>()
            .ForMember(dest => dest.Div1Winners, opt => opt.MapFrom(src => src.Div1Winners))
            .ForMember(dest => dest.Div2Winners, opt => opt.MapFrom(src => src.Div2Winners))
            .ForMember(dest => dest.Div3Winners, opt => opt.MapFrom(src => src.Div3Winners))
            .ForMember(dest => dest.Div4Winners, opt => opt.MapFrom(src => src.Div4Winners))
            .ForMember(dest => dest.Div5Winners, opt => opt.MapFrom(src => src.Div5Winners))
            .ForMember(dest => dest.Div6Winners, opt => opt.MapFrom(src => src.Div6Winners))
            .ForMember(dest => dest.Div7Winners, opt => opt.MapFrom(src => src.Div7Winners))
            ;
        }
    
    }
}