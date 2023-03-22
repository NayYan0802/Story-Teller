using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.DataManager
{
    public enum MouseClick { Left, Right, Double, None };
    public enum Senser { Distance, SoundA, SoundB, ArcadeButton, None };
    //This Class Saves every pieces of data of Assets in the window;
    [System.Serializable]
    public class AssetData
    {
        public MouseClick thisMouseClick;
        public Senser thisSenser;
        public AudioClip thisAudio;
        public AnimationInstance thisAnimation;
        public UduinoCallServo thisServo;
    }

    public class DataManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
