using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Problem
{
    private int[] numbers;
    private int[] operators;
    public float solution => _solution();

    public Problem()
    {
        numbers = new int[0];
        operators = new int[0];
    }

    private float _solution()
    {
        return 0f;
    }
}
