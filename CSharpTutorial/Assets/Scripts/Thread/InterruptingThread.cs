using System;
using System.Security.Permissions;
using System.Threading;
using UnityEngine;

class SideTask2
{
    int count;

    public SideTask2(int count)
    {
        this.count = count; 
    }

    public void KeepAlive()
    {
        try
        {
            Debug.Log("Running thread isn't gonna be interrupted");
            Thread.SpinWait(1000000000);

            while (count > 0)
            {
                Debug.Log($"{count--} left");

                Debug.Log("Entering into WaitJoinSleep state");
                Thread.Sleep(10);
            }
            Debug.Log("Count: 0");
        }
        catch (ThreadInterruptedException e)
        {
            Debug.Log(e);
        }
        finally
        {
            Debug.Log("Clearing resource...");
        }
    }
}

public class InterruptingThread : MonoBehaviour
{   
    void Start()
    {
        SideTask2 task = new SideTask2(100);
        Thread t1 = new Thread(new ThreadStart(task.KeepAlive));
        t1.IsBackground = false;

        Debug.Log("Starting thread..");
        t1.Start();

        Thread.Sleep(10);

        Debug.Log("Interrupting thread...");
        t1.Interrupt();

        Debug.Log("Wating until thread stops..");
        t1.Join();

        Debug.Log("finished");
    }
}
