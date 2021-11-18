using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveLoadManager : MonoBehaviour
{
    MainController m_mainController;
    ScoreManager m_socreManager;

    private void Awake()
    {
        m_mainController = GameObject.Find(Constants.s_gameManagerObjectName).GetComponent<MainController>();
        m_socreManager = GameObject.Find(Constants.s_gameManagerObjectName).GetComponent<ScoreManager>();
    }
    private void Start()
    {
        
    }

    public void SaveGameState(List<Transform> elements)
    {
        JSONArray jArray = new JSONArray();
        string s = "NO JSON STRING";
        jArray.Clear();
        for (int i = 0; i < elements.Count; i++)
        {
            if (!elements[i].gameObject.activeInHierarchy)
            {
                continue;
            }
            s = JsonUtility.ToJson(elements[i].GetComponent<ElementManager>());
            //print("JSON STRING: " + s);
            JSONNode jNode = JSON.Parse(s);
            jArray.Add(jNode);
        }
        jArray.SaveToBinaryFile(Constants.s_gameStateJsonFilePath);

        Debug.Log("Game State Saved");

    }
    public void LoadGameState()
    {
        try
        {
            JSONNode j = JSONObject.LoadFromBinaryFile(Constants.s_gameStateJsonFilePath);
            print("full json :" + j.ToString());
            for (int i = 0; i < j.Count; i++)
            {
                //string s = j[i].ToString();
                m_mainController.ArrangeElementsOnLoadedPlaces(j[i]["m_parentName"], j[i]["m_elementLevel"]);
                print("Parent Name: " + j[i]["m_parentName"] + " Contains Element With Level: " + j[i]["m_elementLevel"]);
            }
        }
        catch
        {
            Debug.LogWarning("No Previous Saved Game State");
        }
    }
    

}
