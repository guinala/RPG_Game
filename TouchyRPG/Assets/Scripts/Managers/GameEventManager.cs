using System;
using UnityEngine;


public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance { get; private set; }


    public MiscEvents miscEvents;
    public QuestEvents questEvents;
    public InputEvents inputEvents;
    public PlayerEvents playerEvents;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one GameEventManager");
        }
        instance = this;

        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();
        inputEvents = new InputEvents();
        playerEvents = new PlayerEvents();
        Debug.Log("GameEventManager Awake");

    }
}
