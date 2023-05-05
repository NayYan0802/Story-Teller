using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOutput : MonoBehaviour
{
    onAssetInWindow onAsset;


    //Init sub
    private Subscription<FlyInEvent> FlyInEventSub;
    private Subscription<FlyOutEvent> FlyOutEventSub;

    Vector2 pos;
    Vector2 dir;
    Vector2 des;



    // Start is called before the first frame update
    public void Start()
    {
        //onAsset = this.GetComponent<onAssetInWindow>();
        //Assign sub event
        //FlyInEventSub = EventBus.Subscribe<FlyInEvent>(_FlyInEvent);
        //FlyOutEventSub = EventBus.Subscribe<FlyOutEvent>(_FlyOutEvent);
        pos = this.GetComponent<RectTransform>().position;
        //pos = this.GetComponent<onAssetInWindow>().originPos;
        dir.x = pos.x / Vector2.Distance(pos, Vector2.zero);
        dir.y = pos.y / Vector2.Distance(pos, Vector2.zero);
        des = pos + dir * 1000;
        if (this.GetComponent<onAssetInWindow>().data.hasAnimation&& 
            this.GetComponent<onAssetInWindow>().data.thisAnimation== Assets.DataManager.AnimationType.In)
        {
            this.GetComponent<RectTransform>().position = des;
        }
    }

    public void Prepare()
    {
        pos = this.GetComponent<RectTransform>().position;
        Debug.Log(pos);
        dir.x = pos.x / Vector2.Distance(pos, Vector2.zero);
        dir.y = pos.y / Vector2.Distance(pos, Vector2.zero);
        des = pos + dir * 1000;
        if (this.GetComponent<onAssetInWindow>().data.hasAnimation &&
            this.GetComponent<onAssetInWindow>().data.thisAnimation == Assets.DataManager.AnimationType.In)
        {
            this.GetComponent<RectTransform>().position = des;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _FlyInEvent()
    {
        //if(onAsset.isPlayMode)
        StartCoroutine(AnimationMove(des, pos, 2));
    }

    public void _FlyOutEvent()
    {
        //if(onAsset.isPlayMode)
            StartCoroutine(AnimationMove(pos, des, 2));
    }

    IEnumerator AnimationMove(Vector3 start, Vector3 end, float time)
    {
        float timeSum=0;
        while (timeSum < time)
        {
            timeSum += Time.deltaTime;
            this.GetComponent<RectTransform>().position = start + (end - start) * timeSum / time;
            yield return null;
        }
    }
}
