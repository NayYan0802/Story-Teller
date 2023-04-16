using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.DataManager;
using TMPro;
using UnityEngine.EventSystems;

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

    [Header("Dynamic Parameters")]

    public onAssetInWindow currentObjectInWindow;
    public TextMeshProUGUI currentTextInWindow;
    public GameObject currentPage;
    public OnAssetInLib currentImageInLib;
    public TMP_InputField inputField;
    public bool isPlaymode;
    public int pageIdx;
    public GameObject[] InSceneGameObjects;
    public List<GameObject> Pages;

    [Header("Prefabs")]

    public GameObject TextIns;

    [Header("Layer")]

    public GameObject[] layerInit;
    public List<GameObject> layerList;

    [Header("ToggleGroup")]

    public Toggle[] LibraryTG;
    public Toggle[] IOTG;
    public Toggle[] EditWindowTG;
    public Toggle[] AngleTG;

    public GameObject[] LibraryWindow;
    public GameObject[] IOWindow;
    public GameObject[] ArduinoIOWindow;
    public GameObject[] EditWindowWindow;

    public GameObject MouseClickTG;
    public GameObject SensorTG;
    public GameObject AnimationTG;
    public GameObject AudioTG;
    public GameObject ServoTG;

    [Header("InSceneObjects")]

    public GameObject TextPopUp;
    public GameObject SpinServoOption;
    public GameObject AngleServoOption;
    public TMP_Dropdown ServoDropDown;
    public Slider TimeOfSpinSlider;
    public GameObject AudioContent;
    public TMP_Dropdown AudioDropDown;
    public AudioManager AudioManager;
    public Assets.EventsManager.EventsManager EventsManager;


    private void Start()
    {
        pageIdx = 0;
        isPlaymode = false;
        layerInit = GameObject.FindGameObjectsWithTag("AssetInWindow");
        for(int i = 0; i < layerInit.Length; i++)
        {
            layerList.Add(layerInit[i]);
        }
    }

    private void Update()
    {
        //UpdatePage(GameObject.Find("Page"), Camera.main);
        updateText();
        /*//UI Debug
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);
            if (raycastResults.Count > 0)
            {
                for (int i = 0; i < raycastResults.Count; i++)
                {
                    Debug.Log(raycastResults[i].gameObject.name);
                }
                Debug.Log(raycastResults.Count);
            }
        }*/
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPlaymode)
            {
                //Exit Playmode
            }
            else
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;                
#else
                Application.Quit();
#endif
            }
        }
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

    public void changeAnimationOutput(string name)
    {
        if (currentObjectInWindow != null)
        {
            currentObjectInWindow.changeAnimationOutput(name);
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

    public void updateText()
    {
        if (currentTextInWindow != null)
        {
            currentTextInWindow.text = inputField.text;
        }
        else
        {
            inputField.text = "New Text";
        }
    }

    public void updateLayer()
    {
        for (int i = 0; i < layerList.Count; i++)
        {
            layerList[i].transform.SetAsLastSibling();
        }
    }

    public void LayerUp()
    {
        if (currentObjectInWindow != null)
        {
            int idx = layerList.IndexOf(currentObjectInWindow.gameObject);
            if (idx != layerList.Count - 1)
            {
                layerList.Remove(currentObjectInWindow.gameObject);
                layerList.Insert(idx + 1, currentObjectInWindow.gameObject);
            }
        }
        else
        {
            Debug.LogError("No Object Selected");
        }
        updateLayer();
    }

    public void LayerDown()
    {
        if (currentObjectInWindow != null)
        {
            int idx = layerList.IndexOf(currentObjectInWindow.gameObject);
            if (idx != 0)
            {
                layerList.Remove(currentObjectInWindow.gameObject);
                layerList.Insert(idx - 1, currentObjectInWindow.gameObject);
            }
        }
        else
        {
            Debug.LogError("No Object Selected");
        }
        updateLayer();
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
                case Senser.Sound:
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

            if (asset.GetComponent<onAssetInWindow>().data.hasMouseClick)
            {
                asset.gameObject.AddComponent<ClickInput>();
            }
            if (asset.GetComponent<onAssetInWindow>().data.hasAnimation)
            {
                asset.gameObject.AddComponent<AnimationOutput>();
            }

            asset.GetComponent<onAssetInWindow>().setOriginPos();
            asset.GetComponent<onAssetInWindow>().isPlayMode = true;
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

    public void ChangeAudioCheck()
    {
        int cCount=AudioContent.transform.childCount;
        AudioSource[] changeGroup=new AudioSource[0];
        for(int i = cCount-1; i >= 0; i--)
        {
            AudioContent.transform.GetChild(i).gameObject.SetActive(false);
        }
        switch (AudioDropDown.options[AudioDropDown.value].text)
        {
            case "Animals":
                changeGroup = AudioManager.Animals;
                break;
            case "Background":
                changeGroup = AudioManager.BackGournd;
                break;
            case "Clocks":
                changeGroup = AudioManager.Clocks;
                break;
            case "Crowds":
                changeGroup = AudioManager.Crowds;
                break;
            case "House":
                changeGroup = AudioManager.House;
                break;
            case "Human":
                changeGroup = AudioManager.Human;
                break;
            case "Music":
                changeGroup = AudioManager.Music;
                break;
            case "Nature":
                changeGroup = AudioManager.Nature;
                break;
            case "Sports":
                changeGroup = AudioManager.Sports;
                break;
            case "Technology":
                changeGroup = AudioManager.Technology;
                break;
            case "Vehicles":
                changeGroup = AudioManager.Vehicles;
                break;
            case "Weather":
                changeGroup = AudioManager.Weather;
                break;
            case "Cartoon":
                changeGroup = AudioManager.Cartoon;
                break;
            case "Other":
                changeGroup = AudioManager.Other;
                break;
            default:
                break;
        }
        foreach(var audio in changeGroup)
        {
            audio.gameObject.SetActive(true);
        }
    }

    public void ToggleGroupCheck()
    {
        for (int i = 0; i < LibraryTG.Length;i++)
        {
            if(LibraryTG[i].isOn)
            {
                LibraryTG[i].transform.GetChild(2).GetComponent<Image>().enabled = true;
                LibraryWindow[i].SetActive(true);
            }
            else
            {
                LibraryTG[i].transform.GetChild(2).GetComponent<Image>().enabled = false;
                LibraryWindow[i].SetActive(false);
            }
        } 
        for (int i = 0; i < IOTG.Length;i++)
        {
            if(IOTG[i].isOn)
            {
                IOTG[i].transform.GetChild(2).GetComponent<Image>().enabled = true;
                if (LibraryTG[2].isOn)
                {
                    IOWindow[i].SetActive(false);
                    ArduinoIOWindow[i].SetActive(true);
                }
                if (!LibraryTG[2].isOn)
                {
                    IOWindow[i].SetActive(true);
                    ArduinoIOWindow[i].SetActive(false);
                }
            }
            else
            {
                IOTG[i].transform.GetChild(2).GetComponent<Image>().enabled = false;
                IOWindow[i].SetActive(false);
                ArduinoIOWindow[i].SetActive(false);
            }
        } 
        for (int i = 0; i < EditWindowTG.Length;i++)
        {
            if(EditWindowTG[i].isOn)
            {
                EditWindowTG[i].transform.GetChild(2).GetComponent<Image>().enabled = true;
                EditWindowWindow[i].SetActive(true);
            }
            else
            {
                EditWindowTG[i].transform.GetChild(2).GetComponent<Image>().enabled = false;
                EditWindowWindow[i].SetActive(false);
            }
        } 
    }

    public void EventInputStatusCheck()
    {
        if (!currentObjectValidationCheck())
        {
            return;
        }
        if (MouseClickTG.GetComponent<Toggle>().isOn)
        {
            currentObjectInWindow.data.hasMouseClick = true;
        }
        else
        {
            currentObjectInWindow.data.hasMouseClick = false;
        }
        if (SensorTG.GetComponent<Toggle>().isOn)
        {
            currentObjectInWindow.data.hasSensor = true;
        }
        else
        {
            currentObjectInWindow.data.hasSensor = false;
        }
    }

    public void EventOutputStatusCheck()
    {
        if (!currentObjectValidationCheck())
        {
            return;
        }
        if (AnimationTG.GetComponent<Toggle>().isOn)
        {
            currentObjectInWindow.data.hasAnimation = true;
        }
        else
        {
            currentObjectInWindow.data.hasAnimation = false;
        }
        if (AudioTG.GetComponent<Toggle>().isOn)
        {
            currentObjectInWindow.data.hasAudio = true;
        }
        else
        {
            currentObjectInWindow.data.hasAudio = false;
        }
        if (ServoTG.GetComponent<Toggle>().isOn)
        {
            currentObjectInWindow.data.hasServo = true;
        }
        else
        {
            currentObjectInWindow.data.hasServo = false;
        }
    }

    public void InputServoTypeCheck()
    {
        if (!currentObjectValidationCheck())
        {
            return;
        }
        if (currentObjectInWindow.data.hasServo)
        {
            if (ServoDropDown.options[ServoDropDown.value].text.Contains("Spin"))
            {
                SpinServoOption.SetActive(true);
                AngleServoOption.SetActive(false);
            }
            else if (ServoDropDown.options[ServoDropDown.value].text.Contains("Angle"))
            {
                SpinServoOption.SetActive(false);
                AngleServoOption.SetActive(true);
            }
            else
            {
                Debug.LogError("WrongServoType");
            }
        }
        //no servo
        else
        {
            SpinServoOption.SetActive(false);
            AngleServoOption.SetActive(false);
        }
    }

    public void ServoValueUpdate()
    {
        if (!currentObjectValidationCheck())
        {
            return;
        }
        if(currentObjectInWindow.data.hasServo && ServoDropDown.options[ServoDropDown.value].text.Contains("Spin"))
        {
            currentObjectInWindow.data.timeOfSpin = (int)TimeOfSpinSlider.value;
        }
        else if(currentObjectInWindow.data.hasServo && ServoDropDown.options[ServoDropDown.value].text.Contains("Angle"))
        {
            for(int i = 0; i < AngleTG.Length; i++)
            {
                if (AngleTG[i].isOn)
                {
                    //0 45 90 135 180
                    currentObjectInWindow.data.AnglesOfSpin = i * 45;
                }
            }
        }
    }

    public bool currentObjectValidationCheck()
    {
        if (currentObjectInWindow == null)
        {
            Debug.LogError("No selected object");
            return false;
        }
        else
        {
            return true;
        }    
    }

    public void currentObjectUpdate()
    {
        InSceneGameObjects = GameObject.FindGameObjectsWithTag("AssetInWindow");
        foreach(var gameObject in InSceneGameObjects)
        {
            if (gameObject.transform.childCount >0)
            {
                if (gameObject == currentObjectInWindow.gameObject)
                {
                    gameObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
                    gameObject.transform.GetChild(0).GetComponent<onDragPoint>().enabled = true;
                }
                else
                {
                    gameObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
                    gameObject.transform.GetChild(0).GetComponent<onDragPoint>().enabled = false;
                }
            }
        }
        LoadCurrentObjectData();
    }

    public void LoadCurrentObjectData()
    {
        //output
        if (currentObjectInWindow.data.hasAnimation)
        {
            AnimationTG.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            AnimationTG.GetComponent<Toggle>().isOn = false;
        }
        if (currentObjectInWindow.data.hasAudio)
        {
            AudioTG.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            AudioTG.GetComponent<Toggle>().isOn = false;
        }
        if (currentObjectInWindow.data.hasServo)
        {
            ServoTG.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            ServoTG.GetComponent<Toggle>().isOn = false;
        }

        //input
        if (currentObjectInWindow.data.hasMouseClick)
        {
            MouseClickTG.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            MouseClickTG.GetComponent<Toggle>().isOn = false;
        }
        if (currentObjectInWindow.data.hasSensor)
        {
            SensorTG.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            SensorTG.GetComponent<Toggle>().isOn = false;
        }
    }

    public void PopUpInputField()
    {
        TextPopUp.SetActive(true);
    }

    public void newText()
    {
        GameObject ins=Instantiate(TextIns,currentPage.transform);
        currentTextInWindow = ins.GetComponent<TextMeshProUGUI>();
        RefreshInputField();
    }

    public void RefreshInputField()
    {
        inputField.text = "New Text";
    }

    public void DeleteAssetsInEditWindow()
    {
        if (!currentObjectValidationCheck())
        {
            return;
        }
        if(currentObjectInWindow.gameObject.GetComponent<TextMeshProUGUI>())
        {
            currentTextInWindow = null;
        }
        Destroy(currentObjectInWindow.gameObject);
        currentObjectInWindow = null;
    }

    public void AudioPreview()
    {
        GameObject.Find("AudioPreviewButton").GetComponent<AudioSource>().PlayOneShot(currentObjectInWindow.data.thisAudio);
    }

    public void DeleteAssetInLib()
    {
        if (currentImageInLib == null)
        {
            return;
        }
        Destroy(currentImageInLib.gameObject);
        currentImageInLib = null;
    }
}
