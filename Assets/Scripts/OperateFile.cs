﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class OperateFile:MonoBehaviour
{
    public GameObject Aprefab;

    //[MenuItem("Tool/SelectFile")] 
    public static void SeleclFile()
    {
        string selectPath =  SelectFileLog.GetSelectFileName();
        Debug.LogError("选择的路径:" + selectPath);
    }

    //[MenuItem("Tool/OpenFile")]
    public void OpenFile()
    {
        OpenFileLog.OpenFileName_(Aprefab);
    }

    //[MenuItem("Tool/SaveFile")]
    public static void SaveFile()
    {
        OpenFileLog.SaveFileName();
    }
}
