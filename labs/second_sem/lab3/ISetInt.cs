using System;

namespace lab3
{
    interface ISetInt
    {
        int Count { get; }

        bool Add(int value);
        bool Remove(int value);
        bool Contains(int value);
        void Clear();

        void CopyTo(int[] array);

        void SymmetricExceptWith(ISetInt other);
        bool SetEquals(ISetInt other);
    }
}
