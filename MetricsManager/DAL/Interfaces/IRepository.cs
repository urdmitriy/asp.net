using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByDatePeriod(int agentId, DateTimeOffset fromDate, DateTimeOffset toDate);
        void Create(int agentId, T item);
        public DateTimeOffset GetDateTimeOfLastRecord(int agentId);
    }

}
