using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogUI : MonoBehaviour
{
    [SerializeField] private GameObject contentPanel;

    [SerializeField] private QuestLogScrollingList scrollingList;

    [SerializeField] private TextMeshProUGUI questDisplayNameText;

    [SerializeField] private TextMeshProUGUI questStatusText;

    [SerializeField] private TextMeshProUGUI goldRewardsText;

    [SerializeField] private TextMeshProUGUI experienceRewardText;

    [SerializeField] private TextMeshProUGUI levelRequirementsText;

    [SerializeField] private TextMeshProUGUI questRequirementsText;

    private Button firstSelectedButton;
    

    private void OnEnable()
    {
        GameEventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        //add button to the scrolling list if its not already there
        QuestLogButton questLogButton = scrollingList.CreateButtonIfNotExists(quest, () => { SetQuestLogInfo(quest); });


        //Initiliaze the first selected button if not already set so there is always a button selected in the top
        if(questLogButton == null)
        {
            firstSelectedButton = questLogButton.button;
            firstSelectedButton.Select();
        }
    }

    private void SetQuestLogInfo(Quest quest)
    {
        //quest name
        questDisplayNameText.text = quest.info.displayName;

        //Status

        //Level Requirements
        levelRequirementsText.text = "Level Required: " + quest.info.levelRequired;
        questRequirementsText.text = "";

        foreach(QuestInfoSO questPrerequisitesInfo in quest.info.questPrerequisites)
        {
            questRequirementsText.text += questPrerequisitesInfo.displayName + "\n";
        }

        //Rewards
        goldRewardsText.text = "Gold: " + quest.info.goldReward;
        experienceRewardText.text = "Exp: " + quest.info.expReward;
    }
}
