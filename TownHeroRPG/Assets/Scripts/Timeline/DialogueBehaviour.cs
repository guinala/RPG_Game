using UnityEngine;
using UnityEngine.Playables;

public class DialoguePlayable : PlayableBehaviour
{
    public string dialogueText;

    // Este método se llama cuando el clip comienza a reproducirse
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        // Aquí llamamos al sistema de diálogo (suponiendo que existe un método para lanzar el diálogo)
        //DialogueManager.Instance.StartDialogue(dialogueText);

        // Pausamos el PlayableGraph (que es lo mismo que pausar la Timeline)
        playable.GetGraph().GetRootPlayable(0).SetSpeed(0);
    }
}
