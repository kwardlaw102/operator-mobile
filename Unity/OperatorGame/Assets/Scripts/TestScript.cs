using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public static Dictionary<Operator, int> OperatorPriorityDict = new Dictionary<Operator, int>();

    void Start()
    {
        //TestOrderOfOperations();
        //TestHeap();
        //TestPriorityQueue();
        //TestPartitionedList();
        //TestExpressionEvaluator();
        TestExpressionEvaluator2();
    }

    private void PrintList<T>(T[] list)
    {
        if (list.Length == 0)
        {
            Debug.Log("{ }");
            return;
        }
        string output = "{ " + list[0];
        for (int i = 1; i < list.Length; i++)
        {
            output += ", " + list[i];
        }
        output += " }";
        Debug.Log(output);
    }

    private void TestOrderOfOperations()
    {
        ExpressionEvaluator evaluator = new ExpressionEvaluator();
        PrintList(evaluator.OrderOfOperations(new Operator[] { Operator.Add, Operator.Subtract, Operator.Multiply, Operator.Divide }));
    }

    private void TestHeap()
    {
        MaxHeap<int> heap = new MaxHeap<int>();
        heap.Add(10);
        PrintList(heap.ToArray());
        heap.Add(5);
        PrintList(heap.ToArray());
        heap.Add(3);
        PrintList(heap.ToArray());
        heap.Add(20);
        PrintList(heap.ToArray());
        MaxHeap<int> heap2 = new MaxHeap<int>(heap);
        Debug.Log(heap.Peek());
        Debug.Log(heap.Remove());
        PrintList(heap.ToArray());
        Debug.Log(heap.Peek());
        PrintList(heap2.ToArray());
    }
    
    private void TestPriorityQueue()
    {
        PriorityQueue<int, string> pq = new PriorityQueue<int, string>();
        pq.Enqueue(1, "add");
        pq.Enqueue(2, "divide");
        pq.Enqueue(3, "multiply");
        PrintList(pq.ToArray());
        PriorityQueue<int, string> pq2 = new PriorityQueue<int, string>(pq);
        Debug.Log(pq.Dequeue());
        Debug.Log(pq.Dequeue());
        Debug.Log(pq.Dequeue());
        PrintList(pq.ToArray());
        PrintList(pq2.ToArray());
    }

    private void TestPartitionedList()
    {
        PartitionedList<float> plist = new PartitionedList<float>(new float[] { 1, 2, 3, 4, 5 });
        PrintList(plist.ToArray());
        plist.Combine(6, 1, 2);
        PrintList(plist.ToArray());
        plist.Combine(1.5f, 2, 3);
        PrintList(plist.ToArray());
        plist.Combine(2.5f, 0, 1);
        PrintList(plist.ToArray());
        plist.Combine(-2.5f, 3, 4);
        PrintList(plist.ToArray());
        plist[1] = 3;
        PrintList(plist.ToArray());
        plist[4] = 7;
        PrintList(plist.ToArray());
    }

    private void TestExpressionEvaluator()
    {
        float[] numbers;
        Operator[] operators;

        // 1 + 2 = 3
        numbers = new float[] { 1, 2 };
        operators = new Operator[] { Operator.Add };
        Debug.Log(ExpressionEvaluator.Evaluate(numbers, operators).ToString() + " == 3");

        // 1 - 2 = -1
        numbers = new float[] { 1, 2 };
        operators = new Operator[] { Operator.Subtract };
        Debug.Log(ExpressionEvaluator.Evaluate(numbers, operators).ToString() + " == -1");

        // 1 * 2 = 2
        numbers = new float[] { 1, 2 };
        operators = new Operator[] { Operator.Multiply };
        Debug.Log(ExpressionEvaluator.Evaluate(numbers, operators).ToString() + " == 2");

        // 1 / 2 = 0.5
        numbers = new float[] { 1, 2 };
        operators = new Operator[] { Operator.Divide };
        Debug.Log(ExpressionEvaluator.Evaluate(numbers, operators).ToString() + " == 0.5");
    }

    private void TestExpressionEvaluator2()
    {
        float[] numbers;
        Operator[] operators;

        // 1 + 2 + 3 = 6
        numbers = new float[] { 1, 2, 3 };
        operators = new Operator[] { Operator.Add, Operator.Add };
        Debug.Log(ExpressionEvaluator.Evaluate(numbers, operators).ToString() + " == 6");

        // 1 + 2 - 3 = 0
        numbers = new float[] { 1, 2, 3 };
        operators = new Operator[] { Operator.Add, Operator.Subtract };
        Debug.Log(ExpressionEvaluator.Evaluate(numbers, operators).ToString() + " == 0");

        // 1 - 2 + 3 = 2
        numbers = new float[] { 1, 2, 3 };
        operators = new Operator[] { Operator.Subtract, Operator.Add };
        Debug.Log(ExpressionEvaluator.Evaluate(numbers, operators).ToString() + " == 2");

        // 1 + 2 * 3 / 4 - 5 = -2.5 
        numbers = new float[] { 1, 2, 3, 4, 5 };
        operators = new Operator[] { Operator.Add, Operator.Multiply, Operator.Divide, Operator.Subtract };
        Debug.Log(ExpressionEvaluator.Evaluate(numbers, operators).ToString() + " == -2.5");
    }

    private void TestExpressionTree()
    {
        var a = Expression.Constant(1, typeof(int));
        var b = Expression.Constant(2, typeof(int));
        var addition = Expression.Add(a, b);
        var lambda = Expression.Lambda(addition);
        System.Func<int> c = (System.Func<int>)lambda.Compile();
        Debug.Log(c());
    }
}
