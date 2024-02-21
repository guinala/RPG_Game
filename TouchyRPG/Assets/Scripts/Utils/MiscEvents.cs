using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Globalization;


public class MiscEvents
{
    public event Action<string> onObjectCollected;
    //public event Action onObjectDropped;

    public void ObjectCollected(String name)
    {
        Debug.Log("Hola obwejrlkwehfiafjkavhuofjgeughasf");
        if(onObjectCollected != null)
        {
            onObjectCollected(name);
        }
    }
}
