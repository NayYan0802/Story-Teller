using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInstance : MonoBehaviour
{
    SSAnimation animationIns;
    [SerializeField] private Transform StartPos;
    [SerializeField] private Transform EndPos;

    [SerializeField] private float timeDuration;

    private Vector2 CurrentPos;
    private float timeSum;

    private Subscription<StartPlay> StartplaySub;

    private void Start()
    {
        StartplaySub = EventBus.Subscribe<StartPlay>(_StartPlay);
        timeSum = 0;
        Init();
    }

    private void Init()
    {
        animationIns = new SSAnimation(StartPos.position, EndPos.position, timeDuration);
    }

    void _StartPlay(StartPlay e)
    {
        animationIns.StartAnimation(this.gameObject);
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(StartplaySub);
    }

}
