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
            Thread.Sleep(10); // sleep 메소드를 만나면 인수(10)만큼 cpu사용 멈춤. 
            // 1000 밀리초 --> 1초, 10밀리초 -> 0.01초
        }
    }
    
    void Start()
    {
        Thread t1 = new Thread(new ThreadStart(DoSomething));

        Debug.Log("starting thread...");
        t1.Start();

        // t1 쓰레드의 실행되는동안 메인에서도 반복문 실행
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
