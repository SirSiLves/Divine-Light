using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TestEvent : MonoBehaviour
{

    //public event EventHandler<OnLeftClickEventArgs> OnLeftClick;
    //public class OnLeftClickEventArgs : EventArgs
    //{
    //    public int leftClickCount;
    //}

    //public event ClickLeftDelegate OnDelegateEvent;
    //public delegate void ClickLeftDelegate(float f);

    //public event Action<bool, int> OnActionEvent;



    //private void Update()
    //{
    //    //if (Input.GetMouseButtonDown(1))
    //    //{
    //    //    OnLeftClick?.Invoke(this, new OnLeftClickEventArgs { leftClickCount = 99 });

    //    //    OnDelegateEvent?.Invoke(5.5f);

    //    //    OnActionEvent?.Invoke(true, 55);
    //    //}
    //}





    ////Can be seperated
    //private void Start()
    //{
    //    TestEvent testEvent = FindObjectOfType<TestEvent>(); 
    //    testEvent.OnLeftClick += TestingEvents_OnLeftClick;

    //    testEvent.OnDelegateEvent += ClickEvent_OnDelegateEvent;

    //    testEvent.OnActionEvent += ClickEvent_OnActionEvent;
    //}

    //private void ClickEvent_OnActionEvent(bool arg1, int arg2)
    //{
    //    print("ACTION EVENT" + arg1 + " " + arg2);
    //}

    //private void ClickEvent_OnDelegateEvent(float f)
    //{
    //    print("DELEGATE EVENT" + f);
    //}

    //private void TestingEvents_OnLeftClick(object sender, TestEvent.OnLeftClickEventArgs e)
    //{
    //    print("TEST!!!" + e.leftClickCount);

    //}

    //private void OnDestroy()
    //{
    //    TestEvent testEvent = FindObjectOfType<TestEvent>();
    //    testEvent.OnLeftClick -= TestingEvents_OnLeftClick;
    //}




}
