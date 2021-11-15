using System;
using System.Threading;
using UnityEngine;

public class Counter
{
    const int LOOP_COUNT = 100;

    readonly object thisLock;

    private int count;
    public int Count
    {
        get { return count; }
    }

    public Counter()
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
                count++;
            }
            //count++;
            Debug.Log("inc : " + count);
            Thread.Sleep(1);
        }
    }

    public void Decrease()
    {
        int loopCount = LOOP_COUNT;

        while (loopCount-- > 0)
        {
            lock (thisLock)
            {
                count--;
            }
            //count--;
            Debug.Log("dec : " + count);
            Thread.Sleep(1);
        }
    }

}

public class SynchronizeLock : MonoBehaviour
{  
    void Start()
    {
        Counter counter = new Counter();

        Thread incThread = new Thread(new ThreadStart(counter.Increase));
        Thread decThread = new Thread(new ThreadStart(counter.Decrease));

        incThread.Start();
        decThread.Start();

        incThread.Join();
        decThread.Join();

        Debug.Log("result : " + counter.Count);
    }
}
