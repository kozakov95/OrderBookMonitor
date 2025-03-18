using AutoMapper;
using OrderBookMonitor.Application.OrderBook.Models;
using OrderBookMonitor.Common.Modules.OrderBook.DTO;
using OrderBookMonitor.Infrastructure.Modules.OrderBook.Bitstamp.Models;
using OrderBookMonitor.Modules.OrderBook.CommonModels;

namespace OrderBookMonitor.Infrastructure.Modules.OrderBook.Mapping;

public class OrderBookProfile : Profile
{
    public OrderBookProfile()
    {
        CreateMap<OrderBookEntryPollingModel, OrderBookEntryItemModel>(MemberList.Destination)
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            ;
        
        CreateMap<OrderBookDataPollingModel, OrderBookModel>(MemberList.Destination)
            .ForMember(dest => dest.Bids, opt => opt.MapFrom(src => src.Bids))
            .ForMember(dest => dest.Asks, opt => opt.MapFrom(src => src.Asks))
            ;

        CreateMap<DepthOfMarketItemModel, DepthOfMarketEntryDto>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            ;
        
        CreateMap<DepthOfMarketModel, DepthOfMarketDto>(MemberList.Destination)
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Asks, opt => opt.MapFrom(src => src.Asks))
            .ForMember(dest => dest.Bids, opt => opt.MapFrom(src => src.Bids))
            ;
    }
}