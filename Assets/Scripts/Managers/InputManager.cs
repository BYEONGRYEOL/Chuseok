using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;

    public void OnUpdate()
    {
        
        if (Input.anyKey)
            if(KeyAction != null)
                KeyAction.Invoke();
        if (EventSystem.current.IsPointerOverGameObject())
            return;
    }

    public void Clear()
    {
        KeyAction = null;
    }
}
