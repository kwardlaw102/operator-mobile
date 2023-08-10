using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PartitionedList<T>
{
    private readonly Partition<T>[] arr;

    public PartitionedList(IEnumerable<T> original)
    {
        arr = new Partition<T>[original.Count()];
        int i = 0;
        foreach (var item in original)
        {
            arr[i++] = new Partition<T>(item);
        }
    }

    public void Combine(T newValue, int ndx1, int ndx2)
    {
        // combine regions containing partitions at ndx1 and ndx2
        Partition<T> partition1 = arr[ndx1];
        Partition<T> partition2 = arr[ndx2];
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == partition2)
            {
                arr[i] = partition1;
            }
        }

        // set value of combined region
        partition1.value = newValue;
    }

    public T this[int index]
    {
        get{ return arr[index].value; }
        set { arr[index].value = value; }
    }

    public T[] ToArray()
    {
        T[] newArr = new T[arr.Length];
        for (int i = 0; i < arr.Length; i++)
        {
            newArr[i] = arr[i].value;
        }
        return newArr;
    }

    private class Partition<U>
    {
        public U value;

        public Partition(U value)
        {
            this.value = value;
        }
    }
}
