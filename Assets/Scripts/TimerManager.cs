using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    UIController m_uIController;
    MergeController m_mergeController;


    float m_timeRemaining;
    bool m_timerIsRunning = false;


    private void Start()
    {

        m_uIController = GameObject.Find(Constants.s_gameManagerObjectName).GetComponent<UIController>();
        m_mergeController = GameObject.Find(Constants.s_gameManagerObjectName).GetComponent<MergeController>();

        DelegateHandler.StopTimer += StartTimer;
    }

    public void StartTimer()
    {        
        m_timeRemaining = Constants.s_timeRemainingValue;
        m_timerIsRunning = true;
    }
    public void StopTimer()
    {
        m_timerIsRunning = false;
    }
    public void UpdateTimerBy(float val)
    {
        m_timeRemaining -= val;
    }


    void Update()
    {
        if (m_timerIsRunning)
        {
            if (m_timeRemaining > 0)
            {
                m_timeRemaining -= Time.deltaTime;
                DisplayTime(m_timeRemaining);
            }
            else
            {
                // Timer Stopped
                m_timeRemaining = 0;
                m_timerIsRunning = false;                
                DelegateHandler.OnStopTimer();
            }
        }
    }


    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        m_uIController.SetTimerButtonValue(seconds);

    }

    private void OnDisable()
    {
        DelegateHandler.StopTimer -= StartTimer;
    }
}
