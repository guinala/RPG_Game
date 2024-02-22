using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List", menuName = "ScriptableObjects/Conversations")]
public class ConversationListSO : ScriptableObject
{
    public List<ConversationSO> conversations = new List<ConversationSO>();
}
