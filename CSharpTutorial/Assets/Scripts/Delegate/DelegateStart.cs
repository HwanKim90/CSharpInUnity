using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Calculator
{
    public int Plus(int a, int b)
    {
        return a + b;
    }

    public static int Minus(int a, int b)
    {
        return a - b;
    }
}

public class DelegateStart : MonoBehaviour
{
    delegate int MyDelegate(int a, int b);

    void Start()
    {
        Calculator calc = new Calculator();
        MyDelegate Callback;

        Callback = new MyDelegate(calc.Plus);
        Debug.Log(Callback(3, 4));

        Callback = new MyDelegate(Calculator.Minus);
        Debug.Log(Callback(5, 2));
    }
}
