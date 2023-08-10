public class PriorityQueue<TKey, TValue> where TKey : System.IComparable<TKey>
{
    // Note: elements in heap after first are NOT GUARANTEED to be in proper queue order
    private readonly MaxHeap<Node<TKey, TValue>> heap = new MaxHeap<Node<TKey, TValue>>();
    public int Count => heap.Count;

    public PriorityQueue()
    {

    }

    public PriorityQueue(PriorityQueue<TKey, TValue> original)
    {
        heap = new MaxHeap<Node<TKey, TValue>>(original.heap);
    }

    public void Enqueue(TKey priority, TValue value)
    {
        heap.Add(new Node<TKey, TValue>(priority, value));
    }

    public TValue Dequeue()
    {
        return heap.Remove().value;
    }

    public class Node<UKey, UValue> : System.IComparable<Node<UKey, UValue>> where UKey : System.IComparable<UKey>
    {
        public UKey priority;
        public UValue value;

        public Node(UKey priority, UValue value)
        {
            this.priority = priority;
            this.value = value;
        }

        public int CompareTo(Node<UKey, UValue> other)
        {
            return priority.CompareTo(other.priority);
        }

        public override string ToString()
        {
            return "(" + priority.ToString() + ", " + value.ToString() + ")";
        }
    }

    // FOR DEBUGGING ONLY
    public Node<TKey, TValue>[] ToArray()
    {
        return heap.ToArray();
    }
}
