﻿using System;
using System.Collections.Generic;

namespace MetricsAgent.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IList<T> GetByDatePeriod(TimeSpan fromDate, TimeSpan toDate);

        void Create(T item);
    }

}