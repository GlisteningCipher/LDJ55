using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    public Transform cursor;
    public Image cursorI;
    public Sprite mouseOn, mouseOff;
    public static bool ShowMouse;

    void Awake()
    {
        Cursor.visible = false;
        ShowMouse = true;
    }

    void FixedUpdate()
    {
        if (ShowMouse)
        {
            cursorI.gameObject.SetActive(true);

            cursor.position = Input.mousePosition;

            if (Input.GetMouseButton(0))
                cursorI.sprite = mouseOn;
            else
                cursorI.sprite = mouseOff;
        }
        else
        {
            cursorI.gameObject.SetActive(false);
        }
    }

}
