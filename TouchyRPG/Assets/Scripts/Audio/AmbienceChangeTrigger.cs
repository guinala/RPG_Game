using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AmbienceChangeTrigger : MonoBehaviour
{
    [Header("Parameter Config")]
    [SerializeField] private string parameterName;
    [SerializeField] private float parameterValue;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Holaaaasdqwd");
        if (collision.tag.Equals("Player"))
        {
            Debug.Log("voy a cambiar el parametro");
            AudioManager.instance.SetAmbienceParameter(parameterName, parameterValue);
        }
    }
}




