using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Operator
{
    Add,
    Subtract,
    Multiply,
    Divide
}


// Based on following link:
// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-create-a-new-method-for-an-enumeration
public static class OperatorExtensions
{
    public static Operator Random(this Operator op)
    {
        Operator[] values = (Operator[]) System.Enum.GetValues(typeof(Operator));
        return values[UnityEngine.Random.Range(0, values.Length)];
    }
}
