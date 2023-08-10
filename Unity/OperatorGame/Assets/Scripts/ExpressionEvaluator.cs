using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ExpressionEvaluator
{
    public static readonly Dictionary<Operator, int> pemdas = new Dictionary<Operator, int>
    {
        // [operator] = priority
        [Operator.Add] = 1,
        [Operator.Subtract] = 1,
        [Operator.Multiply] = 2,
        [Operator.Divide] = 2,
    };

    public ExpressionEvaluator()
    {
    }

    public static float Evaluate(float[] numbers, Operator[] operators)
    {
        LambdaExpression expressionTree = BuildExpressionTree(numbers, operators);
        System.Func<float> solution = (System.Func<float>)expressionTree.Compile();
        return solution();
    }

    private static LambdaExpression BuildExpressionTree(float[] numbers, Operator[] operators)
    {
        // convert numbers to expressions
        List<Expression> expressions = new List<Expression>();
        foreach (float num in numbers)
        {
            expressions.Add(Expression.Constant(num));
        }
        PartitionedList<Expression> expressionsPlist = new PartitionedList<Expression>(expressions);

        // push operators to priority queue
        PriorityQueue<int, int> operatorQueue = new PriorityQueue<int, int>(); // key: priority, value: index of operator
        for (int i = 0; i < operators.Length; i++)
        {
            operatorQueue.Enqueue(pemdas[operators[i]], i);
        }

        // combine expressions by order of operations
        while (operatorQueue.Count > 0)
        {
            int operatorNdx = operatorQueue.Dequeue();
            Expression combinedExpression = BinaryOperatorToExpression(operators[operatorNdx], expressionsPlist[operatorNdx], expressionsPlist[operatorNdx + 1]);
            expressionsPlist.Combine(combinedExpression, operatorNdx, operatorNdx + 1);
        }

        // at this point only one expression should be left; the complete problem
        return Expression.Lambda(expressionsPlist[0]);
    }

    private static Expression BinaryOperatorToExpression(Operator op, Expression left, Expression right)
    {
        switch (op)
        {
            case Operator.Add:
                return Expression.Add(left, right);
            case Operator.Subtract:
                return Expression.Subtract(left, right);
            case Operator.Multiply:
                return Expression.Multiply(left, right);
            case Operator.Divide:
                return Expression.Divide(left, right);
            default:
                return null;
        }
    }

    public int[] OrderOfOperations(Operator[] ops)
    {
        SortedList<int, int> sl = new SortedList<int, int>(); // emulating priority queue with a SortedList
        for (int i = 0; i < ops.Length; i++)
        {
            sl.Add(pemdas[ops[i]], i);
        }
        int[] order = (int[]) sl.Values;
        return order;
    }

    private void ConvertSubtractionAndDivision(float[] operands, string[] operators, out float[] newOperands, out string[] newOperators)
    {
        newOperands = new float[operands.Length];
        newOperators = new string[operators.Length];
        operands.CopyTo(newOperands, 0);
        operators.CopyTo(newOperators, 0);

        for (int i = 0; i < operators.Length; i++)
        {
            if (operators[i] == "subtract")
            {
                newOperands[i+1] *= -1;
                newOperators[i] = "add";
            }
            else if (operators[i] == "divide")
            {
                newOperands[i+1] = 1f / operands[i+1];
                newOperators[i] = "multiply";
            }
        }
    }

    public bool FloatEqualsInt(float f, int i, float tolerance)
    {
        return Mathf.Abs(f - i) <= tolerance;
    }

    


    #region TESTING

    private void Test(string testName, bool success)
    {
        Debug.Log("Test '" + testName + "' result: " + success);
    }

    private void RunTests()
    {
        Test("test1", FloatEqualsInt(0.0001f, 0, 1f) == true);
        Test("test2", FloatEqualsInt(0.0001f, 0, 0f) == false);
        Test("test3", FloatEqualsInt(0f, 0, 0f) == true);

        float[] operands = new float[] { 1, 2, 3, 4, 5 };
        string[] operators = new string[] { "add", "multiply", "divide", "subtract" };
        PrintList(operands);
        PrintList(operators);
        ConvertSubtractionAndDivision(operands, operators, out float[] newOperands, out string[] newOperators);
        PrintList(newOperands);
        PrintList(newOperators);
        PrintList(operands);
        PrintList(operators);
        ConvertSubtractionAndDivision(operands, operators, out operands, out operators);
        PrintList(operands);
        PrintList(operators);
    }

    private void PrintList<T>(T[] list)
    {
        string s = "{ ";
        foreach (object o in list)
        {
            s += o.ToString() + ", ";
        }
        Debug.Log(s + "}");
    }

    #endregion
}


