using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


/**
 * Segunda manera (opcional) de manejar los eventos de FMOD
 * */
public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference musicMenu { get; private set; }

    [field: Header("SFX Footsteps")]
    [field: SerializeField] public EventReference footsteps { get; private set; }

    [field: Header("SFX Sword")]
    [field: SerializeField] public EventReference swordCollected { get; private set; }

    [field: SerializeField] public EventReference swordIdle { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one FMODEvents than this");
        }

        instance = this;
    }
}
