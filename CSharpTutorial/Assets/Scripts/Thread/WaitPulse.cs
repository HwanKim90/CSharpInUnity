using System;
using System.Threading;
using UnityEngine;


public class Counter3
{
    const int LOOP_COUNT = 1000;

    readonly object thisLock;
    bool lockedCount = false; // count변수를 다른 스레드가 사용하고 있는지 판단

    private int count; // 각 스레드가 너무 오랫동안 count변수를 혼자 사용하는 것을 막는다
    public int Count { get { return count; } }

    public Counter3()
    {
        thisLock = new object();
        count = 0;
    }

    public void Increase()
    {
        int loopCount = LOOP_COUNT;

        while (loopCount-- > 0)
        {
            lock (thisLock)
            {
                while (count > 0 || lockedCount == true) // 현재 스레드를 블록시킨다.
                {
                    Monitor.Wait(thisLock);
                }

                lockedCount = true;
                count++;
                lockedCount = false;

                Debug.Log("Inc : " + count);
                Monitor.Pulse(thisLock);
            }
        }
    }

    public void Decrease()
    {
        int loopCount = LOOP_COUNT;

        while (loopCount-- > 0)
        {
            lock (thisLock)
            {
                while (count < 0 || lockedCount == true) // 현재 스레드를 블록시킨다.
                {
                    Monitor.Wait(thisLock);
                }

                lockedCount = true;
                count--;
                lockedCount = false;

                Debug.Log("Dec : " + count);
                Monitor.Pulse(thisLock);
            }
        }
    }
}

public class WaitPulse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Counter3 counter = new Counter3();

        Thread incThread = new Thread(new ThreadStart(counter.Increase));
        Thread decThread = new Thread(new ThreadStart(counter.Decrease));

        incThread.Start();
        decThread.Start();

        incThread.Join();
        decThread.Join();

        Debug.Log(counter.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
