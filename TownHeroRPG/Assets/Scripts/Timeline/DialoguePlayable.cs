using ScriptableObjectArchitecture;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class DialoguePlayable : PlayableBehaviour
{
    public ConversationSO dialogueText;

    
    private PlayableGraph graph;
    private Playable thisPlayable;
    

    public override void OnPlayableCreate(Playable playable)
    {
        graph = playable.GetGraph();
        thisPlayable = playable;
        DialogueManager.Instance.cutsceneEnded += JumpToEndOfPlayable;
    }

    // Este m�todo se llama cuando el clip comienza a reproducirse
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        //Debug.Log("el dialogue es: " + dialogueText.leftCharacter);
        // Aqu� llamamos al sistema de di�logo (suponiendo que existe un m�todo para lanzar el di�logo)
        //conversationRequestEvent.Raise(dialogueText);
        if(dialogueText == null)
        {
            Debug.Log("Ultra cosas");
            return;
        }
        DialogueManager.Instance.StartConversation(dialogueText);


        // Pausamos el PlayableGraph (que es lo mismo que pausar la Timeline)
        graph.GetRootPlayable(0).SetSpeed(0);
    }
    
    private void JumpToEndOfPlayable()
    {
        graph.GetRootPlayable(0).SetTime(graph.GetRootPlayable(0).GetTime() + thisPlayable.GetDuration());
    }
}
