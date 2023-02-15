using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        EventBus.Publish(new StartPlay());
    }
}
