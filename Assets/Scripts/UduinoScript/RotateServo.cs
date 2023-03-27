using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class RotateServo : MonoBehaviour
{
    UduinoDevice MyBoard;

    //Init sub
    private Subscription<RotateServoData> RotateServoSub;

    // Start is called before the first frame update
    void Start()
    {
        MyBoard = UduinoManager.Instance.GetBoard("StoryStudio");
        //Assign sub event
        RotateServoSub = EventBus.Subscribe<RotateServoData>(_RotateServo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //sub event
    private void _RotateServo(RotateServoData e)
    {
        StartCoroutine(DelayedSendCommand("ServoRun",e.delayTime, e.ServoPos));
    }


    IEnumerator DelayedSendCommand(string command, float delayTime, float Num1)
    {
        yield return new WaitForSeconds(delayTime);
        UduinoManager.Instance.sendCommand(MyBoard, command, Num1);
    }

}
