using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class UsingTask : MonoBehaviour
{   
    void Start()
    {
        string srcFile = "UsingTask";

        Action<object> FileCopyAction = (object state) =>
        {
            string[] paths = (string[])state;
            File.Copy(paths[0], paths[1]);

            Debug.Log($"TaskID: {Task.CurrentId} ThreadID: {Thread.CurrentThread.ManagedThreadId} {paths[0]} was copied to {paths[1]}");
        };

        Task t1 = new Task(FileCopyAction, new string[] { srcFile, srcFile + ".copy1" });
        Task t2 = Task.Run(() => 
        {
            FileCopyAction(new string[] { srcFile, srcFile + ".copy2" });
        });

        t1.Start();

        Task t3 = new Task(
            FileCopyAction,
            new string[] { srcFile, srcFile + ".copy3" });

        t3.RunSynchronously();

        t1.Wait();
        t2.Wait();
        t3.Wait();
    }
}
