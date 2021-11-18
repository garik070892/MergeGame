using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class UIController : MonoBehaviour
{
    TimerManager m_timerManager;
    MainController m_mainController;

    [SerializeField]
    Button m_timerButton;

    [SerializeField]
    Button m_startGameButton;

    [SerializeField]
    RectTransform m_startGamePanel;

    [SerializeField]
    Button m_exitButton;

    [SerializeField]
    Text m_scoreText;

    [SerializeField]
    RectTransform m_mergeWarningPanel;

    void Awake()
    {
        m_timerManager = GameObject.Find(Constants.s_gameManagerObjectName).GetComponent<TimerManager>();
        m_mainController = GameObject.Find(Constants.s_gameManagerObjectName).GetComponent<MainController>();
        m_timerButton.onClick.AddListener(OnTimerButtonClick);
        m_exitButton.onClick.AddListener(OnExitButtonClick);
        m_startGameButton.onClick.AddListener(OnStartGameButtonClick);
    }

    void OnStartGameButtonClick()
    {
        m_mainController.StartGame();
    }

    void OnExitButtonClick()
    {
        m_mainController.QuitApplication();
    }

    void OnTimerButtonClick()
    {        
        m_timerManager.UpdateTimerBy(Constants.s_timerSpeedUpValue);
    }
    public void SetTimerButtonValue(float val)
    {
        m_timerButton.transform.GetChild(0).GetComponent<Text>().text = val.ToString();
    }
    public void HideTimer()
    {
        m_timerButton.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void ShowTimer()
    {
        m_timerButton.transform.GetChild(0).gameObject.SetActive(true);
    }

    public IEnumerator HideStartGamePanel()
    {
        m_startGameButton.gameObject.SetActive(false);
        Tween tween = m_startGamePanel.GetComponent<RawImage>().DOFade(0, 2.0f);
        yield return tween.WaitForCompletion();
        m_startGamePanel.gameObject.SetActive(false);
        m_timerManager.StartTimer();
        yield break;
    }

    public IEnumerator ShowMergeWarningPopUp()
    {
        m_mergeWarningPanel.gameObject.SetActive(true);
        Tween tween1 = m_mergeWarningPanel.GetComponent<Image>().DOFade(1,1);
        m_mergeWarningPanel.GetChild(0).GetComponent<Text>().DOFade(1, 1);
        yield return tween1.WaitForCompletion();
        Tween tween2 = m_mergeWarningPanel.GetComponent<Image>().DOFade(0, 1);
        m_mergeWarningPanel.GetChild(0).GetComponent<Text>().DOFade(0, 1);
        yield return tween2.WaitForCompletion();
        m_mergeWarningPanel.gameObject.SetActive(false);
        yield break;
    }
    public void ShowStartGamePanel()
    {
        m_startGamePanel.gameObject.SetActive(true);
    }


    public void SetScoreTextValue(string val)
    {
        m_scoreText.text = val;
    }

    
    void Update()
    {
        
    }
}
