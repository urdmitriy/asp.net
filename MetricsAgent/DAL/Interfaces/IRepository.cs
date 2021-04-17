using System;
using System.Collections.Generic;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByDatePeriod(DateTimeOffset fromDate, DateTimeOffset toDate);

        void Create(T item);
    }

}
