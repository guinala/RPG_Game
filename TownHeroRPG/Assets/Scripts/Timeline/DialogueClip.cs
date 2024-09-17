using ScriptableObjectArchitecture;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

// Definimos el PlayableAsset que será el clip en la Timeline
[System.Serializable]
public class DialogueClip : PlayableAsset, ITimelineClipAsset
{
    public DialoguePlayable behaviour = new DialoguePlayable();
    public ConversationSO dialogueText;


    // Definimos las capacidades del clip en la Timeline
    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    // Sobreescribimos CreatePlayable para definir lo que ocurre cuando se reproduce el clip
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialoguePlayable>.Create(graph);
        playable.GetBehaviour().dialogueText = dialogueText;

        return playable;
    }
}
