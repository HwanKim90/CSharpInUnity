using System;
using System.Threading;
using UnityEngine;

public class Counter2
{
    const int LOOP_COUNT = 1000;

    readonly object thisLock;

    private int count;
    public int Count { get { return count; } }

    public Counter2()
    {
        thisLock = new object();
        count = 0;
    }

    public void Increase()
    {
        int loopCount = LOOP_COUNT;

        while (loopCount-- > 0)
        {
            Monitor.Enter(thisLock);
            try
            {
                count++;
            }
            finally
            {
                Monitor.Exit(thisLock);
            }
            Debug.Log("inc : " + count);
            Thread.Sleep(1);
        }
    }

    public void Decrease()
    {
        int loopCount = LOOP_COUNT;

        while (loopCount-- > 0)
        {
            Monitor.Enter(thisLock);
            try
            {
                count--;
            }
            finally
            {
                Monitor.Exit(thisLock);
            }
            Debug.Log("dec : " + count);
            Thread.Sleep(1);
        }
    }
}



public class UsingMonitor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Counter2 counter = new Counter2();

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
