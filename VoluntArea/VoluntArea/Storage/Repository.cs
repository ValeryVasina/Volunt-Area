using System;
using System.Collections.Generic;
using System.Text;
using VoluntArea.Interfaces;

namespace VoluntArea
{
    class Repository<T> : IRepository<T> 
    {
        protected List<T> items;
        public IEnumerable<T> Items => items;
        public void Add(T item)
        {
            items.Add(item);
        }

        public void Remove(T item)
        {
            items.Remove(item);
        }
    }
}
