using MetricsManager.Requests;
using MetricsManager.Responses;

namespace MetricsManager.DAL.Interfaces
{
    public interface IMetricsAgentClient
    {
        AllRamMetricsResponse GetRamMetrics(GetAllRamMetricsApiRequest request);

        AllHddMetricsResponse GetHddMetrics(GetAllHddMetricsApiRequest request);

        AllDotNetMetricsResponse GetDotNetMetrics(GetAllDotNetMetricsApiRequest request);

        AllCpuMetricsResponse GetCpuMetrics(GetAllCpuMetricsApiRequest request);

        AllNetworkMetricsResponse GetNetworkMetrics(GetAllNetworkMetricsApiRequest request);

    }

}
 