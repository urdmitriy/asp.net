using AutoMapper;
using MetricsManager.DAL.DTO;
using MetricsManager.DAL.Models;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<NetworkMetrics, NetworkMetricsDto>();
            CreateMap<RamMetrics, RamMetricsDto>();
        }
    }
}
