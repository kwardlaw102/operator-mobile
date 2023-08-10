using System.Collections.Generic;

class MaxHeap<T> where T : System.IComparable<T>
{
    private readonly List<T> list = new List<T>();
    public int Count => list.Count;

    public MaxHeap()
    {

    }

    public MaxHeap(MaxHeap<T> original)
    {
        list = new List<T>(original.list);
    }

    public void Add(T item)
    {
        list.Add(item);
        int ndx = list.Count - 1;
        while (ndx != 0 && list[GetParentNdx(ndx)].CompareTo(list[ndx]) < 0)
        {
            Swap(ndx, GetParentNdx(ndx));
            ndx = GetParentNdx(ndx);
        }
    }

    public T Remove()
    {
        T item;
        if (list.Count == 0)
        {
            throw new System.Exception("Cannot remove from an empty heap.");
        }

        item = list[0];
        list[0] = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        MaxHeapify(0);
        return item;
    }

    public T Peek()
    {
        return list[0];
    }

    public void Clear()
    {
        list.Clear();
    }

    public T[] ToArray()
    {
        return list.ToArray();
    }

    private void MaxHeapify(int ndx)
    {
        int max = ndx;
        int l = GetLeftNdx(ndx);
        int r = GetRightNdx(ndx);
        if (list.Count > l && list[l].CompareTo(list[max]) > 0)
        {
            max = l;
        }
        if (list.Count > r && list[r].CompareTo(list[max]) > 0)
        {
            max = r;
        }
        if (max != ndx)
        {
            Swap(ndx, max);
            MaxHeapify(max);
        }
    }

    private int GetLeftNdx(int ndx)
    {
        return ndx * 2 + 1;
    }

    private int GetRightNdx(int ndx)
    {
        return ndx * 2 + 2;
    }

    private int GetParentNdx(int ndx)
    {
        return (ndx - 1) / 2;
    }

    private void Swap(int ndx1, int ndx2)
    {
        T temp = list[ndx1];
        list[ndx1] = list[ndx2];
        list[ndx2] = temp;
    }
}

