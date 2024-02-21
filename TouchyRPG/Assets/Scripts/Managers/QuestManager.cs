using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private bool loadQuestState = true;

    private Dictionary<string, Quest> questMap;

    //Quest Start requirements

    private int currentPlayerLevel = 0;

    private void Awake()
    {
        Debug.Log("UNity es cacota");
        questMap = CreateQuestMap();
        Debug.Log("Unity es caca");
    }

    private void Start()
    {
        
        foreach(Quest quest in questMap.Values)
        {
            //initialize any loaded quest steps
            if(quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            //Broadcast the initial state of all quests on startup
            GameEventManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventManager.instance.questEvents.onFinishQuest += FinishQuest;

        GameEventManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;

        Debug.Log("Hola buenas");

        //Para cuando haga requisitos y experiencia
        //GameEventManager.instance.playerEvents.onPlayerLevelChange += PlayerLevelChange;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventManager.instance.questEvents.onFinishQuest -= FinishQuest;

        GameEventManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;

        //Para cuando haga requisitos y experiencia
        //GameEventManager.instance.playerEvents.onPlayerLevelChange -= PlayerLevelChange;
    }


    private void PlayerLevelChange(int level)
    {
        currentPlayerLevel = level;
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        Debug.Log("Entrando con mision" + quest.info.id);
        //Start true and prove to be false
        bool meetRequirements = true;

        //Check requirements
        if(currentPlayerLevel < quest.info.levelRequired)
        {
            Debug.Log("Nivel no alcanzado");
            meetRequirements = false;
        }


        if(quest.info.questPrerequisites.Length > 0)
        {
            // check quest prerequisites for completion
            foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
            {
                if (GetQuestById(prerequisiteQuestInfo.id).info.id != null)
                {
                    if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
                    {
                        meetRequirements = false;
                    }
                }

                else
                {
                    Debug.Log("recorcholis");
                }

            }
        }
        
        return meetRequirements;
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(id, QuestState.IN_PROGRESS);
        Debug.Log("Las empanadas son buenas");
    }

    private void Update()
    {
        
        //Loop through all quests
        foreach(Quest quest in questMap.Values)
        {
            Debug.Log("Quiero irme a casa");
            Debug.Log(quest.info.id);
            //if we are now meeting the requirements, switch over to the CAN_START state
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                Debug.Log("Mision pued ehaverse");
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
        }
    }
    private void ChangeQuestState(string id, QuestState newState)
    {
        Quest quest = GetQuestById(id);

        if(quest != null)
        {
            quest.state = newState;
            GameEventManager.instance.questEvents.QuestStateChange(quest);
        }
    }
   

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);

        //move to next step
        quest.MoveToNextStep();

        //if there are more steps, instantiate the next one
        if(quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        
        //if there are no more steps, then we have finished all of them for this quest
        else
        {
            ChangeQuestState(id, QuestState.CAN_FINISH);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        ClaimRewards(quest);
        ChangeQuestState(id, QuestState.FINISHED);
    }

    private void ClaimRewards(Quest quest)
    {
        //Claim rewards
        //GameEventManager.instance.playerEvents.GainGold(quest.info.goldReward);
        //GameEventManager.instance.playerEvents.GainExperience(quest.info.expReward);
        Debug.Log("Quest finished! Claiming rewards: " + quest.info.goldReward + " gold and " + quest.info.expReward + " experience");
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestById(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        //Loads all QuestInfoSO Scriptable Objects from the Assets/Resources/Quests folder
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

        //Create a quest map
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();

        foreach(QuestInfoSO questInfo in allQuests)
        {
            Debug.Log("Porque #@$% no funciona");
           if(idToQuestMap.ContainsKey(questInfo.id))
           {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
           }

           idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
           Debug.Log("Mision anyadida, que funcione au`sdasdad");
        }

        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];

        if(quest == null)
        {
            Debug.LogWarning("Quest with id " + id + " not found in quest map");
        }

        return quest;
    }

    private void OnApplicationQuit()
    {
        foreach(Quest quest in questMap.Values)
        {
           SaveQuest(quest);
        }
    }

    private void SaveQuest(Quest quest)
    {
        try
        {
            QuestData questData = quest.GetQuestData();
            string serializedData = JsonUtility.ToJson(questData);
            //Mejor usar guardar en disco
            PlayerPrefs.SetString(quest.info.id, serializedData);
            Debug.Log(serializedData);
        }
        catch(System.Exception e)
        {
               Debug.LogError("Error saving quest: " + quest.info.id + " " + e.Message);
        }
    }

    private Quest LoadQuest(QuestInfoSO questInfo)
    {
        Quest quest = null;

        try
        {
            //Load Quests from saved data
            if(PlayerPrefs.HasKey(questInfo.id) && loadQuestState)
            {
                string serializedData = PlayerPrefs.GetString(questInfo.id);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            }
            //otherwise initialize a new quest
            else
            {
                quest = new Quest(questInfo);
            }
        }
        catch(System.Exception e)
        {
            Debug.LogError("failed to load quest with id: " + questInfo.id + " " + e.Message);
        }

        return quest;
    }
}
