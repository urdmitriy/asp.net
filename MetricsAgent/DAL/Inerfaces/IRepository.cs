﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public interface IRepository<T> where T : class
    {
        //IList<T> GetAll();

        T GetById(int id);

        IList<T> GetByDatePeriod(TimeSpan fromDate, TimeSpan toDate);

        void Create(T item);

        void Update(T item);

        void Delete(int id);
    }

}
