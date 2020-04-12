using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HotbarSlot : MonoBehaviour
{

    private KeyCode _keyCode;
    private int _keyNumber;
    public string _toolName;

    private void OnValidate(){
        _keyNumber = transform.GetSiblingIndex() + 1;
        _keyCode = KeyCode.Alpha0 + _keyNumber;
        gameObject.name = "Hotbar Slot " + _keyNumber;
    }

    void Update(){
        if (Input.GetKeyDown (_keyCode)){
            Selected();
        }
    }

    public void Selected(){
        Player.playerTool = _toolName;
    }
}
