using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.DataManager;

public class onAssetInWindow : MonoBehaviour
{
    public AssetData data;
    public bool isPlayMode = false;

    //Open up the setting window
    public void OpenSetting()
    {
        //Instead of setactive() the object, set the scale from 0 to 1 to achieve the same effect,
        //this way we can use GameObject.Find() cause it's active.
        GameObject.Find("InteractionSetting").transform.localScale = Vector3.one;
        GameManager.Instance.currentObjectInWindow = this;
    }

    public void changeMouseInput(string input)
    {
        switch (input)
        {
            case "Left":
                data.thisMouseClick = MouseClick.Left;
                break;
            case "Right":
                data.thisMouseClick = MouseClick.Right;
                break;
            case "Double":
                data.thisMouseClick = MouseClick.Double;
                break;
            case "None":
                data.thisMouseClick = MouseClick.None;
                break;
            default:
                Debug.LogError("Error MouseClick Type");
                break;
        }
    }

    public void changeSensorInput(string input)
    {
        switch (input)
        {
            case "Distance":
                data.thisSenser = Senser.Distance;
                break;
            case "SoundA":
                data.thisSenser = Senser.SoundA;
                break;
            case "SoundB":
                data.thisSenser = Senser.SoundB;
                break;
            case "ArcadeButton":
                data.thisSenser = Senser.ArcadeButton;
                break;
            default:
                Debug.LogError("Error Sensor Type");
                break;
        }
    }
}
