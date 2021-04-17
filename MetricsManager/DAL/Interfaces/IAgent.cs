using System.Collections.Generic;
using MetricsManager.DAL.Models;
using MetricsManager.Requests;
using MetricsManager.Responses;

namespace MetricsManager.DAL.Interfaces
{
    public interface IAgent
    {
        void Create(Agents item);
        IList<Agents> GetAllAgents();
        string GetAddressForId(int id);
    }

}
 