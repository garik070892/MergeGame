using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    [SerializeField]
    GameObject m_appearancePointsParent;
    
    [SerializeField]
    GameObject m_gridElement;
        
    List<Transform> m_appearancePoints = new List<Transform>();
        
    List<Transform> m_elemets = new List<Transform>();

    TimerManager m_timerManager;
    SaveLoadManager m_saveLoadManager;
    UIController m_uIController;

    void Awake()
    {
        m_timerManager = GameObject.Find(Constants.s_gameManagerObjectName).GetComponent<TimerManager>();
        m_saveLoadManager = GameObject.Find(Constants.s_gameManagerObjectName).GetComponent<SaveLoadManager>();
        m_uIController = GameObject.Find(Constants.s_gameManagerObjectName).GetComponent<UIController>();
        DelegateHandler.StopTimer += SetElementToFreeCell;
    }

    public void StartGame()
    {
        for (int i = 0; i < m_appearancePointsParent.transform.childCount; i++)
        {
            m_appearancePoints.Add(m_appearancePointsParent.transform.GetChild(i));
        }

        for (int i = 0; i < m_appearancePoints.Count; i++)
        {
            CreateElement(null);
        }

        m_saveLoadManager.LoadGameState();
        StartCoroutine(m_uIController.HideStartGamePanel());
    }


    public void ArrangeElementsOnLoadedPlaces(string parentName, int level)
    {
        Transform parent = m_appearancePointsParent.transform.Find(parentName);
        int elementLevel = level;

        for (int i = 0; i < m_elemets.Count; i++)
        {
            if (!m_elemets[i].gameObject.activeInHierarchy)
            {
                if (parentName == "")
                {
                    continue;
                }
                m_elemets[i].SetParent(parent);
                m_elemets[i].GetComponent<ElementManager>().m_elementLevel = elementLevel;
                m_elemets[i].localPosition = Vector3.zero;
                m_elemets[i].gameObject.SetActive(true);
                UpdateColliders();
                return;
            }
        }
    }

    Transform CreateElement(Transform parent)
    {
        if (AvailableFreeSpaceTransform() != null)
        {
            GameObject g = Instantiate(m_gridElement, parent);
            g.name = Constants.s_gridElementName + m_elemets.Count;
            g.transform.localPosition = Vector3.zero;
            g.SetActive(false);
            m_elemets.Add(g.transform);
            return g.transform;
        }
        return null;
    }
       
    void SetElementToFreeCell()
    {
        if (AvailableFreeSpaceTransform() != null)
        {
            for (int i = 0; i < m_elemets.Count; i++)
            {
                if (m_elemets[i].parent == null)
                {
                    m_elemets[i].SetParent(AvailableFreeSpaceTransform());
                    m_elemets[i].localPosition = Vector3.zero;
                    m_elemets[i].GetComponent<ElementManager>().m_elementLevel = 0;
                    m_elemets[i].gameObject.SetActive(true);                    
                    UpdateColliders();
                    return;
                }
            }
        }
        else
        {
            Debug.Log("No Free Space");
        }
    }

    Transform AvailableFreeSpaceTransform()
    {
        for (int i = 0; i < m_appearancePoints.Count; i++)
        {
            if (m_appearancePoints[i].childCount == 0)
            {
                return m_appearancePoints[i];
            }
        }
        return null;
    }

    public void UpdateColliders()
    {
        for (int i = 0; i < m_appearancePoints.Count; i++)
        {
            if (m_appearancePoints[i].childCount > 0)
            {
                m_appearancePoints[i].GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                m_appearancePoints[i].GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetElementToFreeCell();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            m_saveLoadManager.SaveGameState(m_elemets);
        } 
        */
        
    }

    public void QuitApplication()
    {
        m_saveLoadManager.SaveGameState(m_elemets);
        Application.Quit();
    }
         
    private void OnDisable()
    {        
        DelegateHandler.StopTimer -= SetElementToFreeCell;
    }
}
