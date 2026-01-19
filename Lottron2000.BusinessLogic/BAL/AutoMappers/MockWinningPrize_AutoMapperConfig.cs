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
    public static class MockWinningPrize_AutoMapperConfig
    {
        public static void CreateMap()
        {
            Mapper.CreateMap<MockWinningPrize, WinningPrize>()
            .ForMember(dest => dest.Div1, opt => opt.MapFrom(src => src.Div1))
            .ForMember(dest => dest.Div2, opt => opt.MapFrom(src => src.Div2))
            .ForMember(dest => dest.Div3, opt => opt.MapFrom(src => src.Div3))
            .ForMember(dest => dest.Div4, opt => opt.MapFrom(src => src.Div4))
            .ForMember(dest => dest.Div5, opt => opt.MapFrom(src => src.Div5))
            .ForMember(dest => dest.Div6, opt => opt.MapFrom(src => src.Div6))
            .ForMember(dest => dest.Div7, opt => opt.MapFrom(src => src.Div7))
            ;
        }
    }
}