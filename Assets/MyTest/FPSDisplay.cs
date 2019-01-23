﻿using UnityEngine;
using System.Collections;

/// <summary>
/// http://wiki.unity3d.com/index.php/FramesPerSecond
/// </summary>
public class FPSDisplay : MonoBehaviour
{
    public Color color = Color.green;
    float deltaTime = 0.0f;
    GUIStyle style;

    void Start()
    {
        style = new GUIStyle();
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = color;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
