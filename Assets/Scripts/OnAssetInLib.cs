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
        //DraggingObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
    }

    public void onRelease()
    {
        GameObject window = GameManager.Instance.currentPage;
        RectTransform windowBounds = window.GetComponent<RectTransform>();
        if (RectTransformUtility.RectangleContainsScreenPoint(windowBounds, Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            DraggingObject.transform.SetParent(GameManager.Instance.currentPage.transform);
            DraggingObject.transform.localScale = Vector3.one;
            DraggingObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
            this.GetComponent<OnAssetInLib>().enabled = false;
            //Activate the dragging point
            if (GameManager.Instance.currentObjectInWindow == null)
            {
                DraggingObject.transform.GetChild(0).GetComponent<Image>().enabled = true;
            }
            GameManager.Instance.layerList.Add(DraggingObject);
        }
        else
        {
            Destroy(DraggingObject);
        }
    }

    public void SelectImageInLib(GameObject gameObject)
    {
        GameManager.Instance.currentImageInLib = gameObject.GetComponent<OnAssetInLib>();
        GameObject[] pics = GameObject.FindGameObjectsWithTag("PicInLib");
        foreach (var pic in pics)
        {
            pic.transform.GetChild(0).GetComponent<RawImage>().enabled = false;
        }
        GameManager.Instance.currentImageInLib.transform.GetChild(0).GetComponent<RawImage>().enabled = true;
    }
}
