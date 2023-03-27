using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlay
{
    public StartPlay() { }
}

//=========================================Uduino==========================================

public class RotateServoData
{
    public float delayTime;
    public float ServoPos;
    public RotateServoData(float _delayTime, float _Servopos)
    {
        delayTime = _delayTime;
        ServoPos = _Servopos;
    }
}
