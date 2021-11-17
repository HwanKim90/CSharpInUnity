using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskResult : MonoBehaviour
{
    // 소수 찾기
    bool IsPrime(long number)
    {
        if (number < 2)
            return false;

        if (number % 2 == 0 && number != 2)
            return false;

        for (long i = 2; i < number; i++)
        {
            if (number % i == 0)
                return false;
        }

        return true;
    }


    void Start()
    {
        //long from = Convert.ToInt64
    }

    
    void Update()
    {
        
    }
}
