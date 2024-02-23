using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class QuestPoint : MonoBehaviour
{

    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;


    private bool playerIsNear = false;

    private string questID;

    private QuestState currentQuestState;

    private QuestIcon questIcon;

    private DialogueTrigger _dialogueTrigger;



    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;


    private void Awake()
    {
        questID = questInfoForPoint.id;
        questIcon = transform.parent.GetComponentInChildren<QuestIcon>();
        _dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = true;
            Debug.Log("Player is near" + currentQuestState);
           
        }
    }

    private void OnEnable()
    {
        Debug.Log("Empanadas");
        GameEventManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        //GameEventManager.instance.inputEvents.onSubmitPressed -= OnSubmitPressed;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameEventManager.instance.questEvents.onStartQuest += StartQuest;
        //GameEventManager.instance.inputEvents.onSubmitPressed -= OnSubmitPressed;
    }

    private void StartQuest(string id)
    {
        Debug.Log("Amo Unity y a " + id + "(Sarcasmo)");
    }
    public void SubmitPressed()
    {
        Debug.Log("Me gustan los camiones");
        if(!playerIsNear)
        {
            return;
        }

        //Start or finish the quest
        if(currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            GameEventManager.instance.questEvents.StartQuest(questID);
            _dialogueTrigger.setConversation(questInfoForPoint.conversation_initial);
        }

        else if (currentQuestState.Equals(QuestState.IN_PROGRESS) || currentQuestState.Equals(QuestState.FINISHED))
        {
            _dialogueTrigger.TriggerConversationNormal();
        }

        else if(currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            GameEventManager.instance.questEvents.FinishQuest(questID);
            _dialogueTrigger.setConversation(questInfoForPoint.conversation_end);
        }
        
    }

    private void QuestStateChange(Quest quest)
    {
        //only update the quest state if this point has the corresponding quest
        if (quest.info.id.Equals(questID))
        {
            Debug.Log("Hola me llamo Manolo");
            currentQuestState = quest.state;
            questIcon.SetState(currentQuestState, startPoint, finishPoint);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = false;
            
        }
    }
}
