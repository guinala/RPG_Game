using UnityEngine;
using ScriptableObjectArchitecture;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Configuration")]
    private ConversationSO conversation;
    public ConversationSO normal_conversation;

    [Header("Broadcasting events")]
    public ConversationSOGameEvent conversationRequestEvent;

    public void setConversation(ConversationSO conversation)
    {
        Debug.Log("stand proud you are strong");
        this.conversation = conversation;
        TriggerConversationQuest();
    }

    public void TriggerConversationQuest()
    {
        this.conversationRequestEvent.Raise(this.conversation);
    }

    public void TriggerConversationNormal()
    {
        this.conversationRequestEvent.Raise(this.normal_conversation);
    }


}
