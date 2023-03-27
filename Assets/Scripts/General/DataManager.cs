using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.DataManager
{
    public enum MouseClick {None, Left, Right, Double };
    public enum Senser {None, Distance, SoundA, SoundB, ArcadeButton };
    public enum Servo {None, Rotate };
    //This Class Saves every pieces of data of Assets in the window;
    [System.Serializable]
    public class AssetData
    {
        public MouseClick thisMouseClick;
        public Senser thisSenser;
        public AudioClip thisAudio;
        public AnimationInstance thisAnimation;
        public Servo thisServo;
    }
}
