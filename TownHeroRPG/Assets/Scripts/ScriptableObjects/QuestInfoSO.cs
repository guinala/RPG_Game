using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/Quests/Quest")]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }
   

    //ensure the id is always the name of the ScriptableObject
    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }
    public string displayName;
    public bool initialConversation;
    public bool endConversation;
    public ConversationSO conversation_initial;
    public ConversationSO conversation_end;

    [Header("Requieremnts")]
    public int levelRequired;

    public QuestInfoSO[] questPrerequisites;

    [Header("Quest Steps")]
    public GameObject[] questStepsPrefabs;

    [Header("Rewards")]
    public int goldReward;
    public int expReward;
}
