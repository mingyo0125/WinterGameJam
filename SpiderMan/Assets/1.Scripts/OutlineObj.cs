using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OutlineObj : MonoBehaviour
{
    public static Action outLine;
    Outline _outline;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        outLine = () =>
        {
            OutlineOnOff();
        };
    }

    private void OutlineOnOff()
    {
        _outline.enabled = _outline.enabled == true ? false : true;
    }
}
