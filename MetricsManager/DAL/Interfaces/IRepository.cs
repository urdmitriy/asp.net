using System;
using System.Collections.Generic;

namespace MetricsManager.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByDatePeriod(int AgetId, TimeSpan fromDate, TimeSpan toDate);

        void Create(int AgentId, T item);
    }

}
