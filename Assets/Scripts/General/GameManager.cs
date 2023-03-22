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
