using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditorInternal.VersionControl.ListControl;

public class MissionManager : MonoBehaviour
{
    [Header("Dependencies")]
    public MissionUI missionUI;
    public QuestListSO questList;

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
        

       // this.missionUI.StartMission(
         //   mission
        //);
        mission.Start();

       
    }


    public void ReloadMissions(QuestListSO questList)
    {
        questList.reload();
    }

   
}
