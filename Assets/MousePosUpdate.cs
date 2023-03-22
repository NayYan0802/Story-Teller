using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosUpdate : MonoBehaviour
{
    Vector3 mousPos;
    void Update()
    {
        mousPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousPos.z = 0;
        this.transform.position = mousPos;
    }
}
