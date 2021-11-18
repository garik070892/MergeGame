using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{         
    float m_currentScore = 0.0f;
    UIController m_uIController;

    void UpdateCurrentScore()
    {
        m_currentScore += 1;
        m_uIController.SetScoreTextValue(m_currentScore.ToString());
    }
    void Start()
    {
        m_uIController = GameObject.Find(Constants.s_gameManagerObjectName).GetComponent<UIController>();
        DelegateHandler.MergeElement += UpdateCurrentScore;
    }

    
    void Update()
    {
        
    }

    private void OnDisable()
    {
        DelegateHandler.MergeElement -= UpdateCurrentScore;
    }
}
