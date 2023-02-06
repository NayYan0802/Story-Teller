using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInstance : MonoBehaviour
{
    private SSAnimation animationIns;

    [SerializeField] private Transform StartPos;
    [SerializeField] private Transform EndPos;
    [SerializeField] private float timeDuration;

    [SerializeField] private Transform[] posArray;

    [SerializeField] private float[] timeArray;
    [SerializeField] private int AniIdx;

    private Vector2 CurrentPos;
    private float timeSum;

    private Subscription<StartPlay> StartplaySub;

    private void Start()
    {
        AniIdx = 0;
        StartplaySub = EventBus.Subscribe<StartPlay>(_StartPlay);
        timeSum = 0;
        Init();

        //Check Input Validation
        if (posArray.Length != timeArray.Length + 1)
        {
            Debug.LogError("Error pos/time array imput");
        }
    }

    private void Init()
    {
        //animationIns = new SSAnimation(StartPos, EndPos, timeDuration, this.gameObject);
        animationIns = new SSAnimation(posArray, timeArray, this.gameObject);
    }

    void _StartPlay(StartPlay e)
    {
        //animationIns.StartAnimation();
        StartCoroutine(animationIns.LinearAnimation());
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(StartplaySub);
    }

}
