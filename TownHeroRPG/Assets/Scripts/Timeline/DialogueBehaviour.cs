using UnityEngine;
using UnityEngine.Playables;

public class DialoguePlayable : PlayableBehaviour
{
    public string dialogueText;

    // Este m�todo se llama cuando el clip comienza a reproducirse
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        // Aqu� llamamos al sistema de di�logo (suponiendo que existe un m�todo para lanzar el di�logo)
        //DialogueManager.Instance.StartDialogue(dialogueText);

        // Pausamos el PlayableGraph (que es lo mismo que pausar la Timeline)
        playable.GetGraph().GetRootPlayable(0).SetSpeed(0);
    }
}
