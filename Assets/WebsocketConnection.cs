﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebsocketConnection : MonoBehaviour
{
    public string websocketServerSite = "ws://192.168.0.131:8080/websocket";

    private WebSocket ws = null;

    public delegate void OnStringMessageDelegate(string str);
    public delegate void OnBytesMessageDelegate(byte[] bytes);

    public event OnStringMessageDelegate onMessageStr;
    public event OnBytesMessageDelegate onMessageBytes;

    public event OnStringMessageDelegate onError;

    public void OpenConnention()
    {
        if (ws != null)
        {
            Debug.LogError("connection has already opened");
            return;
        }

        StartCoroutine(ConnectServer());
    }

    public void CloseConnention()
    {
        if (ws != null)
        {
            ws.Close();
            ws = null;
        }
    }

    public void SendStr(string str)
    {
        if (ws != null)
        {
            ws.SendString(str);
        }
    }

    public void SendBytes(byte[] bytes)
    {
        if (ws != null)
        {
            ws.Send(bytes);
        }
    }

    void Start ()
    {
    }

    IEnumerator ConnectServer()
    {
        ws = new WebSocket(new Uri(websocketServerSite));
        yield return StartCoroutine(ws.Connect());

        while (true)
        {
            string reply = ws.RecvString();
            if (reply != null && onMessageStr != null)
            {
                onMessageStr(reply);
            }

            byte[] bytes = ws.Recv();
            if (bytes != null && onMessageBytes != null)
            {
                onMessageBytes(bytes);
            }

            if (ws.error != null && onError != null)
            {
                onError(ws.error);
                break;
            }

            yield return null;
        }
        ws.Close();
    }
}
