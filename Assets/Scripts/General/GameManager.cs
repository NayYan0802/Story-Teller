using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.DataManager;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("GameManager is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion
    //public Vector2 pos;
    //public Vector2 xy;

    public onAssetInWindow currentObjectInWindow;
    public bool isPlaymode;

    private void Start()
    {
        isPlaymode = false;
    }

    private void Update()
    {
        //UpdatePage(GameObject.Find("Page"), Camera.main);
    }
    public void OnStartButtonClick()
    {
        EventBus.Publish(new StartPlay());
    }

    public void changeMouseInput(string name)
    {
        if (currentObjectInWindow != null)
        {
            currentObjectInWindow.changeMouseInput(name);
        }
        else
        {
            Debug.LogError("Current Object in window is null!");
        }
    }

    public void changeSensorInput(string name)
    {
        if (currentObjectInWindow != null)
        {
            currentObjectInWindow.changeSensorInput(name);
        }
        else
        {
            Debug.LogError("Current Object in window is null!");
        }
    }

    public void changeServo(string name)
    {
        if (currentObjectInWindow != null)
        {
            currentObjectInWindow.changeServo(name);
        }
        else
        {
            Debug.LogError("Current Object in window is null!");
        }
    }

    public void startPlayMode()
    {
        GameObject[] assetsInWindow;
        assetsInWindow = GameObject.FindGameObjectsWithTag("AssetInWindow");
        //Debug.Log(assetsInWindow.Length);
        foreach(var asset in assetsInWindow)
        {
            Senser senser =asset.GetComponent<onAssetInWindow>().data.thisSenser;
            Assets.DataManager.Servo servo=asset.GetComponent<onAssetInWindow>().data.thisServo;
            switch (senser)
            {
                case Senser.Distance:
                    asset.gameObject.AddComponent<DistanceSensor>();
                    //Debug.Log("Add");
                    break;
                case Senser.SoundA:
                    asset.gameObject.AddComponent<SoundSensor>();
                    break;
                default:
                    break;
            }

            switch (servo)
            {
                case Assets.DataManager.Servo.Rotate:
                    asset.gameObject.AddComponent<RotateServo>();
                    //Debug.Log("Add");
                    break;
                default:
                    break;
            }
        }
    }
    
    //public void UpdatePage(GameObject page, Camera c)
    //{
    //    Texture2D CamTexture;
    //    Rect r = new Rect(pos,xy);
    //    RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 0);
    //    c.targetTexture = rt;
    //    c.Render();
    //    RenderTexture.active = rt;
    //    CamTexture = new Texture2D((int)xy.x, (int)xy.y, TextureFormat.RGB24, false);
    //    CamTexture.ReadPixels(r, 0, 0);
    //    CamTexture.Apply();
    //    c.targetTexture = null;
    //    RenderTexture.active = null;
    //    page.GetComponent<RawImage>().texture = CamTexture;
    //    Destroy(rt);
    //}
}
