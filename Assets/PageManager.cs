using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    public GameObject newPage;
    public GameObject AddButton;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPage()
    {
        GameObject newOne = Instantiate(newPage);
        newOne.transform.SetParent(GameObject.Find("PageContent").transform);
        newOne.transform.localScale = Vector3.one;
        AddButton.transform.SetParent(null);
        AddButton.transform.SetParent(GameObject.Find("PageContent").transform);
    }
}
