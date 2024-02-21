using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest 
{
    public QuestInfoSO info;

    public QuestState state;

    private int currentQuestStepIndex;

    private QuestStepState[] questStepStates;

    public Quest(QuestInfoSO questInfo, QuestState questState, int currentQuestStepIndex, QuestStepState[] questStepStates)
    {
        this.info = questInfo;
        this.state = questState;
        this.currentQuestStepIndex = currentQuestStepIndex;
        this.questStepStates = questStepStates;

        //If the quest step states and prefabs are dfferent lengths, something
        //has changed during development and the saved data is out of range
        if(this.questStepStates.Length != this.info.questStepsPrefabs.Length)
        {
            Debug.LogWarning("Quest Step Prefabs and Quest Step States are of different lengths. This Indicates something" +
                "has changed with the Quest info and the saved data is now out of sync. reset your data as this might cause issues" +
                this.info.id);
        }
    }

    public Quest(QuestInfoSO info)
    {
        this.info = info;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
        this.questStepStates = new QuestStepState[info.questStepsPrefabs.Length];
        for(int i = 0; i < questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();
        }
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
        if (currentQuestStepIndex >= info.questStepsPrefabs.Length)
        {
            state = QuestState.CAN_FINISH;
        }
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepsPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();

        if (questStepPrefab != null)
        {
            QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parentTransform).GetComponent<QuestStep>();
            questStep.InitializeQuestStep(info.id, currentQuestStepIndex, questStepStates[currentQuestStepIndex].state);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;

        if(CurrentStepExists())
        {
            questStepPrefab = info.questStepsPrefabs[currentQuestStepIndex];
        }

        else
        {
            Debug.LogWarning("Tried to get quest prefab, but stepIndex was out of range indicating that " + "there's" +
                "no current step:quest id = " + info.id + ", stepIndex = " + currentQuestStepIndex);
        }

        return questStepPrefab;
    }

    public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
    {
        if(stepIndex < questStepStates.Length)
        {
            questStepStates[stepIndex].state = questStepState.state;
        }
        else
        {
            Debug.LogWarning("Tried to store quest step state, but stepIndex was out of range: " + "quest id = " + info.id + ", stepIndex = " + stepIndex);
        }
    }

    public QuestData GetQuestData()
    {
        return new QuestData(state, currentQuestStepIndex, questStepStates);
    }
}
