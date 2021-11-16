using System;
using System.Threading;
using UnityEngine;


public class Counter3
{
    const int LOOP_COUNT = 1000;

    readonly object thisLock;
    bool lockedCount = false; // count������ �ٸ� �����尡 ����ϰ� �ִ��� �Ǵ�

    private int count; // �� �����尡 �ʹ� �������� count������ ȥ�� ����ϴ� ���� ���´�
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
                while (count > 0 || lockedCount == true) // ���� �����带 ��Ͻ�Ų��.
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
                while (count < 0 || lockedCount == true) // ���� �����带 ��Ͻ�Ų��.
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
