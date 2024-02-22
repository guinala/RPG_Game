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

    [Header("RectTransform")]
    [SerializeField] private RectTransform contentRectTransform;
    [SerializeField] private RectTransform scrollRectTransform;

    private Dictionary<string, QuestLogButton> idToButtonMap = new Dictionary<string, QuestLogButton>();


    //Remove after the test
    /**
    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            QuestInfoSO questInfoTest = ScriptableObject.CreateInstance<QuestInfoSO>();
            questInfoTest.id = "test_" + i;
            questInfoTest.displayName = "Test" + i;
            questInfoTest.questStepsPrefabs = new GameObject[0];
            Quest quest = new Quest(questInfoTest);

            QuestLogButton questLogButton = CreateButtonIfNotExists(quest, () => { Debug.Log("SELECTED" + questInfoTest.displayName); });

            if(i == 0)
            {
                questLogButton.button.Select();
            }
        }
    }
    */

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


        //Initialize and set up button when is selected
        RectTransform buttonRectTransform = questLogButton.GetComponent<RectTransform>();
        questLogButton.Initialize(quest.info.displayName, () => { selectAction(); UpdateScrolling(buttonRectTransform); });

        //Add to mapa to keep track of the new button
        idToButtonMap[quest.info.id] = questLogButton;

        return questLogButton;
    }

    private void UpdateScrolling(RectTransform buttonRectTransform)
    {
        //Calculate Y min and max for the selected button
        float buttonYMin = Mathf.Abs(buttonRectTransform.anchoredPosition.y);
        float buttonYMax = buttonYMin + buttonRectTransform.rect.height;


        //Calculate Y min and max for the content
        float contentYMin = contentRectTransform.anchoredPosition.y;
        float contentYMax = contentYMin + scrollRectTransform.rect.height;

        //Handle scrolling down
        if(buttonYMax > contentYMax)
        {
            contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x, buttonYMax - scrollRectTransform.rect.height);
        }
        //Handle scrolling up
        else if(buttonYMin < contentYMin)
        {
            contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x, buttonYMin);
        }
    }

}
