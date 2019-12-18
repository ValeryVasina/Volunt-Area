using System;
using System.Collections.Generic;
using System.Text;

namespace VoluntArea.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Remove(T item);
        IEnumerable<T> Items { get; }
    }
}
