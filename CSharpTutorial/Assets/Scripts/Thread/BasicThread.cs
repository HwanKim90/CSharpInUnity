using System;
using System.Threading;
using UnityEngine;

public class BasicThread : MonoBehaviour
{
    void DoSomething()
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log($"DoSomething : {i}");
            Thread.Sleep(10); // sleep �޼ҵ带 ������ �μ�(10)��ŭ cpu��� ����. 
            // 1000 �и��� --> 1��, 10�и��� -> 0.01��
        }
    }
    
    void Start()
    {
        Thread t1 = new Thread(new ThreadStart(DoSomething));

        Debug.Log("starting thread...");
        t1.Start();

        // t1 �������� ����Ǵµ��� ���ο����� �ݺ��� ����
        for (int i = 0; i < 5; i++)  
        {
            Debug.Log($"Main : {i}");
            Thread.Sleep(10);
        }

        Debug.Log("Waiting until thread stop..");
        t1.Join();

        Debug.Log("finish");
    }
}
