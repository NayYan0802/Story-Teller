using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSAnimation : MonoBehaviour
{
    public Vector2 StartPos { get; set; }
    public Vector2 EndPos { get; set; }
    public Vector2 CurrentPos;

    public float TimeSum { get; set; }

    public void StartAnimation(GameObject _gameObject)
    {
        _gameObject.transform.position = StartPos;
    }

    public SSAnimation()
    { }

    public SSAnimation(Vector2 startPos, Vector2 endPos, float timesum)
    {
        this.StartPos = startPos;
        this.EndPos = endPos;
        this.TimeSum = timesum;
    }
}
