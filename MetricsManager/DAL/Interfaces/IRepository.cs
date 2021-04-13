using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByDatePeriod(int AgetId, DateTimeOffset fromDate, DateTimeOffset toDate);
        void Create(int AgentId, T item);
        public DateTimeOffset GetDateTimeOfLastRecord(int AgentId);
    }

}
