using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnAssetInLib : MonoBehaviour
{
    public GameObject Aprefab;

    GameObject DraggingObject;



    public void onPress()
    {
        DraggingObject=Instantiate(Aprefab);
        Debug.Log(this.name);
        DraggingObject.GetComponent<RawImage>().texture = this.GetComponent<RawImage>().texture;
        DraggingObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        DraggingObject.transform.SetParent(GameObject.Find("MousePos").transform);
        DraggingObject.transform.localPosition = Vector3.zero;
    }

    public void onRelease()
    {
        GameObject window = GameObject.Find("VisibleWindow");
        RectTransform windowBounds = window.GetComponent<RectTransform>();
        if (RectTransformUtility.RectangleContainsScreenPoint(windowBounds, Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            DraggingObject.transform.SetParent(GameObject.Find("VisibleWindow").transform);
            DraggingObject.transform.localScale = Vector3.one;
            DraggingObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
            this.GetComponent<OnAssetInLib>().enabled = false;
        }
        else
        {
            Destroy(DraggingObject);
        }
    }
}
