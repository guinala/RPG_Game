using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MissionManager : MonoBehaviour
{
    [Header("Dependencies")]
    public MissionUI missionUI;

    //[Header("Action events")]
    //public UnityEvent onConversationStarted;
    //public UnityEvent onConversationEnded;

    //private Queue<Sentence> sentences;
    /**
    private void Start()
    {
        this.sentences = new Queue<Sentence>();
    }
    **/
    public void StartMission(MissionSO mission)
    {
        

        this.missionUI.StartMission(
            mission
        );

       
    }

   
}
