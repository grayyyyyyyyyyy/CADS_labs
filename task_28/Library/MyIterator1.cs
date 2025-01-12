using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface MyIteratorSet<T>
    {
        T Cursor { get; }
        bool HasNext();
        T Next();
        void Remove();
    }
}