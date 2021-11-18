using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateHandler : MonoBehaviour
{    
    public delegate void TimerStop();
    public static event TimerStop StopTimer;
    public static void OnStopTimer()
    {
        StopTimer();
    }

    public delegate void ElementMerge();
    public static event ElementMerge MergeElement;
    public static void OnElementMerge()
    {
        MergeElement();
    }


    /*public delegate void ElementPlaceChange();
    public static event ElementPlaceChange ChangeElementPlace;
    public static void OnElementElementPlaceChange()
    {
        ChangeElementPlace();
    }*/

}
