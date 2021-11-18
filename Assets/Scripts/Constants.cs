using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public const string s_gameManagerObjectName = "GameManager";
    public const string s_gridElementName = "Element_";
    public const string s_gridElementTagName = "GridElement";
    public const string s_appearancePointTagName = "AppearancePoint";

    public static readonly string s_gameStateJsonFilePath = Application.persistentDataPath + "/GameState.json";

    public const float s_timerSpeedUpValue = 1.0f;
    public const float s_timeRemainingValue = 5.0f;

    public const int s_maxUnitLevel = 4;



}
