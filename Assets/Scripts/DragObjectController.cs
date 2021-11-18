using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DragObjectController : MonoBehaviour
{
    Vector3 m_offset;
    float m_zCoord;

    MergeController m_mergeController;
    Transform m_triggeringObject;

    private void Awake()
    {
        m_mergeController = GameObject.Find(Constants.s_gameManagerObjectName).GetComponent<MergeController>();
        m_triggeringObject = this.gameObject.transform.parent;
    }

    void OnMouseDown()
    {
        m_zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        m_offset = gameObject.transform.position - GetMouseAsWorldPoint();

        m_mergeController.SetLastTouchedElement(this.gameObject.transform);
    }

    void OnMouseUp()
    { 
        if (this.gameObject.transform.parent.GetComponent<BoxCollider>() != null)
        {
            m_mergeController.TryMergeElements(this.gameObject.transform, m_triggeringObject);            
        }
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + m_offset;
    }

    Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = m_zCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnTriggerEnter(Collider col)
    {
        m_triggeringObject = col.transform;
    }
    private void OnTriggerStay(Collider col)
    {
        m_triggeringObject = col.transform;
    }

}