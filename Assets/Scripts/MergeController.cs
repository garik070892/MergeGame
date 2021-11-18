using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeController : MonoBehaviour
{

    MainController m_mainController;
    UIController m_uIController;
    void Start()
    {
        m_mainController = GameObject.Find(Constants.s_gameManagerObjectName).GetComponent<MainController>();
        m_uIController = GameObject.Find(Constants.s_gameManagerObjectName).GetComponent<UIController>();
    }


    Transform m_lastTouchedElement;
    public void SetLastTouchedElement(Transform element)
    {
        m_lastTouchedElement = element;
    }

    public void TryMergeElements(Transform draggableObj, Transform triggeringObject)
    {
        if (triggeringObject.tag == Constants.s_gridElementTagName)
        {
            // Triggering With GridElement
            if (draggableObj.GetComponent<ElementManager>().m_elementLevel == triggeringObject.GetComponent<ElementManager>().m_elementLevel)
            {
                if (draggableObj.GetComponent<ElementManager>().m_elementLevel == Constants.s_maxUnitLevel)
                {
                    //Warning Case
                    StartCoroutine(m_uIController.ShowMergeWarningPopUp());
                    draggableObj.SetParent(m_lastTouchedElement.parent);
                    draggableObj.transform.localPosition = Vector3.zero;
                    m_mainController.UpdateColliders();
                    return;
                }
                // Can Merge
                draggableObj.SetParent(triggeringObject.parent);
                draggableObj.transform.localPosition = Vector3.zero;
                draggableObj.GetComponent<ElementManager>().m_elementLevel++;
                triggeringObject.SetParent(null);
                triggeringObject.gameObject.SetActive(false);
                DelegateHandler.OnElementMerge();
            }
            else
            {
                // Can Not Merge
                draggableObj.SetParent(m_lastTouchedElement.parent);
                draggableObj.transform.localPosition = Vector3.zero;
                m_mainController.UpdateColliders();
                return;
            }
        }
        else if (triggeringObject.tag == Constants.s_appearancePointTagName)
        {
            // Triggering With AppearancePoint
            draggableObj.SetParent(triggeringObject);
            draggableObj.transform.localPosition = Vector3.zero;
        }
        else
        {
            draggableObj.SetParent(m_lastTouchedElement.parent);
            draggableObj.transform.localPosition = Vector3.zero;
            m_mainController.UpdateColliders();
            return;
        }

        m_mainController.UpdateColliders();
    }

}
