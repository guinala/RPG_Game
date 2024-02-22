using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestLogScrollingList : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;

    [Header("Quest Log Button")]
    [SerializeField] private QuestLogButton questLogButtonPrefab;

    private Dictionary<string, QuestLogButton> idToButtonMap = new Dictionary<string, QuestLogButton>();


    public QuestLogButton CreateButtonIfNotExists(Quest quest, UnityAction selectAction)
    {

        QuestLogButton questLogButton = null;

        //only create the button if we haven't seen this quest id before
        if (!idToButtonMap.ContainsKey(quest.info.id))
        {
            questLogButton = InstantiateQuestLogButton(quest, selectAction);
        }
        else
        {
            questLogButton = idToButtonMap[quest.info.id];
        }

        return questLogButton;
    }


    private QuestLogButton InstantiateQuestLogButton(Quest quest, UnityAction selectAction)
    {
        //Create button
        QuestLogButton questLogButton = Instantiate(questLogButtonPrefab, contentParent.transform).GetComponent<QuestLogButton>();

        //Game objetct name in scene
        questLogButton.name = quest.info.id + "Button";

        questLogButton.Initiliaze(quest.info.displayName, selectAction);

        //Add to mapa to keep track of the new button
        idToButtonMap[quest.info.id] = questLogButton;

        return questLogButton;
    }

}
