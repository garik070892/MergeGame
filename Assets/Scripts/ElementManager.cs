using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ElementManager : MonoBehaviour
{
    public int m_elementLevel = 0;
    public string m_parentName;
    SaveLoadManager m_saveLoadManager;

    void Start()
    {
        //UpdateInfo();
        //DelegateHandler.ChangeElementPlace += UpdateInfo;        
    }

    void UpdateInfo()
    {
        if (this.gameObject.transform.parent != null)
        {
            m_parentName = this.gameObject.transform.parent.name;
        }
        this.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = m_elementLevel.ToString();
    }
    
    void Update()
    {
        UpdateInfo();
    }


    private void OnDisable()
    {
        
        //DelegateHandler.ChangeElementPlace -= UpdateInfo;        
    }


}
