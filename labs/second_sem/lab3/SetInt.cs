using System;

namespace lab3
{
    class SetInt : ISetInt
    {
        private int size;
        private int[] array;

        public SetInt()
        {
            this.size = 0;
            this.array = new int[16];
        }

        public int Count
        {
            get { return this.size; }
        }
        
        public bool Add(int value)
        {
            if (this.size == this.array.Length)
            {
                EnsureCapacity();
            }
            if (Contains(value))
            {
                return false;
            }
            int i = size - 1;
            while (i >= 0 && this.array[i] > value)
            {
                this.array[i + 1] = this.array[i];
                i--;
            }
            this.array[i + 1] = value;
            this.size++;
            return true;
        }

        public void Clear()
        {
            this.size = 0;
            this.array = new int[this.array.Length];
        }

        public bool Contains(int value)
        {
            int index = GetIndex(value);
            return index != -1;
        }

        public void CopyTo(int[] array)
        {
            if (array.Length != this.size)
            {
                throw new ArgumentException();
            }
            Array.Copy(this.array, array, this.size);
        }

        public bool Remove(int value)
        {
            int index = GetIndex(value);
            if (index == -1)
            {
                return false;
            }
            for (int i = index; i < this.size - 1; i++)
            {
                this.array[i] = this.array[i + 1];
            }
            this.array[this.size] = 0;
            this.size--;
            return true;
        }

        public bool SetEquals(ISetInt other)
        {
            int[] otherArray = new int[this.size];
            try
            {
                other.CopyTo(otherArray);
            }
            catch
            {
                return false;
            }
            for (int i = 0; i < this.size; i++)
            {
                if (this.array[i] != otherArray[i])
                {
                    return false;
                }
            }
            return true;
        }

        public void SymmetricExceptWith(ISetInt other)
        {
            int[] otherArray = new int[other.Count];
            other.CopyTo(otherArray);
            for (int i = 0; i < otherArray.Length; i++)
            {
                if (Contains(otherArray[i]))
                {
                    Remove(otherArray[i]);
                }
                else
                {
                    Add(otherArray[i]);
                }
            }
        }
        private void EnsureCapacity()
        {
            Array.Resize<int>(ref this.array, this.array.Length * 2);
        }
        private int GetIndex(int value)
        {
            int low = 0;
            int high = this.size - 1;
            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (this.array[mid] == value)
                {
                    return mid;
                }
                else if (value < this.array[mid])
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }
            return -1;
        }
    }
}
