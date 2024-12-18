using AutoMapper;

namespace BatchProcessing.Configurations
{
    public class IpDetailsProfile : Profile
    {
        public IpDetailsProfile()
        {
            CreateMap<IpDetailsCache.Models.IpDetails, BatchProcessing.Models.IpDetails>().ReverseMap();
            CreateMap<IpDetailsCache.Models.IpLocation, BatchProcessing.Models.IpLocation>().ReverseMap();
            CreateMap<IpDetailsCache.Models.IpLanguage, BatchProcessing.Models.IpLanguage>().ReverseMap();
            CreateMap<IpDetailsCache.Models.IpTimeZone, BatchProcessing.Models.IpTimeZone>().ReverseMap();
            CreateMap<IpDetailsCache.Models.IpCurrency, BatchProcessing.Models.IpCurrency>().ReverseMap();
            CreateMap<IpDetailsCache.Models.IpConnection, BatchProcessing.Models.IpConnection>().ReverseMap();

            CreateMap<BatchProcessing.Models.BatchRecord, Models.BatchStatusResponse>()
            .ForMember(dest => dest.BatchId, opt => opt.MapFrom(src => src.BatchId.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.IpDetails, opt => opt.MapFrom(src => src.ProcessedIpDetails));
        }
    }
}
