using System;
using System.Threading;
using UnityEngine;

class SideTask
{
    int count;

    public SideTask(int count)
    {
        this.count = count;
    }

    public void KeepAlive()
    {
        try
        {
            while (count > 0)
            {
                Debug.Log($"{count--} left");
                Thread.Sleep(10);
            }

            Debug.Log("Count : 0");
        }
        catch (ThreadAbortException e)
        {
            Debug.Log(e);
            Thread.ResetAbort();
        }
        finally
        {
            Debug.Log("Clearing resource");
        }
    }
}

public class AbortingThread : MonoBehaviour
{
    void Start()
    {
        SideTask task = new SideTask(100);
        Thread t1 = new Thread(new ThreadStart(task.KeepAlive));
        t1.IsBackground = false;

        Debug.Log("Starting Thread");
        t1.Start();
        Thread.Sleep(1000);

        Debug.Log("Aborting thread...");
        t1.Abort(); // --> 스레드취소.

        Debug.Log("Waiting until thread stop");
        t1.Join();

        Debug.Log("finish");

    }
}
