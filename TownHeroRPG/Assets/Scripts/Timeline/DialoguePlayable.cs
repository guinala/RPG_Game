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
    }

    // Este método se llama cuando el clip comienza a reproducirse
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        //Debug.Log("el dialogue es: " + dialogueText.leftCharacter);
        // Aquí llamamos al sistema de diálogo (suponiendo que existe un método para lanzar el diálogo)
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
}
