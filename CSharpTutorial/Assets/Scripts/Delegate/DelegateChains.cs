using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DelegateChains
{
    delegate void Notify(string message);

    class Notifier // 대리자의 인스턴스 EventOccured를 가지는 클래스 선언
    {
        public Notify EventOccured;
    }

    class EventListener
    {
        private string name;
        public EventListener(string name)
        {
            this.name = name;
        }

        public void SomethingHappend(string message)
        {
            Debug.Log($"{name}.SomethingHappend: {message}");
        }
    }

    public class DelegateChains : MonoBehaviour
    {
        void Start()
        {
            Notifier notifier = new Notifier();
            EventListener listener1 = new EventListener("Listener1");
            EventListener listener2 = new EventListener("Listener2");
            EventListener listener3 = new EventListener("Listener3");

            notifier.EventOccured += listener1.SomethingHappend;
            notifier.EventOccured += listener2.SomethingHappend;
            notifier.EventOccured += listener3.SomethingHappend;
            notifier.EventOccured("You've got mail");
        }
    }
}

