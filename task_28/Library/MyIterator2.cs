using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface MyIteratorList<T>
    {
        T Cursor { get; }
        bool HasNext();
        T Next();
        bool HasPrevious();
        T Previous();
        int NextIndex();
        int PreviousIndex();
        void Remove();
        void Set(T element);
        void Add(T element);
    }
}