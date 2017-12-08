using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CursorPoint : NetworkBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D cursorClick;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Use this for initialization
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Cursor.SetCursor(cursorClick, hotSpot, cursorMode);
        }
        else
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
