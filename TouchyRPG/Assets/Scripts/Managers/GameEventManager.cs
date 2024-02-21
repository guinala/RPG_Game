using System;
using UnityEngine;


public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance { get; private set; }


    public MiscEvents miscEvents;
    public QuestEvents questEvents;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one GameEventManager");
        }
        instance = this;

        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();
        Debug.Log("GameEventManager Awake");

    }
}
