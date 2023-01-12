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
        //_outline.enabled = true ? false : true;

        if(_outline.enabled == true)
        {
            _outline.enabled = false;
        }
        else if(_outline.enabled == false)
        {
            _outline.enabled = true;
        }
    }

}
